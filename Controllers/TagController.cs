using finalTaskItra.Data;
using finalTaskItra.Models;
using LinqKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace finalTaskItra.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private readonly EFCoreContext _context;

        public TagController(IConfiguration configuration, IWebHostEnvironment env, EFCoreContext context)
        {
            _configuration = configuration;
            _env = env;
            _context = context;
        }

        [HttpGet("get/")]
        public JsonResult getTags(string contain, int limit)
        {
            Tag[] tags;
            if (!string.IsNullOrWhiteSpace(contain))
            {
                var terms = contain.Split(' ').ToList();
                var predicate = PredicateBuilder.New<Tag>(false);
                foreach (var term in terms)
                    predicate = predicate.Or(x => x.tag.Contains(term));
                tags = _context.tags.Where(predicate).OrderByDescending(tag => tag.id).Take(limit).ToArray();
            }
            else
                tags = _context.tags.OrderByDescending(tag => tag.id).Take(limit).ToArray();
            return new JsonResult(tags);
        }

        [HttpGet("getLast/")]
        public JsonResult getLastTags(int limit)
        {
            if (limit < 0)
                return new JsonResult("Wrong limit.");
            Tag[] tags = _context.tags.OrderByDescending(tag => tag.id).Take(limit).ToArray();
            return new JsonResult(tags);
        }
    }
}
