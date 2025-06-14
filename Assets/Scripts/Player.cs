using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Move info")]
    public float moveSpeed = 5f;
    public float jumpForce = 12f;

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

    #endregion

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, "Idle");
        MoveState = new PlayerMoveState(this, StateMachine, "Move");
        JumpState = new PlayerJumpState(this, StateMachine, "Jump");
        AirState = new PlayerAirState(this, StateMachine, "Jump");
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
    }


    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.linearVelocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }

    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

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
