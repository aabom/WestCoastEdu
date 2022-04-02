using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WestCoastEdu.DataAccess;
using WestCoastEdu.DataAccess.Repository.IRepository;
using WestCoastEdu.Models;

namespace WCE.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly ApplicationDbContext _applicationDbContext;

        public UserController(ILogger<ProductController> logger, ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<ApplicationUser>> GetAll()
        {
            return await _applicationDbContext.ApplicationUsers.ToListAsync();
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<ApplicationUser>> GetById(string id)
        {
            var user = await _applicationDbContext.ApplicationUsers.FindAsync(id);
            if (user == null)
            {
                return NotFound($"User with id:{id} not found");
            }
            return Ok(user);
        }

        [HttpGet("GetByEmail/{email}")]
        public async Task<ActionResult<ApplicationUser>> GetByEmail(string email)
        {
            var user = await _applicationDbContext.ApplicationUsers.FirstOrDefaultAsync(user => user.Email == email);
            if (user == null)
            {
                return NotFound($"User with Email:{email} not found");
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<ApplicationUser>> AddUser(ApplicationUser user)
        {
            if (!ModelState.IsValid || user == null)
            {
                return BadRequest();
            }

            _applicationDbContext.ApplicationUsers.Add(user);
            await _applicationDbContext.SaveChangesAsync();
            return Ok("User added successfully");
        }

        [HttpPut]
        public async Task<ActionResult<ApplicationUser>> UpdateUser(ApplicationUser user, string id)
        {
            var dbUser = await _applicationDbContext.ApplicationUsers.FirstOrDefaultAsync(user => user.Id == id);
            if(dbUser == null)
            {
                return BadRequest("User not found");
            }

            dbUser.FirstName = user.FirstName;
            dbUser.LastName = user.LastName;
            dbUser.Email = user.Email;
            dbUser.PhoneNumber = user.PhoneNumber;
            dbUser.StreetAddress = user.StreetAddress;
            dbUser.PostalCode = user.PostalCode;
            dbUser.City = user.City;

            await _applicationDbContext.SaveChangesAsync();
            return Ok("User updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApplicationUser>> DeleteProduct(int id)
        {
            var dbUser = await _applicationDbContext.ApplicationUsers.FindAsync(id);
            if(dbUser == null)
            {
                return BadRequest("User not found");
            }

            _applicationDbContext.ApplicationUsers.Remove(dbUser);
            await _applicationDbContext.SaveChangesAsync();
            return Ok($"User with id:{id} removed successfully");
        }

        [HttpGet("GetCustomerOrderHistory/{userId}")]
        public async Task<ActionResult<Product>> GetCustomerOrderHistory(string userId)
        {
            var userFromDb = await _applicationDbContext.ApplicationUsers.FindAsync(userId);

            if (userFromDb == null)
            {
                return BadRequest("User not found");
            }

            List<OrderHeader> products = _applicationDbContext.OrderHeaders.Where(x => x.ApplicationUserId == userId).ToList();
            return Ok(products);

        }

    }
}