using _7adarny.Application.Features.Enrollments.Queries.GetEnrollmentById;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace _7adarny.API.EndPoints.Enrollments
{
    public class GetEnrollmentByIdEndPoint : EndpointBaseAsync
        .WithRequest<GetEnrollmentByIdRequest>
        .WithActionResult<GetEnrollmentByIdResponse>
    {
        private readonly IGetEnrollmentByIdHandlerContract<GetEnrollmentByIdHandlerInput, GetEnrollmentByIdHandlerOutput> _handler;
        private readonly IMapper _mapper;

        public GetEnrollmentByIdEndPoint(IGetEnrollmentByIdHandlerContract<GetEnrollmentByIdHandlerInput,GetEnrollmentByIdHandlerOutput> handler,
            IMapper mapper)
        {
            _handler = handler;
            _mapper = mapper;
        }
        //[Authorize(Roles = "Student")]

        [HttpGet(GetEnrollmentByIdRequest.Route)]
        [SwaggerOperation(Summary = "Get Enrollment By Id", Description = "Get enrollment details by id for the student", OperationId = "Enrollments.GetEnrollmentById", Tags = new[] { "Enrollments" })]
        [Produces("application/json")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(GetEnrollmentByIdResponse))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(string))]
        public override async Task<ActionResult<GetEnrollmentByIdResponse>> HandleAsync(GetEnrollmentByIdRequest request, CancellationToken cancellationToken = default)
        {
           var input= _mapper.Map<GetEnrollmentByIdHandlerInput>(request);
            var output =await _handler.HandleAsync(input, cancellationToken);
            if (output == null)
                return NotFound($"Enrollment with id {request.Id} not found.");
            var response = _mapper.Map<GetEnrollmentByIdResponse>(output);
            return response;

        }
    }
}
