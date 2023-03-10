using Service;

namespace Infrastructure.States.StateMachine
{
    public interface IGameStateMachine : IService
    {
        void Enter<TState>() where TState : IState;
    }
}