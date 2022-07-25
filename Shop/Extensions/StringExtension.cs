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
}
