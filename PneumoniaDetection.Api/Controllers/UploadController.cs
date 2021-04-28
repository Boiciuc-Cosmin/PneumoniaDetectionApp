using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PneumoniaDetection.Api.Commands;
using PneumoniaDetection.Api.Commands.Utils;
using System.Linq;
using System.Threading.Tasks;

namespace PneumoniaDetection.Api.Controllers {
    [ApiController]
    [Route("api/")]
    public class UploadController : ControllerBase {
        private readonly IMediator _mediator;
        private string lastImageAdded;

        public UploadController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpPost("upload")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file) {
            if (!HttpContext.Request.Form.Files.Any()) {
                return BadRequest();
            }
            file = HttpContext.Request.Form.Files.First();
            var result = await _mediator.Send(new UploadImageCommand(file));

            return result.Result switch {
                CommandResult.Succes => Ok(result.Data),
                CommandResult.InternalError => Conflict(result.Message),
                CommandResult.ValidationError => BadRequest(result.Message),
                _ => StatusCode(StatusCodes.Status500InternalServerError)
            };
        }

        [HttpPost("remove")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveImage([FromForm] string file) {
            if (string.IsNullOrEmpty(file)) {
                return BadRequest();
            }

            var result = await _mediator.Send(new RemoveImageCommand(file));

            return result.Result switch {
                CommandResult.Succes => Ok(),
                CommandResult.ValidationError => BadRequest(),
                CommandResult.InternalError => Conflict(),
                _ => StatusCode(StatusCodes.Status500InternalServerError)
            };
        }

        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddImage([FromForm] IFormFile file, [FromForm] string pneumonia, [FromForm] string normal) {
            if (!HttpContext.Request.Form.Files.Any()) {
                return BadRequest();
            }

            file = HttpContext.Request.Form.Files.First();

            var result = await _mediator.Send(new AddImageCommand(file, bool.Parse(pneumonia), bool.Parse(normal)));

            return result.Result switch {
                CommandResult.Succes => Ok(),
                CommandResult.ValidationError => BadRequest(),
                CommandResult.InternalError => Conflict(),
                _ => StatusCode(StatusCodes.Status500InternalServerError)
            };

        }
    }
}
