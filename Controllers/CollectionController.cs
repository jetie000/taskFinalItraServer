using finalTaskItra.Data;
using finalTaskItra.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            return new JsonResult(user.collections);
        }

        [HttpPost("add/")]
        [Authorize(Roles = "0")]
        public JsonResult PostMyCollection(Collection collection, string accessToken)
        {
            User? user = _context.users.FirstOrDefault(user => user.accessToken == accessToken);
            if (user is null)
                return new JsonResult("No user found.");
            user.collections?.Add(collection);
            return new JsonResult("Collection added.");
        }

        [HttpPut("changeInfoMy/")]
        [Authorize(Roles = "0")]
        public JsonResult changeMyCollection(Collection collection, string accessToken)
        {
            User? user = _context.users.FirstOrDefault(user => user.accessToken == accessToken);
            if (user is null)
                return new JsonResult("No user found.");
            Collection? myCollection = user.collections?.FirstOrDefault(collectionToFind => collectionToFind.id == collection.id);
            if (myCollection is null)
                return new JsonResult("No collection found.");
            myCollection.title = collection.title;
            myCollection.description = collection.description;
            myCollection.theme = collection.theme;
            myCollection.photoPath = collection.photoPath;
            _context.SaveChanges();
            return new JsonResult("Collection changed.");
        }

        [HttpDelete("delete")]
        [Authorize(Roles = "0")]
        public JsonResult deleteMyCollection(int id, string accessToken)
        {
            User? user = _context.users.FirstOrDefault(user => user.accessToken == accessToken);
            if (user is null)
                return new JsonResult("No user found.");
            Collection? myCollection = user.collections?.FirstOrDefault(collectionToFind => collectionToFind.id == id);
            if (myCollection is null)
                return new JsonResult("No collection found.");
            _context.collections.Remove(myCollection);
            _context.SaveChanges();
            return new JsonResult("Collection deleted.");
        }
    }
}
