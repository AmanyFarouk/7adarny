using _7adarny.API.EndPoints.Teachers;
using _7adarny.Application.Features.Grades.Queries.GetAllGrades;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace _7adarny.API.EndPoints.Grades
{
    public class GetAllGradesEndPoint:EndpointBaseAsync
        .WithoutRequest
        .WithActionResult<GetAllGradesResponse>
    {
        private readonly IGetAllGradesHandlerContract<GetAllGradesHandlerInput, GetAllGradesHandlerOutput> _handler;
        public readonly IMapper _mapper;

        public GetAllGradesEndPoint(IGetAllGradesHandlerContract<GetAllGradesHandlerInput,GetAllGradesHandlerOutput>handler
            ,IMapper mapper)
        {
            _handler = handler;
            _mapper = mapper;
        }
        //[Authorize(Roles = "Teacher")]]
        [HttpGet(GetAllGradesRequest.Route)]
        [SwaggerOperation(Summary = "Get All Grades", Description = "Get All Grades That Teaches Teachs For them", OperationId = "Grades.GetAllGrades", Tags = new[] { "Grades" })]
        [Produces("application/json")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(GetAllGradesResponse))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(string))]
        public override async Task<ActionResult<GetAllGradesResponse>> HandleAsync(CancellationToken cancellationToken = default)
        {
            //var teacherId = int.Parse(
            //User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var teacherId = 1;

            var input= new GetAllGradesHandlerInput{TeacherId = teacherId};

            var output =await _handler.HandleAsync(input, cancellationToken);

            if(!output.Grades.Any())
            {
                return NotFound("No grades found");
            }
            var response = _mapper.Map<GetAllGradesResponse>(output);
            return Ok(response);
        }
    }
}
