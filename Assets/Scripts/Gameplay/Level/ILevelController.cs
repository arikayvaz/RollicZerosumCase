namespace Gameplay
{
    public interface ILevelController<TSaveModel> where TSaveModel: class
    {
        void OnLevelLoad(TSaveModel saveModel);
        void UnloadLevel();
    }
}