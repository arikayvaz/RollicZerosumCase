using Newtonsoft.Json;
using UnityEngine;

namespace Gameplay
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PlatformLevelInfoModel
    {
        [JsonProperty("z")]
        public float z;

        public Vector3 Position => new Vector3(0f, 0f, z);
    }
}