using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        // Additional logic for entering the air state can be added here
    }

    public override void Update()
    {
        base.Update();

        if (xInput != 0)
        {
            // Set horizontal velocity based on input, keeping vertical velocity unchanged
            player.SetVelocity(xInput * player.moveSpeed*.8f, rb.linearVelocity.y);
        }

        if (player.IsWallDetected())
        {
            // If the player is touching a wall, switch to wall slide state
            stateMachine.ChangeState(player.WallSlideState);
            return;
        }

        if (player.IsGroundDetected())
        {
            // Transition to the grounded state when the player is falling
            stateMachine.ChangeState(player.IdleState);
        }

    }

    public override void Exit()
    {
        base.Exit();
        // Additional logic for exiting the air state can be added here
    }
}
