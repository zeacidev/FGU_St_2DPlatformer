using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private bool IsMenu = false;

    private bool cheatUnlocked = false;
    private int codeIndex = 0;

    private readonly KeyCode[] konamiCode = new KeyCode[]
    {
        KeyCode.UpArrow, KeyCode.UpArrow,
        KeyCode.DownArrow, KeyCode.DownArrow,
        KeyCode.LeftArrow, KeyCode.RightArrow,
        KeyCode.LeftArrow, KeyCode.RightArrow,
        KeyCode.B, KeyCode.A,
    };

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
    [SerializeField] private AudioClip unlockSound;
    [SerializeField] private Timer Timer;

    private bool isDead = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        groundCol = GetComponentInChildren<GroundCollisionCheck>();
        wallCol = GetComponentInChildren<WallCollisionCheck>();

        if (IsMenu)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.linearVelocity = Vector2.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (IsMenu && !cheatUnlocked)
        {
            CheckKonamiCode();

            horizontalInput = 0;
            return;
        }

        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (isDead) return;

        if(horizontalInput != 0)
        {
            if (Timer)
            {
                Timer.StartTime();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && groundCol.isGrounded)
        {
            Jump();
            if (Timer)
            {
                Timer.StartTime();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space) && wallCol.onWall)
        {
            WallJump();
        }
    }

    private void CheckKonamiCode()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(konamiCode[codeIndex]))
            {
                codeIndex++;

                if (codeIndex >= konamiCode.Length)
                {
                    UnlockCheatMovement();
                }
            }
            else
            {
                codeIndex = 0;
            }
        }
    }

    private void UnlockCheatMovement()
    {
        cheatUnlocked = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        AudioManager.instance.PlaySound(unlockSound);

        Debug.Log("Konami Code Unlocked! Go break the menu playground!");
    }

    private void FixedUpdate()
    {
        if (isDead || (IsMenu && !cheatUnlocked))
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

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
        if (!isDead)
        {
            StartCoroutine(RespawnSequence());
        }
    }

    private IEnumerator RespawnSequence()
    {
        isDead = true;

        AudioManager.instance.PlaySound(deathSound);
        AudioManager.instance.PauseSoundtrack();

        if (Timer)
        {
            Timer.PauseTime();
            Timer.ResetTime();
        }

        yield return StartCoroutine(MainMenuButton.instance.FadeScreenPublic(1f));
        yield return new WaitForSeconds(0.5f);

        transform.position = startPos;
        rb.linearVelocity = Vector2.zero;

        AudioManager.instance.ResumeSoundtrack();

        yield return StartCoroutine(MainMenuButton.instance.FadeScreenPublic(0f));
        isDead = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            
        }
           
    }

    public void StopMovementForWin()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;

        horizontalInput = 0;

        if (Timer != null)
        {
            Timer.PauseTime();
        }
        else
        {
            Debug.LogError("Player Movement Error: The Timer field slot is empty in the Inspector panel!");
        }
    }
}
