using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))] // checks to make sure that anything with this script also has a BoxCollider2D
public abstract class Interactable : MonoBehaviour
{
    public abstract void Interact();

    private void Reset() // checks to make sure that the BoxCollider2D is set to a trigger
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerBehaviour>().OpenInteractableIcon();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerBehaviour>().CloseInteractableIcon();
        }
    }
}
