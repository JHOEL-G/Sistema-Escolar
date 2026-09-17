using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Sistema_Escolar.Infrastructure.Repositories
{
    public class ValoresJson
    {
        public static string? GetStringOrNull(Dictionary<string, JsonElement> json, string key)
        {
            return json.ContainsKey(key) && json[key].ValueKind != JsonValueKind.Null
                ? json[key].GetString()
                : null;
        }

        public static bool? GetBoolOrNull(Dictionary<string, JsonElement> json, string key)
        {
            return json.ContainsKey(key) && json[key].ValueKind != JsonValueKind.Null
                ? json[key].GetBoolean()
                : null;
        }

        public static int? GetIntOrNull(Dictionary<string, JsonElement> json, string key)
        {
            return json.ContainsKey(key) && json[key].ValueKind != JsonValueKind.Null
                ? json[key].GetInt32()
                : null;
        }

        public static decimal? GetDecimalOrNull(Dictionary<string, JsonElement> json, string key)
        {
            return json.ContainsKey(key) && json[key].ValueKind != JsonValueKind.Null
                ? json[key].GetDecimal()
                : null;
        }

        public static DateTime? GetDateTimeOrNull(Dictionary<string, JsonElement> json, string key)
        {
            return json.ContainsKey(key) && json[key].ValueKind != JsonValueKind.Null
                ? json[key].GetDateTime()
                : null;
        }
    }
}
