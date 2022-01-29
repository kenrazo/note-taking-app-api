using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteTakingApi.Common.Models
{
    public class JwtSettings
    {
        public string Secret { get; set; }

        public int Expires { get; set; }
    }
}
