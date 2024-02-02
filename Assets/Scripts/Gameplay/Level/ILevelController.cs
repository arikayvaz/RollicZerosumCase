namespace Gameplay
{
    public interface ILevelController<TSaveModel, TLevelInfo> 
        where TSaveModel : class
        where TLevelInfo : class
    {
        void OnLevelLoad(TSaveModel saveModel);
        void UnloadLevel();
        void SpawnItems(TLevelInfo[] levelInfos);
        void AddItem(TLevelInfo levelInfo);
        void RemoveLastItem();
    }
}