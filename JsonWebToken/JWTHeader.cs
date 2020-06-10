using System;
using System.Collections.Generic;
using System.Text;

namespace JsonWebToken
{
    public class JWTHeader
    {
        public string alg { get; set; }
        public string typ { get; set; }

        public JWTHeader(string alg, string typ)
        {
            this.alg = alg;
            this.typ = typ;
        }

        public static JWTHeader GenerateDefault()
        {
            return new JWTHeader("HS256", "JWT");
        }
    }
}
