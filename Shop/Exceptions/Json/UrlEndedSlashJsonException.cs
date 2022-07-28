namespace Shop.Exceptions.Json;

public class UrlEndedSlashJsonException : EndedSlashJsonException
{
    private const string _url = "Url";
    public UrlEndedSlashJsonException(char slash) : base(_url, slash)
    {

    }
    public UrlEndedSlashJsonException(string key, string value, char slash) : base(_url, key, value, slash)
    {

    }
}
