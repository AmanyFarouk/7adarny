using _7adarny.Application.Features.Groupss.Queries.GetGroupsCompletion;
using _7adarny.Application.Features.Teachers.Queries.GetTeacherProfile;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace _7adarny.API.EndPoints.Groups
{
    public class GetGroupsCompletionEndpoint:EndpointBaseAsync
        .WithRequest<GetGroupsCompletionRequest>
        .WithActionResult<GetGroupsCompletionResponse>
    {
        private readonly IGetGroupsCompletionHandlerContract<GetGroupsCompletionHandlerInput, GetGroupsCompletionHandlerOutput> _handler;
        private readonly IMapper _mapper;
        public GetGroupsCompletionEndpoint(IGetGroupsCompletionHandlerContract<GetGroupsCompletionHandlerInput,GetGroupsCompletionHandlerOutput> handler
            ,IMapper mapper)
        {
            _handler=handler;
            _mapper=mapper;
        }

        //[Authorize(Roles = "Teacher")]
        [HttpGet(GetGroupsCompletionRequest.Route)]
        [SwaggerOperation(Summary = "Get Groups Completion", Description = "Get groups details and completion persentage for each group", OperationId = "Groups.GetGroupsCompletion", Tags = new[] { "Groups" })]
        [Produces("application/json")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(GetGroupsCompletionResponse))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(string))]
        public override async Task<ActionResult<GetGroupsCompletionResponse>> HandleAsync(GetGroupsCompletionRequest request, CancellationToken cancellationToken = default)
        {
            //var teacherId = int.Parse(
            //User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var teacherId = 1;

            var input=_mapper.Map<GetGroupsCompletionHandlerInput>(request);
            input.TeacherId=teacherId;
            
            var output=await _handler.HandleAsync(input, cancellationToken);

            if (!output.Groups.Any())
                return NotFound("No groups found");

            var response = _mapper.Map<GetGroupsCompletionResponse>(output);

            return Ok(response);
        }
    }
}
