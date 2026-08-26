using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 8f;
    public float airSpeed;
    public float xWallJumpForce;
    public float yWallJumpForce;
    private Rigidbody2D rb;
    private float horizontalInput;
    private GroundCollisionCheck groundCol;
    private WallCollisionCheck wallCol;
    private Vector3 startPos;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip JumpSound;
    [SerializeField] private Timer Timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        groundCol = GetComponentInChildren<GroundCollisionCheck>();
        wallCol = GetComponentInChildren<WallCollisionCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        if(horizontalInput != 0)
        {
            Timer.StartTime();
        }

        if (Input.GetKeyDown(KeyCode.Space) && groundCol.isGrounded)
        {
            Jump();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && wallCol.onWall)
        {
            WallJump();
        }
    }
    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (groundCol.isGrounded)
        {
            rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
        }
        else
        {
            rb.AddForce(new Vector2(horizontalInput * airSpeed, 0));
            rb.linearVelocity = new Vector2(Mathf.Clamp(rb.linearVelocity.x, -moveSpeed, moveSpeed), rb.linearVelocity.y); 
        }
       
    }
    void Jump()
    {
        AudioManager.instance.PlaySound(JumpSound);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }
    void WallJump()
    {
        AudioManager.instance.PlaySound(JumpSound);
        int jumpDirection = -wallCol.wallDirection; //Vores jump direction skal være det modsatte af væggens direction
        rb.linearVelocity = new Vector2(jumpDirection*xWallJumpForce, yWallJumpForce);
    }
    public void Respawn()
    {
        AudioManager.instance.PlaySound(deathSound);
        Timer.PauseTime();
        Timer.ResetTime();
        transform.position = startPos; //Vi transporterer vores player tilbage til start positionen
        rb.linearVelocity = Vector2.zero; //Vi nulstiller momementum på playeren 
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            
        }
           
    }
}
