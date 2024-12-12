using UnityEngine;
using UnityEngine.AI;

public class MantisAnimator : BaseAnimator
{
    public GameObject enemyToControl;
    NavMeshAgent enemyAgent;
    Base_enemy enemyAI;
    Enemi_health enemyHealth;
    base_enemi_attack enemyAttack;
    Boss1_AI bossAI;

    

    void Awake()
    {
        enemyAgent = enemyToControl.GetComponent<NavMeshAgent>();
        enemyAI = enemyToControl.GetComponent<Base_enemy>();
        enemyHealth = enemyToControl.GetComponent<Enemi_health>();
        enemyAttack = enemyToControl.GetComponentInChildren<base_enemi_attack>();
        bossAI = enemyToControl.GetComponent<Boss1_AI>();

        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (enemyHealth.Harponed && !anim.GetBool("Harpooned"))
        {
            anim.SetBool("Harpooned", true);
        }
        else if (!enemyHealth.Harponed && anim.GetBool("Harpooned"))
        {
            anim.SetBool("Harpooned", false);
        }

        if (enemyAgent.velocity.magnitude > 0.1 && !anim.GetBool("Walking"))
        {
            anim.SetBool("Walking", true);
        }
        else if (enemyAgent.velocity.magnitude <= 0.1 && anim.GetBool("Walking"))
        {
            anim.SetBool("Walking", false);
        }

        if(bossAI.PhaseSwitch && !anim.GetBool("PhaseSwitch"))
        {
            anim.SetBool("PhaseSwitch", true);
        }
        else if(!bossAI.PhaseSwitch && anim.GetBool("PhaseSwitch"))
        {
            anim.SetBool("PhaseSwitch", false);
        }

        if(enemyAI.volnereble && !anim.GetBool("VulnBool"))
        {
            anim.SetBool("VulnBool", true);
        }
        else if(!enemyAI.volnereble && anim.GetBool("VulnBool"))
        {
            anim.SetBool("VulnBool", false);
        }
    }
}
