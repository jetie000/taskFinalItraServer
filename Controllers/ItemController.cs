using finalTaskItra.Data;
using finalTaskItra.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Collections;

namespace finalTaskItra.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private readonly EFCoreContext _context;

        public ItemController(IConfiguration configuration, IWebHostEnvironment env, EFCoreContext context)
        {
            _configuration = configuration;
            _env = env;
            _context = context;
        }

        [HttpGet("getOne/")]
        public JsonResult GetOneItem(Item item, string accessToken)
        {
            User? user = _context.users
                .FirstOrDefault(user => user.accessToken == accessToken);
            if (user is null)
                return new JsonResult("No user found.");
            Item? responseItem = _context.items
                .Include(itemFind => itemFind.likes)
                .Include(itemFind => itemFind.tags)
                .Include(itemFind => itemFind.fields)
                .Include(itemFind => itemFind.comments)
                .FirstOrDefault(itemFind => itemFind.id == item.id);
            if (responseItem is null)
                return new JsonResult("No item found.");
            ItemInfo itemInfo = new ItemInfo();
            itemInfo.item = item;
            itemInfo.userId = responseItem.myCollection.user.id;
            itemInfo.collectionId = responseItem.myCollection.id;
            return new JsonResult(itemInfo);
        }


        [HttpPost("add/")]
        [Authorize(Roles = "0")]
        public JsonResult PostMyItem(Item item, int collectionId, string accessToken)
        {
            User? user = _context.users
                .Include(user => user.collections)
                .ThenInclude(c => c.collectionFields)
                .FirstOrDefault(user => user.accessToken == accessToken);
            if (user is null)
                return new JsonResult("No user found.");
            MyCollection? collection = user.collections.FirstOrDefault(collection => collection.id == collectionId);
            if (collection is null)
                return new JsonResult("No collection found.");
            if (collection.collectionFields.Count != item.fields.Count)
                return new JsonResult("Fields don't match.");
            CollectionFields[] collectionFields = collection.collectionFields.ToArray();
            ItemFields[] itemFields = item.fields.ToArray();
            for (int i = 0; i < collection.collectionFields.Count; i++)
            {
                if (collectionFields[i].fieldName != itemFields[i].fieldName)
                    switch (collectionFields[i].fieldType)
                    {
                        case "string":
                            if (itemFields[i].stringFieldValue == null)
                                return new JsonResult("Fields don't match.");
                            break;
                        case "number":
                            if (itemFields[i].doubleFieldValue == null)
                                return new JsonResult("Fields don't match.");
                            break;
                        case "boolean":
                            if (itemFields[i].boolFieldValue == null)
                                return new JsonResult("Fields don't match.");
                            break;
                        case "date":
                            if (itemFields[i].dateFieldValue == null)
                                return new JsonResult("Fields don't match.");
                            break;
                    }
                if (itemFields[i].doubleFieldValue == null 
                    && itemFields[i].boolFieldValue == null 
                    && itemFields[i].dateFieldValue == null 
                    && (itemFields[i].stringFieldValue == null || itemFields[i].stringFieldValue?.Trim() == ""))
                {
                    return new JsonResult("There are empty fields.");
                }
            }
            collection.items.Add(item);
            _context.SaveChanges();
            return new JsonResult("Item added.");
        }
    }
}
