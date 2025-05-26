using UnityEngine;
using StatePattern.StateMachine;
using StatePattern.Enemy.Bullet;
using StatePattern.Main;
using StatePattern.Player;

namespace StatePattern.Enemy
{
    public class CloneManController : EnemyController
    {
        private CloneManStateMachine stateMachine;
        public int ClonesAmount { get; private set; }

        public CloneManController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
        {
            SetCloneAmount(enemyScriptableObject.CloneCount);
            enemyView.SetController(this);
            ChangeColor(EnemyColorType.Default);
            CreateStateMachine();
            stateMachine.ChangeState(States.IDLE);
        }
        public void SetCloneAmount(int cloneAmountToSet) => ClonesAmount = cloneAmountToSet;

        private void CreateStateMachine() => stateMachine = new CloneManStateMachine(this);

        public override void UpdateEnemy()
        {
            if (currentState == EnemyState.DEACTIVE)
                return;

            stateMachine.Update();
        }

        public override void PlayerEnteredRange(PlayerController targetToSet)
        {
            base.PlayerEnteredRange(targetToSet);
            stateMachine.ChangeState(States.CHASING);
        }

        public override void PlayerExitedRange() => stateMachine.ChangeState(States.IDLE);

        public override void Die()
        {
            if (ClonesAmount > 0)
                stateMachine.ChangeState(States.CLONING);
            base.Die();
        }

        public void Teleport() => stateMachine.ChangeState(States.TELEPORTING);

        public void SetDefaultColor(EnemyColorType colorType) => enemyView.SetDefaultColor(colorType);
        public void ChangeColor(EnemyColorType colorType) => enemyView.ChangeColor(colorType);
    }
}