using System.Text;

namespace Shop.Extensions;

public static class StringExtension
{
    public static string NormalFormat(this string text)
    {
        StringBuilder sb = new(text.ToLower());
        sb[0] = char.ToUpper(sb[0]);
        return sb.ToString();
    }
    public static void EndsSlashException(this string value, string key, Action<string, string> exception)
    {
        if (value.EndsSlash())
        {
            exception.Invoke(key, value);
        }
    }
    public static bool EndsSlash(this string value)
    {
        return value.EndsWith("/") || value.EndsWith("\\");
    }
}
