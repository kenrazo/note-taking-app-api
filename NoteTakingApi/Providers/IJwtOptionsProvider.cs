using NoteTakingApi.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteTakingApi.Providers
{
    public interface IJwtOptionsProvider
    {
        JwtOptions GetJwtOptions();
    }
}
