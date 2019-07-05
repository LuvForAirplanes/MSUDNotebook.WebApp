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

            //here the food to record mapping is done, the actual count conversions are done in the service
            var existing = await _recordsService.GetByIdAsync(record.Id);
            var food = await _foodsService.GetByIdAsync(record.FoodId);
            Record newRecord;
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

            return await _recordsService.UpdateAsync(newRecord);
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
}
