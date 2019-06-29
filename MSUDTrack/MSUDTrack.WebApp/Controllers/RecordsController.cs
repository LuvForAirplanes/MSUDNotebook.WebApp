using System;
using System.Threading.Tasks;
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

        public RecordsController(RecordsService recordsService, ChildrensService childrensService, FoodsService foodsService)
        {
            _recordsService = recordsService;
            _childrensService = childrensService;
            _foodsService = foodsService;
        }

        // POST: api/Records
        [HttpPost]
        public async Task<ActionResult<Record>> PostRecord(UpdatedRecord record)
        {
            if (string.IsNullOrEmpty(record.Id))
                return NotFound();

            var existing = await _recordsService.GetByIdAsync(record.Id);
            var food = await _foodsService.GetByIdAsync(record.FoodId);
            var newRecord = new Record();
            if (!string.IsNullOrEmpty(record.FoodId))
            {
                newRecord = new Record()
                {
                    Id = record.Id,
                    ChildId = existing.ChildId,
                    Created = existing.Created,
                    LeucineMilligrams = food.LeucineMilligrams,
                    Name = food.Name,
                    PeriodId = existing.PeriodId,
                    ProteinGrams = food.ProteinGrams,
                    Updated = DateTime.Now
                };

            }
            else
            {
                newRecord = new Record()
                {
                    Id = record.Id,
                    ChildId = existing.ChildId,
                    Created = existing.Created,
                    LeucineMilligrams = record.ProteinGrams * 100,
                    Name = existing.Name,
                    PeriodId = existing.PeriodId,
                    ProteinGrams = record.ProteinGrams,
                    Updated = DateTime.Now
                };

            }

            return await _recordsService.UpdateAsync(newRecord, newRecord.Id);
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
                Id = System.Guid.NewGuid().ToString(),
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

        public int? ProteinGrams { get; set; }

        public int? LeucineMilligrams { get; set; }
    }
}
