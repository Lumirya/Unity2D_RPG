using UnityEngine;
using System.Collections;

public class Player : Entity
{
    [Header("Attack details")]
    public Vector2[] attackMovement;
    

    public bool isBusy { get; private set; }  // 用于跟踪玩家是否忙碌
    [Header("Move info")]
    public float moveSpeed = 5f;
    public float jumpForce = 12f;
    [Header("Dash info")]
    public float dashSpeed = 15f;
    public float dashDuration = 0.5f;
    public float dashCooldown = 1f; // 冲刺冷却时间
    private float dashUsageTimer = 0f; // 用于跟踪冲刺冷却时间
    public float dashDir { get; private set; }


    #region States
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerState JumpState { get; private set; }
    public PlayerAirState AirState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallJump WallJumpState { get; private set; }
    public PlayerPrimaryAttackState PrimaryAttackState { get; private set; }

    #endregion

    protected override void Awake()
    {
        base.Awake();
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, "Idle");
        MoveState = new PlayerMoveState(this, StateMachine, "Move");
        JumpState = new PlayerJumpState(this, StateMachine, "Jump");
        AirState = new PlayerAirState(this, StateMachine, "Jump");
        DashState = new PlayerDashState(this, StateMachine, "Dash");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, "WallSlide");
        WallJumpState = new PlayerWallJump(this, StateMachine, "Jump");
        PrimaryAttackState = new PlayerPrimaryAttackState(this, StateMachine, "Attack");
    }

    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(IdleState);


    }
    protected override void Update()
    {
        base.Update();
        StateMachine.CurrentState.Update();
        CheckForDashInput();
    }

    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true; // 设置玩家为忙碌状态
        yield return new WaitForSeconds(_seconds); // 等待指定的时间
        isBusy = false; // 重置玩家为非忙碌状态
    }

    public void AnimationTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
    public void CheckForDashInput()
    {
        if (IsWallDetected())
        {
            return;
        }
        dashUsageTimer -= Time.deltaTime; // 更新冲刺冷却时间
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTimer <= 0f)
        {
            dashUsageTimer = dashCooldown; // 重置冲刺冷却时间
            dashDir = Input.GetAxisRaw("Horizontal");
            if (dashDir == 0)
            {
                dashDir = facingDir; // 如果没有水平输入，则使用当前面向方向
            }
            StateMachine.ChangeState(DashState);
        }
    }


}
