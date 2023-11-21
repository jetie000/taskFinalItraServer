using finalTaskItra.Data;
using finalTaskItra.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace finalTaskItra.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CollectionController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private readonly EFCoreContext _context;

        public CollectionController(IConfiguration configuration, IWebHostEnvironment env, EFCoreContext context)
        {
            _configuration = configuration;
            _env = env;
            _context = context;
        }

        [HttpGet("getMy/")]
        [Authorize(Roles = "0")]
        public JsonResult GetMyCollections(string accessToken)
        {
            User? user = _context.users.FirstOrDefault(user => user.accessToken == accessToken);
            if (user is null)
                return new JsonResult("No user found.");
            var collections = _context.users
                .Include(user => user.collections)
                    .ThenInclude(c => c.items)
                .Include(user => user.collections)
                    .ThenInclude(c => c.collectionFields)
                .FirstOrDefault(user => user.accessToken == accessToken)
                .collections
                .ToList();
            return new JsonResult(collections);
        }

        [HttpGet("getOneMy/")]
        [Authorize(Roles = "0")]
        public JsonResult GetOneMyCollection(string accessToken, int collectionId)
        {
            User? user = _context.users.Include(user => user.collections).FirstOrDefault(user => user.accessToken == accessToken);
            if (user is null)
                return new JsonResult("No user found.");
            var collection = _context.collections
                .Include(collection => collection.collectionFields)
                .Include(collection => collection.items)
                    .ThenInclude(item => item.tags)
                .Include(collection => collection.items)
                    .ThenInclude(item => item.likes)
                .FirstOrDefault(collection => collection.id == collectionId);
            if (collection is null)
                return new JsonResult("No collection found.");
            if (!user.collections.Any(collectionToCheck => collectionToCheck.id == collection.id))
                return new JsonResult("This collection is not yours.");
            return new JsonResult(collection);
        }

        [HttpPost("add/")]
        [Authorize(Roles = "0")]
        public JsonResult PostMyCollection(MyCollection collection, string accessToken)
        {
            User? user = _context.users.FirstOrDefault(user => user.accessToken == accessToken);
            if (user is null)
                return new JsonResult("No user found.");
            user.collections.Add(collection);
            _context.SaveChanges();
            return new JsonResult("Collection added.");
        }
        
        [HttpPut("changeInfoMy/")]
        [Authorize(Roles = "0")]
        public JsonResult changeMyCollection(MyCollection collection, string accessToken)
        {
            User? user = _context.users.FirstOrDefault(user => user.accessToken == accessToken);
            if (user is null)
                return new JsonResult("No user found.");
            MyCollection? myCollection = user.collections?.FirstOrDefault(collectionToFind => collectionToFind.id == collection.id);
            if (myCollection is null)
                return new JsonResult("No collection found.");
            myCollection.title = collection.title;
            myCollection.description = collection.description;
            myCollection.theme = collection.theme;
            myCollection.photoPath = collection.photoPath;
            _context.SaveChanges();
            return new JsonResult("Collection changed.");
        }

        [HttpDelete("delete/")]
        [Authorize(Roles = "0")]
        public JsonResult deleteMyCollection(int id, string accessToken)
        {
            User? user = _context.users
                .Include(user => user.collections)
                .FirstOrDefault(user => user.accessToken == accessToken);
            if (user is null)
                return new JsonResult("No user found.");
            MyCollection? myCollection = user.collections?
                .FirstOrDefault(collectionToFind => collectionToFind.id == id);
            if (myCollection is null)
                return new JsonResult("No collection found.");
            _context.collections.Remove(myCollection);
            _context.SaveChanges();
            return new JsonResult("Collection deleted.");
        }

        [HttpPost("saveCollectionPhoto/")]
        [Authorize(Roles = "0")]
        public JsonResult SaveCollectionPhoto(string accessToken)
        {
            var userId = _context.users.FirstOrDefault(user => user.accessToken == accessToken)?.id;
            if (userId is null)
                return new JsonResult("default.jpg");
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/photos/" + userId;
                DirectoryInfo dirInfo = new DirectoryInfo(physicalPath);
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }
                string filePath = physicalPath + "/" + fileName;
                string newFilePath = filePath;
                string newFileName = fileName;
                int filesNum = 1;
                while (true)
                {
                    FileInfo fileInf = new FileInfo(newFilePath);
                    if (fileInf.Exists)
                    {
                        newFilePath = filePath.Insert(filePath.Length - 4, " (" + filesNum + ")");
                        newFileName = fileName.Insert(fileName.Length - 4, " (" + filesNum + ")");
                        filesNum++;
                    }
                    else
                        break;
                }
                using (var stream = new FileStream(newFilePath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                return new JsonResult(userId + "/" + newFileName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new JsonResult("default.jpg");
            }
        }
    }
}
