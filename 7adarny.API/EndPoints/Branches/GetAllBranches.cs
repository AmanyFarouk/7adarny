using _7adarny.API.EndPoints.Teachers;
using _7adarny.Application.Features.Branches.Queries.GetAllBranches;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace _7adarny.API.EndPoints.Branches
{
    public class GetAllBranchesEndPoint : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult<GetAllBranchesResponse>
    {
        private readonly IGetAllBranchesHandlerContract<GetAllBranchesHandlerInput, GetAllBranchesHandlerOutput> _handler;
        private readonly IMapper _mapper;

        public GetAllBranchesEndPoint(IGetAllBranchesHandlerContract<GetAllBranchesHandlerInput,GetAllBranchesHandlerOutput>handler
            ,IMapper mapper)
        {
            _handler = handler;
            _mapper = mapper;
        }

        //[Authorize(Roles = "Teacher")]
        [HttpGet(GetAllBranchesRequest.Route)]
        [SwaggerOperation(Summary = "Get All Branches", Description = "Get All Branches For Teacher", OperationId = "Branches.GetAllBranches", Tags = new[] { "Branches" })]
        [Produces("application/json")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(GetAllBranchesResponse))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(string))]
        public override async Task<ActionResult<GetAllBranchesResponse>> HandleAsync(CancellationToken cancellationToken = default)
        {
            //var teacherId = int.Parse(
            //User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var teacherId = 1;

            var input= new GetAllBranchesHandlerInput
            {
                TeacherId = teacherId
            };
            var output=await _handler.HandleAsync(input, cancellationToken);
            if (!output.Branches.Any())
            {
                return NotFound("No branches found");
            }
            var response = _mapper.Map<GetAllBranchesResponse>(output);
            return Ok(response);
        }
    }
}
