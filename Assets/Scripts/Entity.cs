using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Components

    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion

    [Header("Collision info")]
    public Transform attackCheck;
    public float attackCheckRadius;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;

    public int facingDir { get; private set; } = 1; // 1 for right, -1 for left
    protected bool FacingRight => facingDir == 1;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {

        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }
    protected virtual void Update()
    {

    }

    public virtual void Damage()
    {
        // Implement damage logic here
        // This could include reducing health, playing a damage animation, etc.
        Debug.Log(gameObject.name + " Entity damaged!");
    }
    #region Collision
    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + Vector3.right * wallCheckDistance);
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
    #endregion

    #region Flip
    public virtual void Flip()
    {
        facingDir *= -1; // Toggle the facing direction
        transform.Rotate(0f, 180f, 0f); // Rotate the player to face the opposite direction
    }

    public virtual void FlipController(float _x)
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
    #endregion

    #region Velocity
    public void SetZeroVelocity()
    {
        rb.linearVelocity = new Vector2(0, 0);
    }
    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.linearVelocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }
    #endregion


}
