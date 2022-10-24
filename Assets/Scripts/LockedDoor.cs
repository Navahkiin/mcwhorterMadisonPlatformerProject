using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    private PlayerBehaviour Player;

    public bool DoorOpen, WaitingToOpen;

    private void Start()
    {
        Player = FindObjectOfType<PlayerBehaviour>();
    }

    private void Update()
    {
        if (WaitingToOpen)
        {
            if (Vector3.Distance(Player.FollowingKey.transform.position, transform.position) <0.1f)
            {
                WaitingToOpen = false;
                DoorOpen = true;

                Player.FollowingKey.gameObject.SetActive(false);
                Player.FollowingKey = null;
                gameObject.SetActive(false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Player.FollowingKey != null)
            {
                Player.FollowingKey.FollowTarget = transform;
                WaitingToOpen = true;
            }
        }
    }
}
