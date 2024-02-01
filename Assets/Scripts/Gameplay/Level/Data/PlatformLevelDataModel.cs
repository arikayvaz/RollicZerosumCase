using Newtonsoft.Json;

namespace Gameplay
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PlatformLevelDataModel
    {
        [JsonProperty("i")]
        public string platformInfosJson = null;

        public PlatformLevelInfoModel[] GetLevelInfos() 
        {
            if (platformInfosJson == null)
                return null;

            return JsonConvert.DeserializeObject<PlatformLevelInfoModel[]>(platformInfosJson);
        }
    }
}