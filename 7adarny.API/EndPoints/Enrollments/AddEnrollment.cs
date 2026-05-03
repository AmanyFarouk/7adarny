using _7adarny.Application.Features.Enrollments.Commands.AddEnrollment;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Security.Claims;

namespace _7adarny.API.EndPoints.Enrollments
{
    public class AddEnrollmentEndPoint : EndpointBaseAsync
        .WithRequest<AddEnrollmentRequest>
        .WithActionResult<AddEnrollmentResponse>
    {
        private readonly IAddEnrollmentHandlerContract<AddEnrollmentHandlerInput, AddEnrollmentHandlerOutput> _handler;
        private readonly IMapper _mapper;

        public AddEnrollmentEndPoint(IAddEnrollmentHandlerContract<AddEnrollmentHandlerInput,AddEnrollmentHandlerOutput>handler
            ,IMapper mapper)
        {
            _handler = handler;
            _mapper = mapper;
        }
        //[Authorize(Roles ="Student")]
        [HttpPost(AddEnrollmentRequest.Route)]
        [SwaggerOperation(Summary = "Add Enrollment", Description = "Add enrollment for student", OperationId = "Enrollments.AddEnrollment", Tags = new[] { "Enrollments" })]
        [Produces("application/json")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(AddEnrollmentResponse))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        public override async Task<ActionResult<AddEnrollmentResponse>> HandleAsync([FromBody]AddEnrollmentRequest request, CancellationToken cancellationToken = default)
        {
            //var studentId = int.Parse(
            //User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var studentId = 2;
            var input = _mapper.Map<AddEnrollmentHandlerInput>(request);
            input.StudentId=studentId;
            var output = await _handler.HandleAsync(input, cancellationToken);
            var response=_mapper.Map<AddEnrollmentResponse>(output);
            if (!output.IsSuccess)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
