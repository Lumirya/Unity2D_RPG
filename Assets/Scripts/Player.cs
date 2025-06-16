using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Move info")]
    public float moveSpeed = 5f;
    public float jumpForce = 12f;
    [Header("Dash info")]
    public float dashSpeed = 15f;
    public float dashDuration = 0.5f;
    public float dashCooldown = 1f; // 冲刺冷却时间
    private float dashUsageTimer = 0f; // 用于跟踪冲刺冷却时间
    public float dashDir { get; private set; }  
    

    [Header("Collision info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;

    public int facingDir { get; private set; } = 1; // 1 for right, -1 for left
    private bool FacingRight => facingDir == 1;

    #region Components

    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion


    #region States
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerState JumpState { get; private set; }
    public PlayerAirState AirState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallJump WallJumpState { get; private set; }

    #endregion

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, "Idle");
        MoveState = new PlayerMoveState(this, StateMachine, "Move");
        JumpState = new PlayerJumpState(this, StateMachine, "Jump");
        AirState = new PlayerAirState(this, StateMachine, "Jump");
        DashState = new PlayerDashState(this, StateMachine, "Dash");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, "WallSlide");
        WallJumpState = new PlayerWallJump(this, StateMachine, "Jump");
    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        StateMachine.Initialize(IdleState);


    }
    private void Update()
    {
        StateMachine.CurrentState.Update();
        CheckForDashInput();
    }

    public void CheckForDashInput()
    {
        dashUsageTimer -= Time.deltaTime; // 更新冲刺冷却时间
        if (Input.GetKeyDown(KeyCode.LeftShift)&& dashUsageTimer <=0f)
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
    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.linearVelocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }

    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + Vector3.right * wallCheckDistance);
    }

    public void Flip()
    {
        facingDir *= -1; // Toggle the facing direction
        transform.Rotate(0f, 180f, 0f); // Rotate the player to face the opposite direction
    }

    public void FlipController(float _x)
    {
        if (_x > 0 && !FacingRight)
        {
            Flip();
        }
        else if (_x < 0 && FacingRight)
        {
            Flip();
        }
    }
}
