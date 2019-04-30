﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseProfiles
{
    public class JsonBooleanConverter : JsonConverter
    {
        public override bool CanWrite { get { return false; } }
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(Boolean))
            {
                return true;
            }
            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = reader.Value.ToString().ToLower().Trim();
            switch (value)
            {
                case "true":
                case "yes":
                case "y":
                case "1":
                    return true;
            }
            return false;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
