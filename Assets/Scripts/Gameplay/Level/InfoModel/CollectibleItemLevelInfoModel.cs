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

        [JsonProperty("rx")]
        public float rotX;

        [JsonProperty("ry")]
        public float rotY;

        [JsonProperty("rz")]
        public float rotZ;

        public Vector3 Position => new Vector3(x, 0f, z);

        public Vector3 RotationEuler => new Vector3(rotX, rotY, rotZ);
        public Quaternion Rotation => Quaternion.Euler(rotX, rotY, rotZ);

        //For serialization
        public CollectibleItemLevelInfoModel()
        {
            
        }

        public CollectibleItemLevelInfoModel(CollectibleItemTypes type, Vector3 position, Vector3 rotEuler)
        {
            this.type = type;

            x = position.x;
            z = position.z;

            rotX = rotEuler.x;
            rotY = rotEuler.y;
            rotZ = rotEuler.z;
        }
    }
}