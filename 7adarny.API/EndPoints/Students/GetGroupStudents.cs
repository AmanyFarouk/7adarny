using _7adarny.Application.Features.Students.Queries.GetGroupStudents;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Security.Claims;

namespace _7adarny.API.EndPoints.Students
{
    public class GetGroupStudentsEndpoint : EndpointBaseAsync
        .WithRequest<GetGroupStudentsRequest>
        .WithActionResult<GetGroupStudentsResponse>
    {
        private readonly IGetGroupStudentsHandlerContract<GetGroupStudentsHandlerInput, GetGroupStudentsHandlerOutput> _handler;
        private readonly IMapper _mapper;
        public GetGroupStudentsEndpoint(IGetGroupStudentsHandlerContract<GetGroupStudentsHandlerInput,GetGroupStudentsHandlerOutput>handler
            ,IMapper mapper)
        {
            _handler = handler;
            _mapper = mapper;
        }


        //[Authorize(Roles = "Teacher")]

        [HttpGet(GetGroupStudentsRequest.Route)]
        [SwaggerOperation(Summary = "Get Group Students", Description = "Get all students in this group", OperationId = "Students.GetGroupStudents", Tags = new[] { "Students" })]
        [Produces("application/json")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(GetGroupStudentsResponse))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(string))]
        public override async Task<ActionResult<GetGroupStudentsResponse>> HandleAsync([FromRoute]GetGroupStudentsRequest request, CancellationToken cancellationToken = default)
        {
            //var teacherId = int.Parse(
            //User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var teacherId = 1;

            var input = _mapper.Map<GetGroupStudentsHandlerInput>(request);
            input.TeacherId = teacherId;
            var output =await _handler.HandleAsync(input, cancellationToken);
            if (!output.Students.Any())
                return NotFound("No students found in this group");
            var response=_mapper.Map<GetGroupStudentsResponse>(output);
            return Ok(response);
        }
    }
}
