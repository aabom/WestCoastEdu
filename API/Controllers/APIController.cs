using Microsoft.AspNetCore.Mvc;
using WestCoastEdu.DataAccess.Repository.IRepository;
using WestCoastEdu.Models;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [ApiController]
        [Route("[controller]")]
        public class APIController : ControllerBase
        {
            private readonly IUnitOfWork _unitOfWork;


            public APIController(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            [HttpGet]
            public IEnumerable<Product> GetAllProducts()
            {
                return _unitOfWork.Product.GetAll();
            }


        }
    }
}