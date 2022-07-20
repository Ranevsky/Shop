namespace Shop.Exceptions;

public class FileIsNotImageException : BadRequestException
{
    public FileIsNotImageException(string fileName)
        : base($"File '{fileName}' is not image")
    {

    }
}
