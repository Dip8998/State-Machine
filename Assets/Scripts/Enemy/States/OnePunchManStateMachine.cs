using StatePattern.Enemy;
using System.Collections.Generic;

public class OnePunchManStateMachine
{
    private OnePunchManController Owner;

    private IState currentState;

    protected Dictionary<OnePunchManStates, IState> States = new Dictionary<OnePunchManStates, IState>();

    public OnePunchManStateMachine(OnePunchManController owner)
    {
        Owner = owner;
        CreateStates();
        SetOwner();
    }

    private void CreateStates()
    {
        States.Add(OnePunchManStates.IDLE, new IdleState(this));
        States.Add(OnePunchManStates.ROTATING, new RotatingState(this));
        States.Add(OnePunchManStates.SHOOTING, new ShootingState(this));
    }

    private void SetOwner()
    {
        foreach(IState state in States.Values)
        {
            state.Owner = Owner;
        }
    }

    public void Update() => currentState?.Update();

    protected void ChangeState(IState newState)
    {
        currentState?.OnStateExit();
        currentState = newState;
        currentState?.OnStateEnter();
    }

    public void ChangeState(OnePunchManStates newStates) => ChangeState(States[newStates]);
}

public enum OnePunchManStates
{
    IDLE,
    ROTATING,
    SHOOTING
}