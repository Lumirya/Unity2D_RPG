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
        enemy.lastTimeAttacked = Time.time;
        // Cleanup when exiting attack state
    }

    public override void Update()
    {
        base.Update();

        enemy.SetZeroVelocity(); // Stop movement while attacking

        if (triggerCalled)
        {
            // If the attack animation is done, return to battle state
            enemy.stateMachine.ChangeState(enemy.battleState);
        }
        
    }
}