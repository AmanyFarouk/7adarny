using _7adarny.Application.Features.Groupss.Queries.GetTeacherGroupsSummary;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace _7adarny.API.EndPoints.Groups
{
    public class GetTeacherGroupsSummaryEndPoint : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult<GetTeacherGroupsSummaryResponse>
    {
        private readonly IGetTeacherGroupsSummaryHandlerContract<GetTeacherGroupsSummaryHandlerInput,GetTeacherGroupsSummaryHandlerOutput> _handler;
        private readonly IMapper _mapper;

        public GetTeacherGroupsSummaryEndPoint(IGetTeacherGroupsSummaryHandlerContract<GetTeacherGroupsSummaryHandlerInput,GetTeacherGroupsSummaryHandlerOutput>handler
            ,IMapper mapper)
        {
            _handler = handler;
            _mapper = mapper;
        }
        // [Authorize(Roles = "Teacher")]
        [HttpGet(GetTeacherGroupsSummaryRequest.Route)]
        [SwaggerOperation(Summary = "Get Teacher Groups Summary", Description = "Get Teacher Groups Summary :num of full groups , num of active groups , num of students , completion persentage for all groups", OperationId = "Groups.GetTeacherGroupsSummary", Tags = new[] { "Groups" })]
        [Produces("application/json")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(GetTeacherGroupsSummaryResponse))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(string))]
        public override async Task<ActionResult<GetTeacherGroupsSummaryResponse>> HandleAsync(CancellationToken cancellationToken = default)
        {
            //var teacherId = int.Parse(
            //User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var teacherId = 1;
            var input = new GetTeacherGroupsSummaryHandlerInput
            {
                TeacherId = teacherId
            };

            var output = await _handler.HandleAsync(input, cancellationToken);
            
            var response = _mapper.Map<GetTeacherGroupsSummaryResponse>(output);

            return Ok(response);
        }
    }
}
