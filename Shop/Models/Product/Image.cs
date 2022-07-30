using System.Text.Json.Serialization;

namespace Shop.Models.Product;

public abstract class Image : IDelete
{
    public int Id { get; set; }
    /// <summary>
    /// Example: foo.png
    /// </summary>
    public string Name { get; set; } = null!;
    /// <summary>
    /// Example: 2
    /// </summary>
    public string Path { get; set; } = null!;
    public bool IsExists
    {
        get
        {
            if (!_isExists)
            {
                _isExists = File.Exists(FullPath);
            }
            return _isExists;
        }

    }
    private bool _isExists = false;

    [JsonIgnore]
    protected abstract string FullPath { get; }

    /// <summary>
    /// Image may be not exists, checking IsExist
    /// </summary>
    [JsonIgnore]
    public abstract string Url { get; }

    public void Delete()
    {
        if (!IsExists)
        {
            return;
        }

#warning Add logger
        File.Delete(FullPath);
        Console.WriteLine($"Delete image with id = {Id} and path: '{Path}/{Name}'");
    }
}