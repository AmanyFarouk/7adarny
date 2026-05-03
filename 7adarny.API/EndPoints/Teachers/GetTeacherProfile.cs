using _7adarny.API.EndPoints.Students;
using _7adarny.Application.Features.Teachers.Queries.GetTeacherProfile;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace _7adarny.API.EndPoints.Teachers
{
    public class GetTeacherProfileEndpoint : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult<GetTeacherProfileResponse>
    {
        private readonly IGetTeacherProfileHandlerContract<GetTeacherProfileHandlerInput, GetTeacherProfileHandlerOutput> _handler;
        private readonly IMapper _mapper;
        public GetTeacherProfileEndpoint(IGetTeacherProfileHandlerContract<GetTeacherProfileHandlerInput,GetTeacherProfileHandlerOutput> handler
            ,IMapper mapper)
        {
            _handler = handler;
            _mapper = mapper;
        }

        //[Authorize(Roles = "Teacher")]

        [HttpGet(GetTeacherProfileRequest.Route)]
        [SwaggerOperation(Summary = "Get Teacher Profile", Description = "Get teacher details for profile info", OperationId = "Teachers.GetTeacherProfile", Tags = new[] { "Teachers" })]
        [Produces("application/json")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(GetTeacherProfileResponse))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(string))]
        public override async Task<ActionResult<GetTeacherProfileResponse>> HandleAsync( CancellationToken cancellationToken = default)
        {
            //var teacherId = int.Parse(
            //User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var teacherId = 1;

            var input=new GetTeacherProfileHandlerInput { TeacherId=teacherId};

            var output=await _handler.HandleAsync(input, cancellationToken);

            if (output.Teacher is null)
            {
                return NotFound("Teacher not found");
            }
            var response = _mapper.Map<GetTeacherProfileResponse>(output);

            return Ok(response);
        }
    }
}
