using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) 
        : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.ZeroVelocity();
    }

    public override void Update()
    {
        base.Update();
        if (player.IsWallDetected() && xInput * player.facingDir > 0)
        {
            return;
        }

        if (xInput != 0 && !player.isBusy)
        {
            // Transition to the move state when there is horizontal input
            stateMachine.ChangeState(player.MoveState);
        }

    }

    public override void Exit()
    {
        base.Exit();

    }
}
