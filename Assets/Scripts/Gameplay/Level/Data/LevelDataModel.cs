using Newtonsoft.Json;

namespace Gameplay
{
    [System.Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class LevelDataModel
    {
        [JsonProperty("p")]
        public string platformLevelDataModelJson = null;
        public PlatformLevelDataModel platformLevelDataModel = null;
    }
}