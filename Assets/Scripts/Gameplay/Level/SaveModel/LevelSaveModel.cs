using Newtonsoft.Json;

namespace Gameplay
{
    [System.Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class LevelSaveModel
    {
        [JsonProperty("p")]
        public string platformSaveModelJson = null;
        [System.NonSerialized] public PlatformLevelSaveModel platformSaveModel = null;

        [JsonProperty("g")]
        public string gatePointSaveModelJson = null;
        [System.NonSerialized] public GatePointLevelSaveModel gatePointSaveModel = null;

        [JsonProperty("c")]
        public string collectibleItemSaveModelJson = null;
        [System.NonSerialized] public CollectibleItemLevelSaveModel collectibleItemSaveModel = null;
    }
}