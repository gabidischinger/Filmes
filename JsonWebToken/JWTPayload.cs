using System;
using System.Collections.Generic;
using System.Text;

namespace JsonWebToken
{
    public class JWTPayload : IJWTPayloadBase
    {
        public long exp { get; set; }
        public string uid { get; set; }
        public string uname { get; set; }
        public string role { get; set; }
    }
}
