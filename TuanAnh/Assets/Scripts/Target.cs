using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour
{
    /*========== KHAI BÁO VÀ KHỞI TẠO CÁC GIÁ TRỊ BAN ĐẦU ==========*/
    public GameObject player;
    public HealthBar healthBar;
    public float maxHealth = 100f;
    public float currentHealth;

    public NavMeshAgent agent;
    public Animator enemyAnimator;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        currentHealth = maxHealth;
        healthBar.SetValue(maxHealth);

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0f)
        {
            Die();
        }

        healthBar.SetValue(currentHealth);
    }

    void Update()
    {
        if(currentHealth > 0)
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= 15f && Vector3.Distance(transform.position, player.transform.position) >= 2f)
            {
                GetComponent<NavMeshAgent>().destination = player.transform.position;

                //enemyAnimator.SetBool("Idle", false);
                enemyAnimator.SetBool("Run", true);
            }
            else
            {
                //enemyAnimator.SetBool("Idle", true);
                enemyAnimator.SetBool("Run", false);
                GetComponent<NavMeshAgent>().destination = transform.position;
            }

            if (Vector3.Distance(transform.position, player.transform.position) <= 2f)
            {
                enemyAnimator.SetTrigger("Attack");
            }
        }
        else
        {
            enemyAnimator.SetBool("Run", false);
            //enemyAnimator.SetBool("Idle", false);
            enemyAnimator.ResetTrigger("Attack");
            GetComponent<NavMeshAgent>().destination = transform.position;
            StartCoroutine(Die());
        }

    }

    IEnumerator Die()
    {    
        enemyAnimator.SetBool("Dead", true);
        yield return new WaitForSeconds(3); 
        Destroy(gameObject); 
    }
}
