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

        [HttpGet]
        public IEnumerable<Product> GetAll()
        {
            return _applicationDbContext.Products.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await _applicationDbContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound($"Product with id:{id} not found");
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            if (!ModelState.IsValid || product == null)
            {
                return BadRequest();
            }

            _applicationDbContext.Products.Add(product);
            await _applicationDbContext.SaveChangesAsync();
            return Ok("Product added successfully");
        }

        [HttpPut]
        public async Task<ActionResult<Product>> UpdateProduct(Product product)
        {
            var dbProduct = await _applicationDbContext.Products.FindAsync(product.Id);
            if(dbProduct == null)
            {
                return BadRequest("Product not found");
            }

            dbProduct.Title = product.Title;
            dbProduct.Description = product.Description;
            dbProduct.Length = product.Length;
            dbProduct.Level = product.Level;
            dbProduct.Price = product.Price;
            dbProduct.ImageUrl = product.ImageUrl;
            dbProduct.LocationId = product.LocationId;
            dbProduct.StatusId = product.StatusId;

            await _applicationDbContext.SaveChangesAsync();
            return Ok("Product updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var dbProduct = await _applicationDbContext.Products.FindAsync(id);
            if(dbProduct == null)
            {
                return BadRequest("Product not found");
            }

            _applicationDbContext.Remove(dbProduct);
            await _applicationDbContext.SaveChangesAsync();
            return Ok($"Product with id:{id} removed successfully");
        }
    }
}