using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MSUDTrack.DataModels.Models;
using MSUDTrack.Services;

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
            return await _foodsService.Get()
                .Where(f => (f.Name 
                    .ToLower() + " " + f.Manufacturer.ToLower())
                    .ContainsAny(query.ToLower().Split(' ', StringSplitOptions.None)))
                .Take(page_limit)
                .OrderBy(f => f.LastUsed)
                .OrderBy(f => f.TimesUsed)
                .ToListAsync();
        }
    }
}
