using System;
using System.Text;

namespace JsonWebToken
{
    public static class ExtensionMethods
    {
        public static byte[] ToByteArray(this string text)
        {
            return Encoding.UTF8.GetBytes(text);
        }

        public static string ToBase64(this string text)
        {
            return Encoding.UTF8.GetBytes(text).ToBase64();
        }

        public static string FromBase64(this string text)
        {
            text = text.Replace('-', '+');
            text = text.Replace('_', '/');
            switch (text.Length % 4)
            {
                case 0: break;
                case 2: text += "=="; break;
                case 3: text += "="; break;
                default:
                    throw new System.Exception(
             "Illegal base64url string!");
            }
            return Encoding.UTF8.GetString(Convert.FromBase64String(text));
        }

        public static string ToBase64(this byte[] bytes)
        {
            var base64 = Convert.ToBase64String(bytes);
            base64 = base64.Split('=')[0];
            base64 = base64.Replace('+', '-');
            base64 = base64.Replace('/', '_');
            return base64;
        }

        public static string ToJson<T>(this T o)
        {
            return System.Text.Json.JsonSerializer.Serialize<T>(o);
        }

        public static T FromJson<T>(this string json)
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(json);
        }
    }
}
