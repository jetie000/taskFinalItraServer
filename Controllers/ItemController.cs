using finalTaskItra.Data;
using finalTaskItra.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Collections;
using System.Security.Cryptography;

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

        [HttpGet("getLast/")]
        public JsonResult GetLast(int limit)
        {
            if(limit < 0)
                return new JsonResult("Wrong limit.");
            Item?[] responseItem = _context.items
                .Include(itemFind => itemFind.likes)
                .Include(itemFind => itemFind.tags)
                .Include(itemFind => itemFind.fields)
                .Include(itemFind => itemFind.comments)
                .Include(itemFind => itemFind.myCollection)
                    .ThenInclude(myCollection => myCollection!.user)
                .OrderByDescending(item => item.id).Take(limit).ToArray();
            if (responseItem is null || responseItem.Length == 0)
                return new JsonResult("No item found.");
            var itemInfo = new List<ItemInfo>();
            for (int i = 0; i < responseItem.Length; i++)
            {
                int id = responseItem[i]!.myCollection!.user!.id;
                responseItem[i]!.myCollection!.user!.accessToken = null!;
                responseItem[i]!.myCollection!.user!.saltedPassword = null!;
                responseItem[i]!.myCollection!.user!.email = null!;
                itemInfo.Add(new ItemInfo
                {
                    userId = id,
                    collectionId = responseItem[i]!.myCollection!.id,
                    item = responseItem[i]!
                });
            }
            return new JsonResult(itemInfo);
        }

        [HttpGet("getOne/")]
        public JsonResult GetOneItem(int itemId)
        {
            Item? responseItem = _context.items
                .Include(itemFind => itemFind.likes)
                .Include(itemFind => itemFind.tags)
                .Include(itemFind => itemFind.fields)
                .Include(itemFind => itemFind.comments)
                .Include(itemFind => itemFind.myCollection)
                    .ThenInclude(myCollection => myCollection!.user)
                .FirstOrDefault(itemFind => itemFind.id == itemId);
            if (responseItem is null)
                return new JsonResult("No item found.");
            ItemInfo itemInfo = new ItemInfo();
            itemInfo.userId = responseItem!.myCollection!.user!.id;
            itemInfo.collectionId = responseItem.myCollection.id;
            responseItem.myCollection.user = null;
            itemInfo.item = responseItem;
            return new JsonResult(itemInfo);
        }


        [HttpPost("add/")]
        [Authorize(Roles = "0, 1")]
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
            string response = checkFieldsMatch(collectionFields, itemFields);
            if (response != "Ok")
                return new JsonResult(response);
            collection.items.Add(item);
            _context.SaveChanges();
            return new JsonResult("Item added.");
        }

        [HttpPut("change/")]
        [Authorize(Roles = "0, 1")]
        public JsonResult ChangeMyItem(Item item, string accessToken)
        {
            User? user = _context.users
                .FirstOrDefault(user => user.accessToken == accessToken);
            if (user is null)
                return new JsonResult("No user found.");
            Item? itemFind = _context.items
                .Include(itemFind => itemFind.fields)
                .Include(itemFind => itemFind.tags)
                .Include(itemFind => itemFind.myCollection)
                .ThenInclude(myCollection => myCollection!.user)
                .FirstOrDefault(itemFind => itemFind.id == item.id);
            if (itemFind is null)
                return new JsonResult("No item found.");
            if (itemFind!.myCollection!.user!.accessToken != accessToken && user.role == 0)
                return new JsonResult("No access to item.");
            CollectionFields[] collectionFieldsArr = _context.collectionFields.Where(field => field.myCollection!.id == itemFind.id).ToArray();
            ItemFields[] itemFieldsArr = item.fields.ToArray();
            string response = checkFieldsMatch(collectionFieldsArr, itemFieldsArr);
            if (response != "Ok")
                return new JsonResult(response);
            itemFind.name = item.name;
            _context.tags.RemoveRange(itemFind.tags);
            itemFind.tags = item.tags;
            itemFind.fields = item.fields;
            _context.SaveChanges();
            return new JsonResult("Item changed.");
        }

        [HttpDelete("delete/")]
        [Authorize(Roles = "0, 1")]
        public JsonResult DeleteMyItem(int itemId, string accessToken)
        {
            User? user = _context.users
                .FirstOrDefault(user => user.accessToken == accessToken);
            if (user is null)
                return new JsonResult("No user found.");
            Item? itemFind = _context.items
                .Include(itemFind => itemFind.myCollection)
                    .ThenInclude(myCollection => myCollection!.user)
                .FirstOrDefault(itemFind => itemFind.id == itemId);
            if (itemFind is null)
                return new JsonResult("No item found");
            if (itemFind!.myCollection!.user!.accessToken != accessToken && user.role == 0)
                return new JsonResult("No access to item.");
            _context.items.Remove(itemFind);
            _context.SaveChanges();
            return new JsonResult("Item deleted.");
        }

        private static string checkFieldsMatch(CollectionFields[] collectionFields, ItemFields[] itemFields)
        {
            for (int i = 0; i < collectionFields.Length; i++)
            {
                if (collectionFields[i].fieldName != itemFields[i].fieldName)
                    switch (collectionFields[i].fieldType)
                    {
                        case "string":
                            if (itemFields[i].stringFieldValue == null)
                                return "Fields don't match.";
                            break;
                        case "number":
                            if (itemFields[i].doubleFieldValue == null)
                                return "Fields don't match.";
                            break;
                        case "boolean":
                            if (itemFields[i].boolFieldValue == null)
                                return "Fields don't match.";
                            break;
                        case "date":
                            if (itemFields[i].dateFieldValue == null)
                                return "Fields don't match.";
                            break;
                    }
                if (itemFields[i].doubleFieldValue == null
                    && itemFields[i].boolFieldValue == null
                    && itemFields[i].dateFieldValue == null
                    && (itemFields[i].stringFieldValue == null || itemFields[i].stringFieldValue?.Trim() == ""))
                {
                    return "There are empty fields.";
                }
            }
            return "Ok";
        }
    }
}
