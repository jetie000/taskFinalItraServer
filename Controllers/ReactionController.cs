using finalTaskItra.Data;
using finalTaskItra.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace finalTaskItra.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReactionController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private readonly EFCoreContext _context;

        public ReactionController(IConfiguration configuration, IWebHostEnvironment env, EFCoreContext context)
        {
            _configuration = configuration;
            _env = env;
            _context = context;
        }

        [HttpPost("set/")]
        [Authorize(Roles = "0, 1")]
        public JsonResult SetReaction(Reaction reaction, int itemId, string accessToken)
        {
            User? user = _context.users
                .FirstOrDefault(user => user.accessToken == accessToken && user.id == reaction.userId);
            if (user is null)
                return new JsonResult("No user found.");
            Item? item = _context.items.FirstOrDefault(item => item.id == itemId);
            if (item is null)
                return new JsonResult("No item found.");
            Reaction reactionToSet = reaction;
            reactionToSet.creationDate = DateTime.Now;
            Reaction? reactionToFindLike = _context.likes
                .Include(like => like.item)
                .FirstOrDefault(reactionToFind => reactionToFind.isLike == true && reactionToFind.userId == reaction.userId && reactionToFind.item!.id == itemId);
            Reaction? reactionToFindDislike = _context.likes
                .Include(like => like.item)
                .FirstOrDefault(reactionToFind => reactionToFind.isLike == false && reactionToFind.userId == reaction.userId && reactionToFind.item!.id == itemId);
            if (reactionToFindLike is null && reactionToFindDislike is null)
                item.likes.Add(reactionToSet);
            else
            {
                if (reactionToSet.isLike == true)
                {
                    if (reactionToFindLike is null)
                    {
                        item.likes.Add(reactionToSet);
                        _context.likes.Remove(reactionToFindDislike!);
                    }
                    else
                        _context.likes.Remove(reactionToFindLike!);

                }
                else
                {
                    if (reactionToFindDislike is null)
                    {
                        item.likes.Add(reactionToSet);
                        _context.likes.Remove(reactionToFindLike!);
                    }
                    else
                        _context.likes.Remove(reactionToFindDislike!);
                }
            }
            _context.SaveChanges();
            return new JsonResult("Reaction set.");
        }

        [HttpGet("getMy/")]
        [Authorize(Roles = "0, 1")]
        public JsonResult getReactions(string accessToken)
        {
            User? user = _context.users
                .FirstOrDefault(user => user.accessToken == accessToken);
            if (user is null)
                return new JsonResult("No user found.");
            Item?[] items = _context.items
                .Include(item => item.likes)
                .Include(item => item.comments)
                .Include(item => item.tags)
                .Include(item => item.myCollection).Where(item => item.likes.Any(like => like.userId == user.id)).ToArray();
            return new JsonResult(items);
        }

    }
}
