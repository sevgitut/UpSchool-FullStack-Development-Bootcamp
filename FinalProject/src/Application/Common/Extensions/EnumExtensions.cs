using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Application.Common.Extensions;

public static class EnumExtensions
{
    public static string GetDisplayName(Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var displayAttribute = field?.GetCustomAttribute<DisplayAttribute>();

        return displayAttribute?.GetName() ?? value.ToString();
    }
}