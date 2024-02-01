using Newtonsoft.Json;

namespace Gameplay
{
    [System.Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class LevelSaveModel
    {
        [JsonProperty("p")]
        public string platformSaveModelJson = null;
        public PlatformLevelSaveModel platformSaveModel = null;

        [JsonProperty("gp")]
        public string gatePointSaveModelJson = null;
        public GatePointLevelSaveModel gatePointSaveModel = null;
    }
}