using StatePattern.Main;
using StatePattern.StateMachine;

namespace StatePattern.Enemy
{
    public class CloningState<T> : IState where T : EnemyController
    {
        public EnemyController Owner { get; set; }
        private GenericStateMachine<T> stateMachine;

        public CloningState(GenericStateMachine<T> stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter()
        {
            CreateNewClone();
            CreateNewClone();
        }

        public void Update() { }

        private void CreateNewClone()
        {
            CloneManController newCloneMan = GameService.Instance.EnemyService.CreateEnemy(Owner.Data) as CloneManController;
            newCloneMan.SetCloneAmount((Owner as CloneManController).ClonesAmount - 1);
            newCloneMan.Teleport();
            newCloneMan.SetDefaultColor(EnemyColorType.Clone);
            newCloneMan.ChangeColor(EnemyColorType.Clone);
            GameService.Instance.EnemyService.AddEnemy(newCloneMan);
        }

        public void OnStateExit() { }

    }
}