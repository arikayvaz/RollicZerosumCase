using Newtonsoft.Json;

namespace Gameplay
{
    [System.Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class LevelSaveModelBase<T>
    {
        [JsonProperty("i")]
        public string infosJson = null;

        public T[] GetLevelInfos() 
        {
            if (infosJson == null)
                return null;

            return JsonConvert.DeserializeObject<T[]>(infosJson);
        }
    }
}