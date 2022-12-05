using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    public Animator Animator;

    public GameObject BossPlatformHolder;
    public GameObject WinScreen;
    
    public GameObject AttackTrigger;
    public GameObject DmgRange;
    public GameObject ChargeDmg;
    public bool CanAttack = false;
    private bool isAttacking = false;
    private bool isCharging = false;
    private bool isStunned = false;
    private float attackCounter;

    public CameraShake CameraShake;

    public GameObject PlayerRef;
    private bool flip;
    private bool facingRight;
    public float SearchingSpeed;
    private float chargingSpeed;

    // Start is called before the first frame update
    void Start()
    {
        SearchingSpeed = 0f;
        chargingSpeed = 0f;
        isAttacking = false;
        isCharging = false;
        isStunned = false;

        attackCounter = 0f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (attackCounter < 3f)
        {
            Vector3 scale = transform.localScale;
            if (PlayerRef.transform.position.x > transform.position.x)
            {
                scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
                transform.Translate(SearchingSpeed * Time.deltaTime, 0, 0);
                facingRight = false;
            }
            else
            {
                scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
                transform.Translate(SearchingSpeed * Time.deltaTime * -1, 0, 0);
                facingRight = true;
            }
            transform.localScale = scale;
        }
        else if (isCharging == true)
        {
            if (facingRight == false)
            {
                transform.Translate(chargingSpeed * Time.deltaTime * 1, 0, 0);
            }
            else
            {
                transform.Translate(chargingSpeed * Time.deltaTime * -1, 0, 0);
            }
        }
    }

    public void AttackRef() //added so that the coroutine can be called from another script
    {
        if (isAttacking == false)
        {
            StartCoroutine(Attack());
        }
    }

    public IEnumerator Attack()
    {
        if ((attackCounter < 3f) && (CanAttack = true)) // regular attack
        {
            // preparing to attack
            SearchingSpeed = 0f;
            Animator.SetFloat("Speed", 0f);
            Animator.SetBool("AttackTrigger", true);
            isAttacking = true;
            Debug.Log("In Range!");
            yield return new WaitForSeconds(0.58f);

            // Attacking
            Debug.Log("Attack!");
            DmgRange.SetActive(true);
            yield return new WaitForSeconds(0.3f);

            // Returning to idle/searching state
            Debug.Log("Attack Ended");
            Animator.SetBool("AttackTrigger", false);
            Animator.SetBool("IdleTrigger", true);
            DmgRange.SetActive(false);
            isAttacking = false;
            attackCounter += 1f;
            SearchingSpeed = 3f;
            Animator.SetFloat("Speed", 3f);

            if (attackCounter == 3f)
            {
                AttackRef();
            }
        }
        else if ((attackCounter == 3f) && (isStunned == false)) // special attack
        {
            // preparing to charge
            SearchingSpeed = 0f;
            yield return new WaitForSeconds(2f);

           // charge attack
            chargingSpeed = 8;
            Animator.SetFloat("Speed", 8f);
            ChargeDmg.SetActive(true);
            isCharging = true;
        }
        else if ((attackCounter == 3f) && (isStunned == true)) // stun
        {
            Debug.Log("stunned!");
            ChargeDmg.SetActive(false);
            isCharging = false;
            chargingSpeed = 0f;
            Animator.SetFloat("Speed", 0f);
            FallingPlatforms();

            yield return new WaitForSeconds(5f);
            attackCounter = 0f;
            SearchingSpeed = 3f;
            Animator.SetFloat("Speed", 3f);
        }
        // art and animations are not finished for the boss so this replaces that for now
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Wall") && (attackCounter == 3f))
        {
            Debug.Log("wall collision!");
            isStunned = true;
            isCharging = false;
            StartCoroutine(Attack()); // makes it so that you don't have to be in front of the boss for the stun to trigger
        }

        if (collision.gameObject.tag == "Chandelier")
        {
            BossDeath();
        }
    }

    void FallingPlatforms()
    {
        BossPlatformHolder.SetActive(false);
    }

    void BossDeath()
    {
        Time.timeScale = 0;
        WinScreen.SetActive(true);
    }
}
