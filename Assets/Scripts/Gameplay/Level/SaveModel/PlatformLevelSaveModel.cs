using Newtonsoft.Json;

namespace Gameplay
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PlatformLevelSaveModel : LevelSaveModelBase<PlatformLevelInfoModel>
    {

    }
}