namespace Shop.Exceptions;

public sealed class UploadFileIsEmptyException : BadRequestException
{
    public UploadFileIsEmptyException()
        : base("Upload file is empty")
    {

    }
}
