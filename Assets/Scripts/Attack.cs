using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Animator animator;

    public Transform AttackPoint;
    public float AttackRange = 0.5f;
    public float AttackRate = 2f;
    float nextAttackTime = 0f;

    public LayerMask EnemyLayers;

    public GameObject HitRange;

    void Start()
    {
        HitRange.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                attack();
                nextAttackTime = Time.time + 1f / AttackRate;
                Debug.Log("attack button working");

                StartCoroutine(ShowAndHide(1.0f));
            }
        }
    }

    IEnumerator ShowAndHide(float delay)
    {
        HitRange.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        HitRange.SetActive(false);
    }

    void attack()
    {
        //play attack animation, currently represented by a box
        //register and damage enemies that the attack hits
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, EnemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit" + enemy.name);
            enemy.GetComponent<EnemyBehaviour>().TakeDamage(1);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
            return;

        //draws a circle where the attack hitbox is
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
}
