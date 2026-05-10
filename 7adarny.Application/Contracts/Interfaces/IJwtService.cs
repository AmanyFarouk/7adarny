using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.Contracts.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(int userId, string role, string name);
    }
}
