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

        public RecordsController(RecordsService recordsService)
        {
            _recordsService = recordsService;
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
    }
}
