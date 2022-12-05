using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomEnter : MonoBehaviour
{
    public GameObject ClosingDoor;

    public GameObject Camera;

    private BossBehaviour bossBehaviour;

    void Start()
    {
        ClosingDoor.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Camera.GetComponent<CameraBehaviour>().enabled = false;

        ClosingDoor.SetActive(true);

        // This references BossBehaviour and changes the bools to change actions :D
        GameObject.Find("corruptedking").GetComponent<BossBehaviour>().SearchingSpeed = 3f;

        bossBehaviour = GameObject.Find("corruptedking").GetComponent<BossBehaviour>();
        bossBehaviour.AttackRef();
        bossBehaviour.Animator.SetFloat("Speed", 3f);
    }
}
