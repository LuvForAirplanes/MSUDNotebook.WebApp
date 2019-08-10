using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> userManager;

        public ChildrenController(ChildrensService childrensService, UserManager<ApplicationUser> userManager)
        {
            _childrensService = childrensService;
            this.userManager = userManager;
        }

        // GET: api/Children/5
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetChild(string id)
        {
            var children = await _childrensService.ListAsync();
            foreach (var child in children)
            {
                if (id == child.Name)
                {
                    var user = await userManager.GetUserAsync(User);
                    user.ChildId = child.Id;
                    await userManager.UpdateAsync(user);
                }
                //else
                //{
                //    var user = await userManager.GetUserAsync(User);
                //    user.ChildId = ;
                //    await userManager.UpdateAsync(user);
                //}

                //await _childrensService.UpdateAsync(child, child.Id);
            }

            return "Success";
        }
    }
}
