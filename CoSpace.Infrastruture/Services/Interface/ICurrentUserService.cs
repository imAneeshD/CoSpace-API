using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSpace.Infrastruture.Services.Interface
{
    public interface ICurrentUserService
    {
        int UserId { get; }
        int OrgId { get; }
        int AppAdmin { get; }
        int Role { get; }
    }
}
