namespace Shop.Exceptions.Json;

public class PathEndedSlashJsonException : EndedSlashJsonException
{
    private const string _path = "Path";
    public PathEndedSlashJsonException(char slash) : base(_path, slash)
    {

    }

    public PathEndedSlashJsonException(string key, string value, char slash) : base(_path, key, value, slash)
    {

    }
}
