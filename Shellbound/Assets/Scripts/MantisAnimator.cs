using UnityEngine;
using UnityEngine.AI;

public class MantisAnimator : BaseAnimator
{
    public GameObject enemyToControl;
    NavMeshAgent enemyAgent;
    Base_enemy enemyAI;
    Enemi_health enemyHealth;
    Boss1_AI bossAI;
    base_enemi_attack attack;


    void Awake()
    {
        enemyAgent = enemyToControl.GetComponent<NavMeshAgent>();
        enemyAI = enemyToControl.GetComponent<Base_enemy>();
        enemyHealth = enemyToControl.GetComponent<Enemi_health>();
        bossAI = enemyToControl.GetComponent<Boss1_AI>();
        attack = enemyToControl.GetComponentInChildren<base_enemi_attack>();

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
            if(!attack.WaveAnim)
            {
                anim.SetBool("Walking", true);
            }
            else
            {
                anim.SetBool("Walking", false);
            }
        }
        else if (enemyAgent.velocity.magnitude <= 0.1 && anim.GetBool("Walking") && attack.WaveAnim)
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
            anim.SetBool("Walking", false);
        }
        else if(!enemyAI.volnereble && anim.GetBool("VulnBool"))
        {
            anim.SetBool("VulnBool", false);
        }
    }
}
