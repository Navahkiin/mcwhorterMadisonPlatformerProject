using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    // Horizontal movement
    public float MovementSpeed = 1f;

    // Vertical movement
    [SerializeField]
    private Transform foot;
    [SerializeField]
    private LayerMask ground;

    public float JumpSpeed = 5f;
    [SerializeField]
    private Rigidbody2D player;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Horizontal movement
        var movement = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;

        // Vertical movement
        if (Input.GetButtonDown("Jump") && CheckGround())
        {
            player.velocity = new Vector2(player.velocity.x, JumpSpeed);
            Debug.Log("Jump Working");
        }
    }

    private bool CheckGround()
    {
        RaycastHit2D hit;

        hit = Physics2D.Raycast(foot.position, Vector2.down, 0.2f, ground);

        return hit;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision Detected");
    }
}
