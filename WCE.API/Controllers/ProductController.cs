using Microsoft.AspNetCore.Mvc;
using WestCoastEdu.DataAccess;
using WestCoastEdu.DataAccess.Repository.IRepository;
using WestCoastEdu.Models;

namespace WCE.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {        
        private readonly ILogger<ProductController> _logger;
        private readonly ApplicationDbContext _applicationDbContext;

        public ProductController(ILogger<ProductController> logger, ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<Product> GetProducts()
        {
            return _applicationDbContext.Products.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var product = await _applicationDbContext.Products.FindAsync(id);
            if (product == null)
            {
                return BadRequest("Product not found");
            }
            return Ok(product);
        }
        
    }
}