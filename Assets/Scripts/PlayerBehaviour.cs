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

    public float JumpSpeed = 5f;
    [SerializeField]
    private Rigidbody2D player;

    // Player health & taking damage
    private int maxHealth = 100;
    private int playerHealth;
    public Slider Health;

    // Flip character when turning
    bool facingRight = true;

    // Shadow character control
    public bool notShadow;
    public GameObject Player;

    public GameObject Attack;

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
                // Disables attacking when in shadow form
                Attack.SetActive(false);
                Animator.SetBool("NotShadow", false);

            }
            else
            {
                Player.layer = LayerMask.NameToLayer("Default");
                Player.tag = "Player";
                SR.sprite = PlayerNormal;
                notShadow = true;
                Debug.Log("Normal form!");
                Attack.SetActive(true);
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
        hit = Physics2D.Raycast(foot.position, Vector2.down, 0.2f, ground);
        return hit;
    }

    private bool CheckShadowGround()
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(foot.position, Vector2.down, 0.2f, shadow);
        return hit;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            TakeDamage(20);
        }
    }
    void TakeDamage(int damage)
    {
        playerHealth -= damage;
        SetHealth(playerHealth);
        Animator.SetTrigger("Hit");
        if (playerHealth <= 0)
        {
            LoseScreen.SetActive(true);
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
