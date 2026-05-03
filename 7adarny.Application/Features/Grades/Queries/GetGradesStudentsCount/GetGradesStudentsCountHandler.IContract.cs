using _7adarny.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.Features.Grades.Queries.GetGradesStudentsCount
{
    public interface IGetGradesStudentsCountHandlerContract<Input,Output>:IBusinessHandler
    {
        Task<Output> HandleAsync(Input request, CancellationToken cancellationToken);
    }
}
