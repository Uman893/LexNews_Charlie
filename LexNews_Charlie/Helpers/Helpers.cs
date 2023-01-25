using Newtonsoft.Json;

namespace LexNews_Charlie.Helpers
{
    public static class Helpers
    {
        public static void Set<T>(this ISession session, string key, T Value)
        {
            session.SetString(key, JsonConvert.SerializeObject(Value));
        }
        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value); // ? = turnery operator. <T> stands for every class in ur program,
                                                                                      // Called for generic datatype.
        }
    }    
}

