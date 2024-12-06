using UnityEngine;
using UnityEngine.AI;

public class MantisAnimator : MonoBehaviour
{
    public GameObject enemyToControl;
    NavMeshAgent enemyAgent;
    Base_enemy enemyAI;
    Enemi_health enemyHealth;
    base_enemi_attack enemyAttack;
    shrimp_phase_2 phase2;
    shrimp_phase_3 phase3;

    [HideInInspector] public Animator anim;

    void Awake()
    {
        enemyAgent = enemyToControl.GetComponent<NavMeshAgent>();
        enemyAI = enemyToControl.GetComponent<Base_enemy>();
        enemyHealth = enemyToControl.GetComponent<Enemi_health>();
        enemyAttack = enemyToControl.GetComponentInChildren<base_enemi_attack>();
        phase2 = enemyToControl.GetComponentInChildren<shrimp_phase_2>();
        phase3 = enemyToControl.GetComponentInChildren<shrimp_phase_3>();

        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (enemyAI.volnereble && !anim.GetBool("Vulnerable") && !enemyHealth.Harponed)
        {
            anim.SetBool("Vulnerable", true);
            Debug.Log("Squick");
        }
        else if (!enemyAI.volnereble && anim.GetBool("Vulnerable"))
        {
            anim.SetBool("Vulnerable", false);
        }

        if (enemyHealth.Harponed && !anim.GetBool("Harpooned"))
        {
            anim.SetBool("Vulnerable", false);
            anim.SetBool("Harpooned", true);
        }
        else if (!enemyHealth.Harponed && anim.GetBool("Harpooned"))
        {
            anim.SetBool("Harpooned", false);
        }

        //if (enemyAttack.ElastickAnim && !anim.GetBool("Punch"))
        //{
        //    anim.SetBool("Punch", true);
        //    anim.SetBool("Shockwave 0", false);
        //}
        //else if(!enemyAttack.ElastickAnim && anim.GetBool("Punch"))
        //{
        //    anim.SetBool("Punch", false);
        //}

        if (enemyAgent.velocity.magnitude > 0.1 && !anim.GetBool("Walking") && !anim.GetBool("Punch") && !anim.GetBool("Shockwave 0"))
        {
            anim.SetBool("Walking", true);
        }
        else if (enemyAgent.velocity.magnitude <= 0.1 && anim.GetBool("Walking"))
        {
            anim.SetBool("Walking", false);
        }

    }
}
