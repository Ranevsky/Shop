namespace Shop.Exceptions;

public sealed class FileIsNotImageException : BadRequestException
{
    public FileIsNotImageException(string fileName)
        : base($"File '{fileName}' is not image")
    {

    }
}
