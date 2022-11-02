using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f, 0f, -1f);
    private float smoothTime = 0.10f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField]
    private Transform player;

    void FixedUpdate()
    {
        Vector3 playerPosition = player.position + offset;
        // SmoothDamp changes a vector over time! :)
        transform.position = Vector3.SmoothDamp(transform.position, playerPosition, ref velocity, smoothTime);
    }
}
