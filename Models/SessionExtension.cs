using System.Text.Json;

namespace TequilasRestaurant.Models
{
    public static class SessionExtensions
    { // save object into our seesion
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            // read object into our session
            var json = session.GetString(key);
            if (string.IsNullOrEmpty(json))
            {
                return default(T);
            }
            else
            {
                return JsonSerializer.Deserialize<T>(json);
            }
        }
    }
}
