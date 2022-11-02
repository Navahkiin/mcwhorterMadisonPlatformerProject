using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableBlock : MonoBehaviour
{
    public GameObject BlockRef;

    private void Start()
    {
        BlockRef.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            BlockRef.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            BlockRef.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }
}
