using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBossTrigger : MonoBehaviour
{
    private PlayerBehaviour playerBehaviourRef;
    private BossBehaviour bossBehaviourRef;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CorruptedKing")
        {
            playerBehaviourRef = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
            playerBehaviourRef.TakeDamage(20);
        }

        if (collision.gameObject.tag == "CorruptedKingRange")
        {
            bossBehaviourRef = GameObject.Find("corruptedking").GetComponent<BossBehaviour>();
            bossBehaviourRef.CanAttack = true;
            bossBehaviourRef.AttackRef();
            Debug.Log("attack trigger working!");
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CorruptedKingRange")
        {
            bossBehaviourRef = GameObject.Find("corruptedking").GetComponent<BossBehaviour>();
            bossBehaviourRef.CanAttack = false;
        }
    }
}
