using UnityEngine;

public class PlayerWallJump : PlayerState
{
    public PlayerWallJump(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 0.4f; // 设置墙跳的持续时间
        player.SetVelocity(-5*player.facingDir, player.jumpForce); // 重置速度
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer <= 0)
        {
            // 如果墙跳时间结束，切换到空中状态
            stateMachine.ChangeState(player.AirState);
            return;
        }
        
    }

    public override void Exit()
    {
        base.Exit();
        // 离开墙跳状态时可以添加其他逻辑
        if (player.IsGroundDetected())
        {
            // 如果在墙跳结束时检测到地面，切换到空闲状态
            stateMachine.ChangeState(player.IdleState);
        }
    }
}
