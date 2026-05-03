using _7adarny.API.EndPoints.Grades;
using _7adarny.Application.Features.Groupss.Queries.GetAllGroups;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace _7adarny.API.EndPoints.Groups
{
    public class GetAllGroupsEndpoint : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult<GetAllGroupsResponse>
    {
        private readonly IGetAllGroupsHandlerContract<GetAllGroupsHandlerInput, GetAllGroupsHandlerOutput> _handler;
        private readonly IMapper _mapper;

        public GetAllGroupsEndpoint(IGetAllGroupsHandlerContract<GetAllGroupsHandlerInput,GetAllGroupsHandlerOutput>handler
            ,IMapper mapper)
        {
            _handler = handler;
            _mapper = mapper;
        }
        //[Authorize(Roles = "Teacher")]
        [HttpGet(GetAllGroupsRequest.Route)]
        [SwaggerOperation(Summary = "Get All Groups", Description = "Get all groups for the teacher", OperationId = "Groups.GetAllGroups", Tags = new[] { "Groups" })]
        [Produces("application/json")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(GetAllGroupsResponse))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(string))]
        public override async Task<ActionResult<GetAllGroupsResponse>> HandleAsync(CancellationToken cancellationToken = default)
        {
            //var teacherId = int.Parse(
            //User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var teacherId = 1;

            var input= new GetAllGroupsHandlerInput
            {
                TeacherId = teacherId
            };

            var output =await _handler.HandleAsync(input, cancellationToken);

           if (!output.Groups.Any())
           {
                return NotFound("No groups found");
           }

           var response = _mapper.Map<GetAllGroupsResponse>(output);
            return Ok(response);
        }
    }
}
