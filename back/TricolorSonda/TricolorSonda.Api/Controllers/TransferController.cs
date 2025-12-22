using Microsoft.AspNetCore.Mvc;
using TricolorSonda.Api.Dtos;
using TricolorSonda.Api.Services;

namespace TricolorSonda.Api.Controllers
{
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly TransferService _transferService;

        public TransferController(TransferService transferService)
        {
            _transferService = transferService;
        }

        [HttpPost("transfers")]
        public async Task<IActionResult> Post([FromBody] CreateTransfer createTransfer)
        {
            var response = await _transferService.Create(createTransfer);
            return Ok(response);
        }

        [HttpGet("transfers:paginated")]
        public async Task<IActionResult> GetTransfersPaginated([FromQuery] GetPaginatedTransfer getPaginatedTransfer)
        {
            var response = await _transferService.GetPaginated(getPaginatedTransfer);
            return Ok(response);
        }
    }
}
