namespace Shop.Exceptions;

public class UploadFileIsEmptyException : BadRequestException
{
    public UploadFileIsEmptyException()
        : base("Upload file is empty")
    {

    }
}
