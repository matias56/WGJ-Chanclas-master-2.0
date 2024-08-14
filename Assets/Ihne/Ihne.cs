using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Ihne : MonoBehaviour
{
    private Camera cam;
    public float speed = 4f;

    private float horizontal;


    private Vector2 vel;
    public bool running => Mathf.Abs(vel.x) > 0.25f || Mathf.Abs(horizontal) > 0.25f;

   
    private float jumpingPower = 8f;
    private bool isFacingRight = true;

    public Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

   

    

    public bool isDoubleJ;
    private bool doubleJump;

    public float attackRange = 1f;       // Range of the attack
    public int attackDamage = 20;        // Damage dealt by the attack
    public Transform attackPoint;        // Point from where the attack originates
    public LayerMask enemyLayers;        // Layers that are considered enemies
    private bool canAttack;

    public float delayBeforeRestart = 5f; // Time to wait before restarting the level
    private Coroutine restartCoroutine; // Reference to the active coroutine

    public GameObject startP;

    public GameObject l;

    public bool onT;

    private bool isPlayerProtected = false;

    public float timer = 5;

    public float initTime = 5;

    public bool hasStone;

    public FadeTitle ft;

    public bool isSton;

    public GameObject oi;



    [SerializeField] private TrailRenderer tr;


    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        startP = GameObject.Find("Start");

        cam = Camera.main;


        l.SetActive(false);

        onT = false;

        hasStone = false;


       

        isSton = false;

        if(SceneManager.GetActiveScene().name == "1")
        {
            canDash = false;
        }

    }

    // Update is called once per frame
    void Update()
    {



        horizontal = Input.GetAxisRaw("Horizontal");

        if (onT && Input.GetKeyDown(KeyCode.Q))
        {
           

            l.SetActive(true);

            isPlayerProtected = true;

           
        }

        if(isPlayerProtected == true)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                l.SetActive(false);

                isPlayerProtected = false;

                timer = initTime;
            }
        }

        if (IsGrounded() && !Input.GetButton("Jump"))
        {
            doubleJump = false;
        }


        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded() || doubleJump)
            {
                

                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);

                doubleJump = !doubleJump;
            }
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

        }


       

        if (Input.GetKeyDown(KeyCode.X) && canAttack)
        {
            Attack();
        }

        Flip();

        if (isDashing)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E) && canDash)
        {
            StartCoroutine(Dashing());
        }
    }

    private void FixedUpdate()
    {

        if (isDashing)
        {
            return;
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        bool isWalking = Mathf.Abs(horizontal) > 0;
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;

        }
    }

    

    void Attack()
    {
        // Play attack animation
        // Example: animator.SetTrigger("Attack");

        // Detect enemies in range of the attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage each enemy
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Dark"))
        {
            // Start the countdown to restart the level if not already running
            speed = 1;
        }

        if (other.CompareTag("Hazard"))
        {

            if(oi.activeSelf && SceneManager.GetActiveScene().name == "1")
            {
                ft.TriggerDialogue("death");
            }
            
            Teleport();

           
            
        }

        if(other.CompareTag("Torch"))
        {
            onT = true;
        }

        if(other.CompareTag("Stone"))
        {
            hasStone = true;
            Destroy(other.gameObject);
            if (SceneManager.GetActiveScene().name == "1")
            {
                ft.TriggerDialogue("st");
            }
               
            Teleport();
        }

        if (other.CompareTag("Exit") && hasStone)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if(other.CompareTag("OiTr"))
        {
            oi.SetActive(true);
            ft.TriggerDialogue("oi");
            Destroy(other.gameObject);
        }

        if (other.CompareTag("AddDash"))
        {
            
            ft.TriggerDialogue("dash");
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Loop"))
        {

            ft.TriggerDialogue("end");
            Destroy(other);
            speed = 0;
        }

        if (other.CompareTag("Key"))
        {

            
            Teleport();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Dark") && !isPlayerProtected)
        {
            // Start the countdown to restart the level if not already running
            if (restartCoroutine == null)
            {
                restartCoroutine = StartCoroutine(RestartLevel());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Dark"))
        {
            speed = 4;
            // Stop the countdown when the player leaves the danger zone
            if (restartCoroutine != null)
            {
                StopCoroutine(restartCoroutine);
                restartCoroutine = null;
            }
        }

        if (other.CompareTag("Torch"))
        {
            onT = false;
        }
    }

    private IEnumerator RestartLevel()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delayBeforeRestart);

        // Restart the current level
        Teleport();

       
        
    }

    void Teleport()
    {
        this.transform.position = startP.transform.position;
    }

    public IEnumerator Dashing()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
