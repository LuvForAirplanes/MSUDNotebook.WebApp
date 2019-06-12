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
                return await _recordsService.UpdateAsync(record, record.Id);
            else
                return await _recordsService.CreateAsync(record);
        }
    }
}
