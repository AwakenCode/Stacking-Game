using Character;
using Service;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        Player CreatePlayer();
    }
}