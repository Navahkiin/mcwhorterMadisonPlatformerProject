using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    // Horizontal movement
    public float MovementSpeed = 1f;
    public Animator Animator;

    // Vertical movement
    [SerializeField]
    private Transform foot;
    [SerializeField]
    private LayerMask ground;
    [SerializeField]
    private LayerMask shadow;
    private Vector2 jumpCheck = new Vector2(0.7f, 0.01f);

    public float JumpSpeed = 5f;
    [SerializeField]
    private Rigidbody2D player;

    // Interactable actions
    private Vector2 boxSize = new Vector2(3f, 3f);
    public GameObject InteractIcon;

    // Player health & taking damage
    private int maxHealth = 100;
    private int playerHealth;
    public Slider Health;
    private BossBehaviour bossBehaviour;

    // Flip character when turning
    bool facingRight = true;

    // Shadow character control
    public bool notShadow;
    public GameObject Player;

    // Player sprites
    public SpriteRenderer SR;
    public Sprite PlayerNormal;
    public Sprite PlayerShadow;

    // Key and door unlocking
    public Transform KeyFollowPoint;
    public KeyBehaviour FollowingKey;

    // References to the canvas and win/lose screens
    public GameObject Canvas;
    public GameObject WinScreen;
    public GameObject LoseScreen;

    private void Start()
    {
        Time.timeScale = 1;
        InteractIcon.SetActive(false);

        notShadow = true;
        Animator.SetBool("NotShadow", true);
        Player.tag = "Player";

        playerHealth = maxHealth;
        SetMaxHealth(maxHealth);
    }

    void Update()
    {
        // Vertical movement
        if (Input.GetButtonDown("Jump") && CheckGround() && notShadow)
        {
            player.velocity = new Vector2(player.velocity.x, JumpSpeed);
            Debug.Log("Jump Working");
        }

        if (Input.GetButtonDown("Jump") && CheckShadowGround() && !notShadow)
        {
            player.velocity = new Vector2(player.velocity.x, JumpSpeed);
            Debug.Log("Shadow Jump Working");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckIfInteractable();
        }

        // Shadow transformation
        if (Input.GetButtonDown("LShift"))
        {
            if (notShadow == true)
            {
                Player.layer = LayerMask.NameToLayer("PlayerShadow");
                Player.tag = "PlayerShadow";
                SR.sprite = PlayerShadow;
                notShadow = false;
                Debug.Log("Shadow form!");
                Animator.SetBool("NotShadow", false);

            }
            else
            {
                Player.layer = LayerMask.NameToLayer("Default");
                Player.tag = "Player";
                SR.sprite = PlayerNormal;
                notShadow = true;
                Debug.Log("Normal form!");
                Animator.SetBool("NotShadow", true);
            }
        }
    }

    private void FixedUpdate()
    {
        // Horizontal movement
        var movement = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;

        if (Input.GetAxisRaw("Horizontal") > 0 && !facingRight)
        {
            Flip();
        }
        if (Input.GetAxisRaw("Horizontal") < 0 && facingRight)
        {
            Flip();
        }

        Animator.SetFloat("Speed", Mathf.Abs(movement));
    }

    void Flip() // Flips character when turning
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }

    private bool CheckGround()
    {
        RaycastHit2D hit;
        hit = Physics2D.BoxCast(foot.position, jumpCheck, 0f, Vector2.down, 0.2f, ground);
        return hit;
    }

    private bool CheckShadowGround()
    {
        RaycastHit2D hit;
        hit = Physics2D.BoxCast(foot.position, jumpCheck, 0f, Vector2.down, 0.2f, shadow);
        return hit;
    }

    void CheckIfInteractable()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position,boxSize, 0, Vector2.zero);

        if (hits.Length > 0)
        {
            foreach (RaycastHit2D rc in hits)
            {
                if (rc.transform.GetComponent<Interactable>())
                {
                    rc.transform.GetComponent<Interactable>().Interact();
                    return;
                }
            }
        }
    }

    public void OpenInteractableIcon()
    {
        InteractIcon.SetActive(true);
    }

    public void CloseInteractableIcon()
    {
        InteractIcon.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            TakeDamage(20);
        }

        if (collision.gameObject.tag == "CorruptedKing")
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        SetHealth(playerHealth);
        Animator.SetTrigger("Hit");
        if (playerHealth <= 0)
        {
            LoseScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void SetMaxHealth(int health)
    {
        Health.maxValue = health;
        Health.value = health;
    }

    public void SetHealth(int health)
    {
        Health.value = health;
    }
}
