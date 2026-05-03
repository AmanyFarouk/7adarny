using _7adarny.Application.Features.Teachers.Queries.GetTeacherPublicProfile;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace _7adarny.API.EndPoints.Teachers
{
    public class GetTeacherPublicProfileEndPoint : EndpointBaseAsync
        .WithRequest<GetTeacherPublicProfileRequest>
        .WithActionResult<GetTeacherPublicProfileResponse>
    {
        private readonly IGetTeacherPublicProfileHandlerContract<GetTeacherPublicProfileHandlerInput,GetTeacherPublicProfileHandlerOutput> _handler;
        private readonly IMapper _mapper;
        public GetTeacherPublicProfileEndPoint(IGetTeacherPublicProfileHandlerContract<GetTeacherPublicProfileHandlerInput, GetTeacherPublicProfileHandlerOutput> handler
            ,IMapper mapper)
        {
            _handler = handler;
            _mapper = mapper;
        }
        //[Authorize(Roles ="Student")]
        [HttpGet(GetTeacherPublicProfileRequest.Route)]
        [SwaggerOperation(Summary = "Get Teacher public Profile", Description = "Get teacher public profile info", OperationId = "Teachers.GetTeacherPublicProfile", Tags = new[] { "Teachers" })]
        [Produces("application/json")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(GetTeacherPublicProfileResponse))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(string))]
        public override async Task<ActionResult<GetTeacherPublicProfileResponse>> HandleAsync(GetTeacherPublicProfileRequest request, CancellationToken cancellationToken = default)
        {
            var input = _mapper.Map<GetTeacherPublicProfileHandlerInput>(request);

            var output = await _handler.HandleAsync(input, cancellationToken);

            if (output.Profile is null)
                return NotFound("Teacher not found or not approved");

            var response = _mapper.Map<GetTeacherPublicProfileResponse>(output);
            return Ok(response);
        }
    }
}
