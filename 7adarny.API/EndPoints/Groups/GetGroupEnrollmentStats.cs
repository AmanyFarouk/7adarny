using _7adarny.Application.Features.Groupss.Queries.GetGroupEnrollmentStats;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Reflection.Metadata;

namespace _7adarny.API.EndPoints.Groups
{
    public class GetGroupEnrollmentStatsEndpoint : EndpointBaseAsync
    .WithRequest<GetGroupEnrollmentStatsRequest>
    .WithActionResult<GetGroupEnrollmentStatsResponse>
    {
        private readonly IGetGroupEnrollmentStatsHandlerContract<GetGroupEnrollmentStatsHandlerInput,GetGroupEnrollmentStatsHandlerOutput> _handler;
        private readonly IMapper _mapper;

        public GetGroupEnrollmentStatsEndpoint(IGetGroupEnrollmentStatsHandlerContract<GetGroupEnrollmentStatsHandlerInput,GetGroupEnrollmentStatsHandlerOutput> handler
            ,IMapper mapper)
        {
            _handler = handler;
            _mapper = mapper;
        }
        //[Authorize(Roles = "Teacher")]
        [HttpGet(GetGroupEnrollmentStatsRequest.Route)]
        [SwaggerOperation(Summary = "Get Group Enrollment Stats", Description = "Get num of enrollments for the group in every day in the week", OperationId = "Groups.GetGroupEnrollmentStats", Tags = new[] { "Groups" })]
        [Produces("application/json")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(GetGroupEnrollmentStatsResponse))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(string))]
        public override async Task<ActionResult<GetGroupEnrollmentStatsResponse>> HandleAsync(GetGroupEnrollmentStatsRequest request, CancellationToken cancellationToken = default)
        {
            //var teacherId = int.Parse(
            //User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var teacherId = 1;

            var input = _mapper.Map<GetGroupEnrollmentStatsHandlerInput>(request);
            input.TeacherId = teacherId;

            var output = await _handler.HandleAsync(input, cancellationToken);

            if (!output.DailyStats.Any())
                return NotFound("No enrollment data found for this group");

            var response = _mapper.Map<GetGroupEnrollmentStatsResponse>(output);
            return Ok(response);
        }
    }
}
