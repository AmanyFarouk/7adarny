using _7adarny.Application.Features.Students.Commands.StudentLogin;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace _7adarny.API.EndPoints.Students
{
    public class StudentLoginEndPoint : EndpointBaseAsync
        .WithRequest<StudentLoginRequest>
        .WithActionResult<StudentLoginResponse>
    {
        private readonly IStudentLoginHandlerContract<StudentLoginHandlerInput, StudentLoginHandlerOutput> _handler;
        private readonly IMapper _mapper;

        public StudentLoginEndPoint(IStudentLoginHandlerContract<StudentLoginHandlerInput,StudentLoginHandlerOutput>handler,
                                    IMapper mapper)
        {
            _handler = handler;
            _mapper = mapper;
        }
        [HttpPost(StudentLoginRequest.Route)]
        [SwaggerOperation(Summary = "Student Login",Description = "Login with name and phone — sends OTP if not verified",OperationId = "Auth.StudentLogin",Tags = new[] { "Auth" })]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(StudentLoginResponse))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(StudentLoginResponse))]
        public override async Task<ActionResult<StudentLoginResponse>> HandleAsync(StudentLoginRequest request, CancellationToken cancellationToken = default)
        {
            var input = _mapper.Map<StudentLoginHandlerInput>(request);
            var output = await _handler.HandleAsync(input, cancellationToken);
            var response = _mapper.Map<StudentLoginResponse>(output);

            if (!output.IsSuccess)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
