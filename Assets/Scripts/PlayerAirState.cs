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

        // if (xInput != 0)
        // {
        //     // Transition to the move state when there is horizontal input
        //     stateMachine.ChangeState(player.MoveState);
        // }
        if (rb.linearVelocity.y == 0)
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
