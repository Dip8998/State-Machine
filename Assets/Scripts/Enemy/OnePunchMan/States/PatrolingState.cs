using StatePattern.Enemy;
using UnityEngine;

public class PatrolingState: IState
{
    public EnemyController Owner {  get; set; }
    private IStateMachine stateMachine;
    private int currentPatrollingIndex = -1;
    private Vector3 destination;

    public PatrolingState(IStateMachine stateMachine) => this.stateMachine = stateMachine;

    public void OnStateEnter()
    {
        SetNextWaypointIndex();
        destination = GetDestination();
        MoveTowardsDestination();
    }

    public void Update()
    {
        if (ReachedDestination())
            stateMachine.ChangeState(States.IDLE);
    }

    public void OnStateExit() { }

    private void SetNextWaypointIndex()
    {
        if(currentPatrollingIndex == Owner.Data.PatrollingPoints.Count - 1)
        {
            currentPatrollingIndex = 0;
        }
        else
        {
            currentPatrollingIndex++;
        }
    }

    private Vector3 GetDestination() => Owner.Data.PatrollingPoints[currentPatrollingIndex];

    private void MoveTowardsDestination()
    {
        Owner.Agent.isStopped = false;
        Owner.Agent.SetDestination(destination);
    }

    private bool ReachedDestination() => Owner.Agent.remainingDistance <= Owner.Agent.stoppingDistance;
}