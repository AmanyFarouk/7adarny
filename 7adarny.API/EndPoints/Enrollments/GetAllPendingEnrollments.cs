using _7adarny.API.EndPoints.Groups;
using _7adarny.Application.Features.Enrollments.Queries.GetAllEnrollments;
using _7adarny.Application.Features.Enrollments.Queries.GetAllPendingEnrollments;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace _7adarny.API.EndPoints.Enrollments
{
    public class GetAllPendingEnrollmentsEndpoint : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult<GetAllPendingEnrollmentsResponse>
    {
        private readonly IGetAllPendingEnrollmentsHandlerContract<GetAllPendingEnrollmentsHandlerInput, GetAllPendingEnrollmentsHandlerOutput> _handler;
        private readonly IMapper _mapper;
        public GetAllPendingEnrollmentsEndpoint(IGetAllPendingEnrollmentsHandlerContract<GetAllPendingEnrollmentsHandlerInput, GetAllPendingEnrollmentsHandlerOutput> handler
            , IMapper mapper)
        {
            _handler = handler;
            _mapper = mapper;
        }

        //[Authorize(Roles = "Teacher")]

        [HttpGet(GetAllPendingEnrollmentsRequest.Route)]
        [SwaggerOperation(Summary = "Get All Pending Enrollments", Description = "Get all pending enrollments details for the teacher", OperationId = "Enrollments.GetAllPendingEnrollments", Tags = new[] { "Enrollments" })]
        [Produces("application/json")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(GetAllPendingEnrollmentsResponse))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(string))]
        public override async Task<ActionResult<GetAllPendingEnrollmentsResponse>> HandleAsync(CancellationToken cancellationToken = default)
        {
            //var teacherId = int.Parse(
            //User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var teacherId = 2;

            var input =new GetAllPendingEnrollmentsHandlerInput() { TeacherId=teacherId};
            var output=await _handler.HandleAsync(input, cancellationToken);
            if (!output.Enrollments.Any())
                return NotFound("No enrollments found");
            var response=_mapper.Map<GetAllPendingEnrollmentsResponse>(output);
            return Ok(response);
        }
    }
}
