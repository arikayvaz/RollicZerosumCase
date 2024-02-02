using Newtonsoft.Json;
using UnityEngine;

namespace Gameplay
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GatePointLevelInfoModel
    {
        [JsonProperty("z")]
        public float z;

        [JsonProperty("v")]
        public int targetValue;

        public Vector3 Position => new Vector3(0f, 0f, z);

        //For serialization
        public GatePointLevelInfoModel()
        {
            
        }

        public GatePointLevelInfoModel(float posZ, int targetValue)
        {
            z = posZ;
            this.targetValue = targetValue;
        }
    }
}
