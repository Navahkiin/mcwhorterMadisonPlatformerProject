using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenChain : Interactable
{
    private bool notBroken;

    public override void Interact()
    {
        if (notBroken)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            GetComponent<BoxCollider2D>().enabled = false;
            // sets chandelier sprite to it's normal state
        }
        else
        {
            // change sprite to broken sprite
            // plays nim breaking the chandelier animation
            // after nim's animation, the chandelier will fall and nim will jump off
        }
        notBroken = !notBroken;
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static; notBroken = true;
        notBroken = true; 
    }
}
