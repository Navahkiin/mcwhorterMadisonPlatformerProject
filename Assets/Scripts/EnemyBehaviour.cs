using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public int MaxHealth = 5;
    int currentHealth;

    private float direction = -1f;
    private float moveSpeed = 2f;
    private Rigidbody2D rb;
    private bool facingRight = false;
    private Vector3 localScale;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;

        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
    }

    private void LateUpdate()
    {
        CheckWhereToFace();
    }

    void CheckWhereToFace()
    {
        if (direction > 0)
            facingRight = true;
        else if (direction < 0)
            facingRight = false;

        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
            localScale.x *= -1;

        transform.localScale = localScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        direction *= -1f;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //dmg taken animation

        if(currentHealth <= 0)
        {
            Die();
        }
    }
    
    void Die()
    {
        //death animation
        //despawn enemy
        Debug.Log("Enemy has died");
        Destroy(gameObject);
    }
}
