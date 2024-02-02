using Newtonsoft.Json;
using UnityEngine;

namespace Gameplay
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CollectibleItemLevelInfoModel
    {
        [JsonProperty("x")]
        public float x;

        [JsonProperty("z")]
        public float z;

        [JsonProperty("t")]
        public CollectibleItemTypes type = CollectibleItemTypes.None;

        public Vector3 Position => new Vector3(x, 0f, z);

        //For serialization
        public CollectibleItemLevelInfoModel()
        {
            
        }

        public CollectibleItemLevelInfoModel(CollectibleItemTypes type, float posX, float posZ)
        {
            this.type = type;
            x = posX;
            z = posZ;
        }
    }
}