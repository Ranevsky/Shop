namespace Shop.Exceptions.Json;

public class EndedSlashJsonException : JsonException
{
    public EndedSlashJsonException(string type, char slash) : base($"{type} not should ended '{slash}'")
    {

    }

    public EndedSlashJsonException(string type, string key, string value, char slash) : base($"{type} \"{key}\": \"{value}\" not should ended '{slash}'")
    {

    }
}
