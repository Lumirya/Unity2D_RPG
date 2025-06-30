using UnityEngine;

public class SkeletonIdleState : SkeletonGroundedState
{

    public SkeletonIdleState(Enemy _enemybase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemybase, _stateMachine, _animBoolName, _enemy)
    {
        enemy = _enemy;
    }
    public override void Enter()
    {
        base.Enter();
        // Additional logic for entering the idle state
        stateTimer = enemy.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
        // Additional logic for exiting the idle state
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
        {
            // Transition to move state or another state if needed
            stateMachine.ChangeState(enemy.moveState);
        }
        // Logic for updating the idle state
        // For example, check for player proximity or other conditions to transition to another state
        
    }
}
