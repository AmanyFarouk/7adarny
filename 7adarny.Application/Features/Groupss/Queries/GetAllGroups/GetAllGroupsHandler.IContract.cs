using _7adarny.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.Features.Groupss.Queries.GetAllGroups
{
    public interface IGetAllGroupsHandlerContract<Input,Output>:IBusinessHandler
    {
        Task<Output> HandleAsync(Input input, CancellationToken cancellationToken);
    }
}
