using AutoMapper;

namespace Shop.Models.Converters;

public class PathToUrlConverter : IValueConverter<string, string>
{
    public string Convert(string sourceMember, ResolutionContext context)
    {
        return $"{Program.applicationUrl}{Program.ImageUrl}/{sourceMember}";
    }
}