using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Configuration
{
    public class Settings
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public string JwtKey { get; set; }
        public string AdminUserName { get; set; }
        public string AdminPassword { get; set; }
        public string ShouldSetupAdminUser { get; set; }
    }
}