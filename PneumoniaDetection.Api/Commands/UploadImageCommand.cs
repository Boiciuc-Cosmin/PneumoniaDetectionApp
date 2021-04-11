using MediatR;
using Newtonsoft.Json;
using PneumoniaDetection.Api.Commands.Utils;
using PneumoniaDetection.Api.Dtos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PneumoniaDetection.Api.Commands {
    public class UploadImageCommand : IRequest<UploadImageCommandResult> {

    }

    public class UploadImageCommandResult {
        private readonly string _message;
        private readonly string _defaultErrorMessage = "No information to be displayed";

        public UploadImageCommandResult(CommandResult result) {
            Result = result;
        }

        public UploadImageCommandResult(CommandResult result, string message) : this(result) {
            _message = message;
        }

        public CommandResult Result { get; }
        public string Message => JsonConvert.SerializeObject(new { error = _message ?? _defaultErrorMessage });
        public PredictionResultDto Data { get; set; }
    }

    public class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, UploadImageCommandResult> {
        public Task<UploadImageCommandResult> Handle(UploadImageCommand request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }
}
