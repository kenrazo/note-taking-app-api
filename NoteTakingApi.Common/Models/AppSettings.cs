using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteTakingApi.Common.Models
{
    public class AppSettings
    {
        public string SaltHC { get; set; }
        public JwtSettings JwtSettings { get; set; }
    }
}
