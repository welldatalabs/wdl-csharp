using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace wdl.sdk.common.serialization
{
    public static class SerializationExtensionMethods
    {
        public static T DeserializeJson<T>(this string json, JsonSerializerSettings settings = null) where T : class
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return null;
            }

            JsonSerializerSettings settingsInternal = settings ?? GetDefaultJsonSerializerSettings();
            return JsonConvert.DeserializeObject<T>(json, settingsInternal);
        }


        private static JsonSerializerSettings GetDefaultJsonSerializerSettings()
        {
            return new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }
    }
}
