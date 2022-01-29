using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteTakingApi.Common.Models
{
    public class JwtOptions
    {
        public JwtOptions(string secret, int expiresIn)
        {
            Secret = secret;
            ExpiresIn = expiresIn;
        }

        public string Secret { get; set; }

        public int ExpiresIn { get; set; }
    }
}
