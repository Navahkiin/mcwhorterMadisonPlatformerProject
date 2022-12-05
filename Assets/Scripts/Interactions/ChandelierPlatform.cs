using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChandelierPlatform : MonoBehaviour
{
    public GameObject Chandelier;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "King")
        {
            Destroy(this);
        }
    }
}
