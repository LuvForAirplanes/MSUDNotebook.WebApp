﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MSUDTrack.DataModels.Models;
using MSUDTrack.Services;
using MSUDTrack.Services.DTOs;

namespace MSUDTrack.WebApp.Pages
{
    [Authorize]
    public class TodayModel : PageModel
    {
        private readonly RecordsService _recordsService;
        private readonly PeriodsService _periodsService;
        private readonly FoodsService _foodsService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ChildrensService childrensService;

        public TodayModel(RecordsService recordsService, PeriodsService periodsService, UserManager<ApplicationUser> userManager, FoodsService foodsService, ChildrensService childrensService)
        {
            _recordsService = recordsService;
            _periodsService = periodsService;
            _userManager = userManager;
            _foodsService = foodsService;
            this.childrensService = childrensService;
        }

        public TodayDTO TodaysLog { get; set; } = new TodayDTO();

        [BindProperty]
        public NewFood Food { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (string.IsNullOrEmpty(user.ChildId))
                return RedirectToPage("/Settings");

            ModelState.Clear();
            await LoadData();

            return Page();
        }

        public async Task<IActionResult> OnPostAddRecordAsync(string periodId, string childId)
        {
            await _recordsService.CreateAsync(new Record()
            {
                Id = Guid.NewGuid().ToString(),
                ChildId = childId,
                PeriodId = periodId,
                Created = DateTime.Now
            });

            await LoadData();
            ModelState.Clear();
            return RedirectToPage("/Today");
        }

        public async Task<IActionResult> OnPostNewFoodAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var currentChild = childrensService.Get().Where(c => c.Id == user.ChildId).FirstOrDefault();

            Food.Name = Food.Name.Transform(To.TitleCase);

            await _foodsService.CreateAsync(new Food()
            {
                Created = DateTime.Now,
                Id = System.Guid.NewGuid().ToString(),
                Name = Food.Name.Transform(To.TitleCase),
                ProteinGrams = Food.ProteinGrams,
                Updated = DateTime.Now,
                WeightGrams = Food.ServingGrams,
                LastUsed = DateTime.Now,
                TimesUsed = 1,
                Manufacturer = ""
            });

            var record = await _recordsService.GetByIdAsync(Food.RecordId);

            var newRecord = new Record()
            {
                Id = record.Id,
                ChildId = record.ChildId,
                Created = record.Created,
                Name = Food.Name,
                PeriodId = record.PeriodId,
                ProteinGrams = Food.ProteinGrams,
                Updated = DateTime.Now,
                WeightGrams = Food.ServingGrams
            };

            await _recordsService.UpdateAsync(newRecord, user);

            await LoadData();
            ModelState.Clear();
            return RedirectToPage("/Today");
        }

        public async Task<IActionResult> OnPostDeleteRecordAsync(string recordId)
        {
            await _recordsService.DeleteAsync(recordId);

            await LoadData();
            ModelState.Clear();
            return RedirectToPage("/Today");
        }

        public async Task LoadData()
        {
            TodaysLog = await _recordsService.GetTodaysRecordsByCurrentChildAsync(await _userManager.GetUserAsync(User));
        }
    }

    public class NewFood
    {
        public string RecordId { get; set; }

        public string Name { get; set; }

        public double ProteinGrams { get; set; }

        public double ServingGrams { get; set; }
    }
}
