using _7adarny.Application.Features.Students.Commands.VerifyOtp;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace _7adarny.API.EndPoints.Students
{
    public class VerifyOtpEndPoint : EndpointBaseAsync
        .WithRequest<VerifyOtpRequest>
        .WithActionResult<VerifyOtpResponse>
    {
        private readonly IVerifyOtpHandlerContract<VerifyOtpHandlerInput, VerifyOtpHandlerOutput> _handler;
        private readonly IMapper _mapper;

        public VerifyOtpEndPoint(IVerifyOtpHandlerContract<VerifyOtpHandlerInput,VerifyOtpHandlerOutput> handler,
                                 IMapper mapper)
        {
            _handler = handler;
            _mapper = mapper;
        }
        [HttpPost(VerifyOtpRequest.Route)]
        [SwaggerOperation(Summary = "Verify OTP",Description = "Verify OTP via Twilio and return JWT token",OperationId = "Auth.VerifyOtp",Tags = new[] { "Auth" })]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(VerifyOtpResponse))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(VerifyOtpResponse))]
        public override async Task<ActionResult<VerifyOtpResponse>> HandleAsync(VerifyOtpRequest request, CancellationToken cancellationToken = default)
        {
            var input = _mapper.Map<VerifyOtpHandlerInput>(request);
            var output = await _handler.HandleAsync(input, cancellationToken);
            var response = _mapper.Map<VerifyOtpResponse>(output);

            if (!output.IsSuccess)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
