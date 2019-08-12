using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MSUDTrack.DataModels.Models;
using MSUDTrack.Services;
using NinjaNye.SearchExtensions;

namespace MSUDTrack.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodsController : ControllerBase
    {
        private readonly TrackerDbContext _context;
        private readonly FoodsService _foodsService;

        public FoodsController(TrackerDbContext context, FoodsService foodsService)
        {
            _context = context;
            _foodsService = foodsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Food>>> GetFood(string query, int page_limit)
        {
            var search = query.ToLower().Split(" ");

            return await _foodsService.Get()
                .Search(x => x.Name.ToLower(),
                        x => x.Manufacturer.ToLower())
                .Containing(search)
                .OrderBy(f => f.LastUsed).ThenBy(f => f.TimesUsed)
                .Take(page_limit)
                .ToListAsync();
        }
    }
}
