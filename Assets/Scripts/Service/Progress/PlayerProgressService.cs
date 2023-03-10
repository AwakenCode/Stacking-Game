using Data;

namespace Service.Progress
{
    public class PlayerProgressService : IPlayerProgressService
    {
        private readonly PlayerProgress _plyerProgress;

        public PlayerProgress PlayerProgress => _plyerProgress;

        public PlayerProgressService()
        {
            _plyerProgress = new PlayerProgress();
        }
    }
}