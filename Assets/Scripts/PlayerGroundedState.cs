using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.linearVelocity = new Vector2(0, 0); // Reset horizontal velocity to 0
        // Additional logic for entering the grounded state can be added here
    }

    public override void Update()
    {
        base.Update();
        if (player.IsGroundDetected() == false)
        {
            // Transition to the air state when the player is not grounded
            stateMachine.ChangeState(player.AirState);
        }

        // if (xInput != 0)
        // {
        //     // Transition to the move state when there is horizontal input
        //     stateMachine.ChangeState(player.MoveState);
        // }
        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
        {
            // Transition to the jump state when the space key is pressed
            stateMachine.ChangeState(player.JumpState);
        }

        
    }

    public override void Exit()
    {
        base.Exit();
        // Additional logic for exiting the grounded state can be added here
    }
    
}
