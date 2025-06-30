using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        // Additional logic for entering the wall slide state can be added here
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // If the player presses jump while wall sliding, switch to wall jump state
            stateMachine.ChangeState(player.WallJumpState);
            return;
        }

        if (xInput != 0 && player.facingDir != xInput)
        {
            // If the player is trying to move in the opposite direction, switch to air state
            stateMachine.ChangeState(player.IdleState);
            return;
        }
        if (yInput < 0)
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); // Reset horizontal velocity if moving down
        else
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y*.7f); // Set vertical velocity for wall slide

        if (player.IsGroundDetected())
        {
            // If the player is on the ground, switch to idle state
            stateMachine.ChangeState(player.IdleState);
            return;
        }


    }

    public override void Exit()
    {
        base.Exit();
        // Additional logic for exiting the wall slide state can be added here
    }
}
