using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDoor : Interactable
{
    private bool interacted;

    public GameObject LockedDoorTextBox;

    public override void Interact()
    {
        if (interacted)
        {
            LockedDoorTextBox.SetActive(true);
        }
        else
        {
            LockedDoorTextBox.SetActive(false);
        }
        interacted = !interacted;
    }

    void Start()
    {
        LockedDoorTextBox.SetActive(false);
    }
}
