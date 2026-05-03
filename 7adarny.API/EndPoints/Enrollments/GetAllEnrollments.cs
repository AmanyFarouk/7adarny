using _7adarny.API.EndPoints.Groups;
using _7adarny.Application.Features.Enrollments.Queries.GetAllEnrollments;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace _7adarny.API.EndPoints.Enrollments
{
    public class GetAllEnrollmentsEndpoint : EndpointBaseAsync
        .WithRequest<GetAllEnrollmentsRequest>
        .WithActionResult<GetAllEnrollmentsResponse>
    {
        private readonly IGetAllEnrollmentsHandlerContract<GetAllEnrollmentsHandlerInput, GetAllEnrollmentsHandlerOutput> _handler;
        private readonly IMapper _mapper;
        public GetAllEnrollmentsEndpoint(IGetAllEnrollmentsHandlerContract<GetAllEnrollmentsHandlerInput, GetAllEnrollmentsHandlerOutput> handler
            , IMapper mapper)
        {
            _handler = handler;
            _mapper = mapper;
        }

        //[Authorize(Roles = "Teacher")]

        [HttpGet(GetAllEnrollmentsRequest.Route)]
        [SwaggerOperation(Summary = "Get All Enrollments", Description = "Get all students enrollments details for the teacher", OperationId = "Students.GetAllEnrollments", Tags = new[] { "Enrollments" })]
        [Produces("application/json")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(GetAllEnrollmentsResponse))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(string))]
        public override async Task<ActionResult<GetAllEnrollmentsResponse>> HandleAsync([FromQuery] GetAllEnrollmentsRequest request, CancellationToken cancellationToken = default)
        {
            //var teacherId = int.Parse(
            //User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var teacherId = 1;

            var input = _mapper.Map<GetAllEnrollmentsHandlerInput>(request);
            input.TeacherId = teacherId;
            var output=await _handler.HandleAsync(input, cancellationToken);
            if (!output.Enrollments.Any())
                return NotFound("No enrollments found");
            var response=_mapper.Map<GetAllEnrollmentsResponse>(output);
            return Ok(response);
        }
    }
}
