using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MSUDTrack.DataModels.Models;
using MSUDTrack.Services;

namespace MSUDTrack.WebApp.Pages
{
    public class SettingsModel : PageModel
    {
        private readonly ChildrensService _childrensService;

        public SettingsModel(ChildrensService childrensService)
        {
            _childrensService = childrensService;
        }

        [BindProperty]
        public List<Child> Children { get; set; } = new List<Child>();
        [BindProperty]
        public Child NewChild { get; set; } = new Child() { Id = Guid.NewGuid().ToString(), IsActive = true };

        public void OnGet()
        {
            Children = _childrensService.Get().ToList();
        }

        public void InitData()
        {
            ModelState.Clear();

            NewChild = new Child() { Id = Guid.NewGuid().ToString(), IsActive = true };
            Children = _childrensService.Get().ToList();
        }

        public async Task OnPostAddChildAsync()
        {
            await _childrensService.CreateAsync(NewChild);

            InitData();
        }

        public async Task OnPostDeleteChildAsync(string id)
        {
            await _childrensService.DeleteAsync(id);

            InitData();
        }
    }
}