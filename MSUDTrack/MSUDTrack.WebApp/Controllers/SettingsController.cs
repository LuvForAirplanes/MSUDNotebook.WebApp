using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MSUDTrack.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        // GET: api/Settings/leucineGrams/{grams}
        [HttpGet("/{grams}", Name = "Get")]
        public string Get(int grams)
        {
            return "";
        }
    }
}
