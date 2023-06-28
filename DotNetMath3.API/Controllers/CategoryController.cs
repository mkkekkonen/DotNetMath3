using DotNetMath3.API.DbContexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetMath3.API.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly MathDataContext _context;

        public CategoryController(MathDataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/Categories")]
        public IActionResult GetCategories()
        {
            var categories = _context.Categories
                .Where(category => category.ParentCategory == null)
                .ToList();

            return Ok(categories);
        }

        [Authorize]
        [HttpGet]
        [Route("api/AllCategories")]
        public IActionResult GetAllCategories()
        {
            var categories = _context.Categories
                .ToList();

            return Ok(categories);
        }
    }
}