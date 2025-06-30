using UnityEngine;

public class SkeletonAttackState : EnemyState
{
    private Enemy_Skeleton enemy;

    public SkeletonAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton enemy) 
        : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        // Additional setup for attack state can be done here
    }

    public override void Exit()
    {
        base.Exit();
        // Cleanup when exiting attack state
    }

    public override void Update()
    {
        base.Update();
        // Logic for attacking the player
        if (enemy.IsPlayerDetected())
        {
            if (enemy.IsPlayerDetected().distance < enemy.attackDistance)
            {
                Debug.Log("Attack Player");
                enemy.SetZeroVelocity(); // Stop movement while attacking
                // Trigger attack animation or logic here
                return;
            }
        }
        if (triggerCalled)
        {
            // If the attack animation is done, return to battle state
            enemy.stateMachine.ChangeState(enemy.battleState);
            return;
        }
    }
}