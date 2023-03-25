using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Core
{
    public static class Constants
    {
        public struct Configuration
        {
            public const string CloudinarySection = "CloudinarySetting";
        }

        public struct Routes
        {
            public const string ApiVersion = "1.0";
            public const string BasePrefix = "api/v{version: ApiVersion}";
            public const string Prefix = BasePrefix + "/[controller]";


            public struct User
            {
                public const string Login = nameof(User) + nameof(Login);
            }
        }
    }
}