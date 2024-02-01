using Newtonsoft.Json;

namespace Gameplay
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GatePointLevelSaveModel : LevelSaveModelBase<GatePointLevelInfoModel>
    {

    }
}
