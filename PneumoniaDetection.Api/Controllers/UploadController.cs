using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PneumoniaDetection.Api.Commands;
using PneumoniaDetection.Api.Commands.Utils;
using System.Linq;
using System.Threading.Tasks;

namespace PneumoniaDetection.Api.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase {
        private readonly IMediator _mediator;

        public UploadController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UploadImage(IFormFile file) {
            if (!HttpContext.Request.Form.Files.Any()) {
                return BadRequest();
            }

            var result = await _mediator.Send(new UploadImageCommand());

            return result.Result switch {
                CommandResult.Succes => Ok(result.Data),
                CommandResult.InternalError => Conflict(result.Message),
                CommandResult.ValidationError => BadRequest(result.Message),
                _ => StatusCode(StatusCodes.Status500InternalServerError)
            };
        }
    }
}
