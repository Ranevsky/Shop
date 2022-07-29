namespace Shop.Extensions;

public static class StringExtension
{
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
