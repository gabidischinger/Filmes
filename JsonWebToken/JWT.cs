using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace JsonWebToken
{
    public class JWT
    {
        public static string GenerateToken<TPayload>(
            TPayload payload,
            string secret,
            JWTHeader header = null
        )
        {
            var jsonHeader = header?.ToJson() ?? JWTHeader.GenerateDefault().ToJson();
            var jsonPayload = payload.ToJson();

            var encodedHeader = jsonHeader.ToBase64();
            var encodedPayload = jsonPayload.ToBase64();

            var content = encodedHeader + "." + encodedPayload;

            using HMACSHA256 hmac = new HMACSHA256(secret.ToByteArray());
            string signature = hmac.ComputeHash(content.ToByteArray()).ToBase64();
            return $"{encodedHeader}.{encodedPayload}.{signature}";

        }

        public static bool VerifyToken<TPayload>(
            string token,
            string secret,
            out TPayload payload,
            Action<Exception> onError = null
        )
            where TPayload : JWTPayload, IJWTPayloadBase
        {
            var pattern = @"Bearer: ([=\w_-]+).([=\w_-]+).([=\w_-]+)";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Match match = regex.Match(token);

            if (match.Success && match.Value == token)
            {
                string encodedHeader = match.Groups[1].Value;
                string encodedPayload = match.Groups[2].Value;
                string tokenSignature = match.Groups[3].Value;

                long now = DateTimeOffset.Now.ToUnixTimeSeconds();
                var content = encodedHeader + "." + encodedPayload;

                try
                {
                    using (HMACSHA256 hmac = new HMACSHA256(secret.ToByteArray()))
                    {
                        string signature = hmac.ComputeHash(content.ToByteArray()).ToBase64();
                        if (tokenSignature == signature)
                        {
                            payload = encodedPayload.FromBase64().FromJson<TPayload>();
                            if (payload.exp > now) return true;
                        }
                    }
                }
                catch (Exception ex) { if (onError != null) onError(ex); }
            }
            payload = null;
            return false;
        }
    }
}
