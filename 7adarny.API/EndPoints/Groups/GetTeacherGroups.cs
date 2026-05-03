using _7adarny.Application.Features.Groupss.Queries.GetGroupsCompletion;
using _7adarny.Application.Features.Groupss.Queries.GetTeacherGroups;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace _7adarny.API.EndPoints.Groups
{
    public class GetTeacherGroupsEndPoint : EndpointBaseAsync
        .WithRequest<GetTeacherGroupsRequest>
        .WithActionResult<GetTeacherGroupsResponse>
    {
        private readonly IGetTeacherGroupsHandlerContract<GetTeacherGroupsHandlerInput, GetTeacherGroupsHandlerOutput> _handler;
        private readonly IMapper _mapper;
        public GetTeacherGroupsEndPoint(IGetTeacherGroupsHandlerContract<GetTeacherGroupsHandlerInput,GetTeacherGroupsHandlerOutput> handler
            ,IMapper mapper)
        {
            _handler = handler;
            _mapper = mapper;
        }

        //[Authorize(Roles = "Teacher")]
        [HttpGet(GetTeacherGroupsRequest.Route)]
        [SwaggerOperation(Summary = "Get Teacher Groups", Description = "Get details for all groups for this teacher", OperationId = "Groups.GetTeacherGroups", Tags = new[] { "Groups" })]
        [Produces("application/json")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(GetTeacherGroupsResponse))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(string))]
        public override async Task<ActionResult<GetTeacherGroupsResponse>> HandleAsync(GetTeacherGroupsRequest request, CancellationToken cancellationToken = default)
        {
            //var teacherId = int.Parse(
            //User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var teacherId = 1;

            var input= _mapper.Map<GetTeacherGroupsHandlerInput>(request);
            input.TeacherId = teacherId;

            var output=await _handler.HandleAsync(input, cancellationToken);

            if (!output.Groups.Any())
                return NotFound("No groups found");

            var response = _mapper.Map<GetTeacherGroupsResponse>(output);

            return Ok(response);
        }
    }
}
