using Data;

namespace Service.Progress
{
    public interface IPlayerProgressService : IService
    {
        PlayerProgress PlayerProgress { get; }
    }
}