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
    public class ChildrenController : ControllerBase
    {
        private readonly ChildrensService _childrensService;

        public ChildrenController(ChildrensService childrensService)
        {
            _childrensService = childrensService;
        }

        // GET: api/Children/5
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetChild(string id)
        {
            var children = await _childrensService.ListAsync();
            foreach (var child in children)
            {
                if (id == child.Name)
                    child.IsSelected = true;
                else
                    child.IsSelected = false;

                await _childrensService.UpdateAsync(child, child.Id);
            }

            return "Success";
        }
    }
}
