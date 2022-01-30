using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using CarApi.Core.Services;
using Microsoft.AspNetCore.Mvc;
using CarApi.Requests;

namespace CarApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly IAutoPliusProvider _autoPliusService;
        public CarController(IAutoPliusProvider autoPliusService)
        {
            _autoPliusService = autoPliusService;
        }

        [HttpPost("GetAllAutoPliusCarAdds")]
        public async Task<IActionResult> GetAllAutoPliusCarAdds(GetAllAutoPliusCarAddRequest request)
        {
            var result = await _autoPliusService.GetAllAutoPliusCarAdds(
                request.YearFrom, request.YearTo, request.CarModel, request.Cookie);
            return Ok(result);
        }

        [HttpPost("GetAllNewAutoPliusCarAdds")]
        public async Task<IActionResult> GetAllNewAutoPliusCarAdds(GetAllNewAutoPliusCarAddRequest request)
        {
            var result = await _autoPliusService.GetAllNewAutoPliusCarAdds(request.Cookie);
            return Ok(result);
        }
    }
}
