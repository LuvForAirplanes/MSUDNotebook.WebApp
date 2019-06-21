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

        public RecordsController(RecordsService recordsService, ChildrensService childrensService)
        {
            _recordsService = recordsService;
            _childrensService = childrensService;
        }

        // POST: api/Records
        [HttpPost]
        public async Task<ActionResult<Record>> PostRecord(Record record)
        {
            if(await _recordsService.GetByIdAsync(record.Id) != null)
            {
                var existing = await _recordsService.GetByIdAsync(record.Id);

                var newRecord = new Record()
                {
                    Id = record.Id,
                    ChildId = existing.ChildId,
                    Created = existing.Created,
                    FoodId = record.FoodId,
                    PeriodId = existing.PeriodId,
                    Updated = DateTime.Now
                };

                return await _recordsService.UpdateAsync(newRecord, newRecord.Id);
            }
            else
                return await _recordsService.CreateAsync(record);
        }

        // DELETE: api/Foods/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Record>> DeleteFood(string id)
        {
            var recordToDelete = await _recordsService.GetByIdAsync(id);

            await _recordsService.DeleteAsync(id);

            return recordToDelete;
        }

        // PUT: api/Foods/5
        [HttpPut("{periodId}")]
        public async Task<ActionResult<Record>> PutFood(string periodId)
        {
            return await _recordsService.CreateAsync(new Record()
            {
                Id = System.Guid.NewGuid().ToString(),
                PeriodId = periodId,
                ChildId = _childrensService.GetCurrentChild().Id,
                Created = DateTime.Now,
                FoodId = "defaultFood"
            });
        }
    }
}
