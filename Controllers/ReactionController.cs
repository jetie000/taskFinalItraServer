using finalTaskItra.Data;
using finalTaskItra.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [Authorize(Roles = "0")]
        public JsonResult SetReaction(Reaction reaction, int itemId, string accessToken)
        {
            User? user = _context.users
                .FirstOrDefault(user => user.accessToken == accessToken && user.id == reaction.userId);
            if (user is null)
                return new JsonResult("No user found.");
            Item? item = _context.items.FirstOrDefault(item => item.id == itemId);
            if (item is null)
                return new JsonResult("No item found.");
            Reaction? reactionToFind = _context.likes.FirstOrDefault(reactionToFind => reactionToFind.isLike == reaction.isLike && reactionToFind.userId == reaction.userId);
            if (reactionToFind is null)
                item.likes.Add(reaction);
            else
                item.likes.Remove(reactionToFind);
            _context.SaveChanges();
            return new JsonResult("Reaction set.");
        }

    }
}
