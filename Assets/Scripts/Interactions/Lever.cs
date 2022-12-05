using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Lever : Interactable
{
    public Sprite LeverUp;
    public Sprite LeverDown;

    private SpriteRenderer sr;
    private bool isDown;

    public GameObject SpikeTrap;

    public Sprite SpikeDown;
    public Sprite SpikeUp;

    public override void Interact()
    {
        if(isDown)
        {
            sr.sprite = LeverUp;
            SpikeTrap.GetComponent<SpriteRenderer>().sprite = SpikeUp;
            SpikeTrap.GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            sr.sprite = LeverDown;
            SpikeTrap.GetComponent<SpriteRenderer>().sprite = SpikeDown;
            SpikeTrap.GetComponent<BoxCollider2D>().enabled = false;
        }
        isDown = !isDown;
    }

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = LeverUp;
    }
}
