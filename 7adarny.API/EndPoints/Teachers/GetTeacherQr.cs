using _7adarny.Application.Features.Teachers.Queries.GetTeacherQr;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace _7adarny.API.EndPoints.Teachers
{
    public class GetTeacherQrEndPoint : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult<GetTeacherQrResponse>
    {
        private readonly IGetTeacherQrHandlerContract<GetTeacherQrHandlerInput, GetTeacherQrHandlerOutput> _handler;
        private readonly IMapper _mapper;

        public GetTeacherQrEndPoint(IGetTeacherQrHandlerContract<GetTeacherQrHandlerInput,GetTeacherQrHandlerOutput>handler
            ,IMapper mapper)
        {
            _handler = handler;
            _mapper = mapper;
        }
        //[Authorize(Roles ="Teacher")]
        [HttpGet(GetTeacherQrRequest.Route)]
        [SwaggerOperation(Summary = "Get Teacher Qr", Description = "Get Teacher Qr Image and teacher details : teacher name , subject name , description", OperationId = "Teachers.GetTeacherQr", Tags = new[] { "Teachers" })]
        [Produces("application/json")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(GetTeacherQrResponse))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(string))]
        public override async Task<ActionResult<GetTeacherQrResponse>> HandleAsync(CancellationToken cancellationToken = default)
        {
            //var teacherId = int.Parse(
            //User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var teacherId = 1;

            var input=new GetTeacherQrHandlerInput { TeacherId = teacherId };

            var output=await _handler.HandleAsync(input, cancellationToken);

            if (output.TeacherQr is null)
                return NotFound("Teacher not found");

            var response = _mapper.Map<GetTeacherQrResponse>(output);
            return Ok(response);
        }
    }
}
