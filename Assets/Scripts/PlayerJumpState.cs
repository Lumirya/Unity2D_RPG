using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, player.jumpForce); // Set the vertical velocity to the jump force
    } 

    public override void Update()
    {
        base.Update();

        if (rb.linearVelocity.y < 0)
        {
            // Transition to the air state when the player is falling
            stateMachine.ChangeState(player.AirState);
        }


    }

    public override void Exit()
    {
        base.Exit();

    }
}
