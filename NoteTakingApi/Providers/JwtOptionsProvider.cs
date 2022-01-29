using Microsoft.Extensions.Options;
using NoteTakingApi.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteTakingApi.Providers
{
    public class JwtOptionsProvider : IJwtOptionsProvider
    {
        private readonly JwtOptions _jwtOptions;

        public JwtOptionsProvider(IOptions<AppSettings> options)
        {
            var secret = options.Value.JwtSettings.Secret;
            var expires = options.Value.JwtSettings.Expires;
            _jwtOptions = new JwtOptions(secret, expires);
        }

        public JwtOptions GetJwtOptions()
        {
            return _jwtOptions;
        }
    }
}
