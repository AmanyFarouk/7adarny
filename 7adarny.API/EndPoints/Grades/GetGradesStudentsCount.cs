using _7adarny.Application.Features.Grades.Queries.GetGradesStudentsCount;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace _7adarny.API.EndPoints.Grades
{
    public class GetGradesStudentsCountEndPoint : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult<GetGradesStudentsCountResponse>
    {
        private readonly IGetGradesStudentsCountHandlerContract<GetGradesStudentsCountHandlerInput,GetGradesStudentsCountHandlerOutput> _handler;
        private readonly IMapper _mapper;
        public GetGradesStudentsCountEndPoint(IGetGradesStudentsCountHandlerContract<GetGradesStudentsCountHandlerInput,GetGradesStudentsCountHandlerOutput> handler
            ,IMapper mapper)
        {
            _handler = handler;
            _mapper = mapper;
        }
        //[Authorize(Roles = "Teacher")]]
        [HttpGet(GetGradesStudentsCountRequest.Route)]
        [SwaggerOperation(Summary = "Get Grades Students Count", Description = "Get num of students in each grade", OperationId = "Grades.GetGradesStudentsCount", Tags = new[] { "Grades" })]
        [Produces("application/json")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(GetGradesStudentsCountResponse))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(string))]
        public override async Task<ActionResult<GetGradesStudentsCountResponse>> HandleAsync(CancellationToken cancellationToken = default)
        {
            //var teacherId = int.Parse(
            //User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var teacherId = 1;

            var input = new GetGradesStudentsCountHandlerInput
            {
                TeacherId = teacherId
            };

            var output = await _handler.HandleAsync(input, cancellationToken);

            if (!output.Grades.Any())
                return NotFound("No grades found");

            var response = _mapper.Map<GetGradesStudentsCountResponse>(output);
            return Ok(response);
        }
    }
}
