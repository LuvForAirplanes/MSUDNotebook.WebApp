using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MSUDTrack.DataModels.Models;
using MSUDTrack.Services;

namespace MSUDTrack.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private readonly RecordsService _recordsService;
        private readonly ChildrensService _childrensService;
        private readonly FoodsService _foodsService;
        private readonly UserManager<ApplicationUser> _userManager;

        public RecordsController(RecordsService recordsService, ChildrensService childrensService, FoodsService foodsService, UserManager<ApplicationUser> userManager)
        {
            _recordsService = recordsService;
            _childrensService = childrensService;
            _foodsService = foodsService;
            _userManager = userManager;
        }

        // POST: api/Records
        [HttpPost]
        public async Task<ActionResult<Record>> PostRecord(UpdatedRecord record)
        {
            if (string.IsNullOrEmpty(record.Id))
                return NotFound();

            //here the food to record mapping is done, the actual count conversions are done in the service
            var existing = await _recordsService.GetByIdAsync(record.Id);
            Record newRecord;
            if (!string.IsNullOrEmpty(record.FoodId))
            {
                var food = await _foodsService.GetByIdAsync(record.FoodId);

                newRecord = new Record()
                {
                    Id = record.Id,
                    ChildId = existing.ChildId,
                    Created = existing.Created,
                    LeucineMilligrams = food.LeucineMilligrams,
                    Name = food.Name,
                    PeriodId = existing.PeriodId,
                    ProteinGrams = food.ProteinGrams,
                    Updated = DateTime.Now,
                    WeightGrams = food.WeightGrams
                };

            }
            else
            {
                newRecord = new Record()
                {
                    Id = record.Id,
                    ChildId = existing.ChildId,
                    Created = existing.Created,
                    LeucineMilligrams = record.LeucineMilligrams,
                    Name = existing.Name,
                    PeriodId = existing.PeriodId,
                    ProteinGrams = record.ProteinGrams,
                    Updated = DateTime.Now,
                    WeightGrams = record.WeightGrams
                };
            }

            var user = await _userManager.GetUserAsync(User);
            var updated = await _recordsService.UpdateAsync(newRecord);
            var child = _childrensService.GetCurrentChild();
            var leucineTotal = _recordsService.Get().Where(r => r.Created.Date == user.CurrentView.Date).Sum(r => r.LeucineMilligrams).Value;

            return new ReturnRecord()
            {
                ChildId = updated.ChildId,
                Created = updated.Created,
                Id = updated.Id,
                LeucineLeft = child.LeucineDailyCount - leucineTotal,
                LeucineMilligrams = updated.LeucineMilligrams,
                LuecineCount = leucineTotal,
                Name = updated.Name,
                PeriodId = updated.PeriodId,
                ProteinGrams = updated.ProteinGrams,
                Updated = updated.Updated,
                WeightGrams = updated.WeightGrams
            };
        }

        // DELETE: api/Foods/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Record>> DeleteRecord(string id)
        {
            var recordToDelete = await _recordsService.GetByIdAsync(id);

            await _recordsService.DeleteAsync(id);

            return recordToDelete;
        }

        // PUT: api/Foods/5
        [HttpPut("{periodId}")]
        public async Task<ActionResult<Record>> PutRecord(string periodId)
        {
            return await _recordsService.CreateAsync(new Record()
            {
                Id = Guid.NewGuid().ToString(),
                PeriodId = periodId,
                ChildId = _childrensService.GetCurrentChild().Id,
                Created = DateTime.Now
            });
        }
    }

    public class UpdatedRecord
    {
        public string Id { get; set; }

        public string FoodId { get; set; }

        public double? ProteinGrams { get; set; }

        public double? LeucineMilligrams { get; set; }

        public double? WeightGrams { get; set; }
    }

    public class ReturnRecord : Record
    {
        public double LuecineCount { get; set; }

        public double LeucineLeft { get; set; }
    }
}
