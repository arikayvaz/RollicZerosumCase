using Newtonsoft.Json;
using UnityEngine;

namespace Gameplay
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CollectibleItemLevelSaveModel : LevelSaveModelBase<CollectibleItemLevelInfoModel>
    {

    }
}