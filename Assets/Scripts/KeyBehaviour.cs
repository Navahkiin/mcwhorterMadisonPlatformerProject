using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{
    private bool isFollowing;
    public float FollowSpeed = 3;
    public Transform FollowTarget;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowing)
        {
            // Lerp is used here to move the key to the player slowly and gradually
            transform.position = Vector3.Lerp(transform.position, FollowTarget.position, FollowSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (!isFollowing)
            {
                PlayerBehaviour Player = FindObjectOfType<PlayerBehaviour>();
                FollowTarget = Player.KeyFollowPoint;
                isFollowing = true;
                Player.FollowingKey = this;
            }
        }
    }
}
