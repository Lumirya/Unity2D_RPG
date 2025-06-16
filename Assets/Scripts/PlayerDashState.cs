using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.dashDuration; // 设置冲刺持续时间
        // 初始化冲刺状态
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(player.dashSpeed * player.dashDir,0); // 设置冲刺速度
        if(!player.IsGroundDetected() && player.IsWallDetected())
        {
            // 如果在空中且检测到墙壁，切换到墙滑状态
            stateMachine.ChangeState(player.WallSlideState);
            return;
        }

        // 处理冲刺逻辑
        // 例如：检查输入，更新速度等
        if (stateTimer <= 0)
        {
            // 冲刺结束，返回到空中状态或其他状态
            stateMachine.ChangeState(player.IdleState);
            return;
        }

    }
    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, rb.linearVelocity.y); // 停止冲刺时将水平速度设置为0
        // 离开冲刺状态
    }
}
