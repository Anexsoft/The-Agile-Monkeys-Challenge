using System.Text.Json;

namespace CRM.Common.Mapper
{
    public static class Mapper
    {
        public static T MapTo<T>(this object value)
        {
            return JsonSerializer.Deserialize<T>(
                JsonSerializer.Serialize(value)
            );
        }
    }
}
