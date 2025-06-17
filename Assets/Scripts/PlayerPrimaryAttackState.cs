using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int comboCounter ;
    private float lastTimeAttacked;
    private float comboWindow = 2f;
    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (comboCounter > 2 || Time.time - lastTimeAttacked > comboWindow)
        {
            comboCounter = 0;
        }

        #region Choose attack Direction
        float attackDir = player.facingDir;
        if (xInput != 0)
            attackDir = xInput;
        #endregion

        player.anim.SetInteger("ComboCounter", comboCounter);
        player.SetVelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y);
        stateTimer = 0.1f;

    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            player.ZeroVelocity();
        }
        if (triggerCalled)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }


    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine("BusyFor", 0.1f);

        comboCounter++;
        lastTimeAttacked = Time.time;
        // 离开攻击状态时可以添加其他逻辑
    }
}
