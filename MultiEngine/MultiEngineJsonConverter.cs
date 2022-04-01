//using MultiEngine.Structures;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MultiEngine
//{
//    public class MultiEngineJsonConverter : JsonConverter
//    {
//        public override bool CanConvert(Type objectType)
//        {
//            return typeof(MCSettingsBase).IsAssignableFrom(objectType);
//        }

//        public override object ReadJson(JsonReader reader, Type objectType,
//            object existingValue, JsonSerializer serializer)
//        {
//            JObject jObject = JObject.Load(reader);
//            var type = (string)jObject["Type"];
//            MCSettingsBase target = null;
//            switch (type)
//            {
//                case nameof(MCNightmareSettings):
//                    target = new MCNightmareSettings();
//                    break;
//                case nameof(MCDistortionSettings):
//                    target = new MCDistortionSettings();
//                    break;
//                case nameof(MCFreezeSettings):
//                    target = new MCFreezeSettings();
//                    break;
//                case nameof(MCHellgenieSettings):
//                    target = new MCHellgenieSettings();
//                    break;
//                case nameof(MCPipeSettings):
//                    target = new MCPipeSettings();
//                    break;
//                case nameof(MCVectorSettings):
//                    target = new MCVectorSettings();
//                    break;
//                case nameof(MCClusterSettings):
//                    target = new MCClusterSettings();
//                    break;
//                default:
//                    throw new JsonReaderException("No matching type found for " + type);
//            }
            
//            serializer.Populate(jObject.CreateReader(), target);
//            return target;
//        }

//        public override void WriteJson(JsonWriter writer, object value,
//            JsonSerializer serializer)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
