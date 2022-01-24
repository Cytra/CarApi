using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarApi.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarApi.Data.Contexts;
using CarApi.Data.Entities;

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

        [HttpGet("JustTesting")]
        public async Task<IActionResult> GetAd()
        {

            return Ok("ok");
        }
    }
}
