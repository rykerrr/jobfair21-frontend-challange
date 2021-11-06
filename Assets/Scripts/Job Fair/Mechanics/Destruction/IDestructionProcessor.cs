namespace Platformer.JobFair.Destruction
{
    /// <summary>
    /// Kept separate from IProjectileCreationHandler as not all objects that will be destroyed
    /// will also be created again during runtime
    /// </summary>
    public interface IDestructionProcessor
    {
        void Destroy();
    }
}
