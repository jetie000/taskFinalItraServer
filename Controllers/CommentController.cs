using finalTaskItra.Data;
using finalTaskItra.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace finalTaskItra.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private readonly EFCoreContext _context;

        public CommentController(IConfiguration configuration, IWebHostEnvironment env, EFCoreContext context)
        {
            _configuration = configuration;
            _env = env;
            _context = context;
        }

        [HttpPost("add/")]
        [Authorize(Roles = "0")]
        public JsonResult PostMyComment(Comment comment, int itemId, string accessToken)
        {
            User? user = _context.users
                .FirstOrDefault(user => user.accessToken == accessToken && user.id == comment.userId && user.fullName == comment.userFullName);
            if (user is null)
                return new JsonResult("No user found.");
            Item? item = _context.items.FirstOrDefault(item => item.id == itemId);
            if (item is null)
                return new JsonResult("No item found.");
            Comment commentToAdd = comment;
            commentToAdd.creationDate = DateTime.Now;
            item.comments.Add(commentToAdd);
            _context.SaveChanges();
            return new JsonResult("Comment added.");
        }

        [HttpDelete("delete/")]
        [Authorize(Roles = "0")]
        public JsonResult DeleteMyComment(int commentId, string accessToken)
        {
            User? user = _context.users
                .FirstOrDefault(user => user.accessToken == accessToken);
            if (user is null)
                return new JsonResult("No user found.");
            Comment? commentFind = _context.comments
                .Include(comment => comment.item)
                    .ThenInclude(item => item!.myCollection)
                    .ThenInclude(myCollection => myCollection!.user)
                .FirstOrDefault(commentFind => commentFind.id == commentId);
            if (commentFind is null)
                return new JsonResult("No comment found");
            if (commentFind.item!.myCollection!.user!.accessToken != accessToken)
                return new JsonResult("No access to comment.");
            _context.comments.Remove(commentFind);
            _context.SaveChanges();
            return new JsonResult("Comment deleted.");
        }
    }
}
