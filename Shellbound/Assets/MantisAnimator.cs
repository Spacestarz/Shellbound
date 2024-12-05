using Spine.Unity.Examples;
using UnityEngine;
using UnityEngine.AI;

public class MantisAnimator : MonoBehaviour
{
    public GameObject enemyToControl;
    NavMeshAgent enemyAgent;
    Base_enemy enemyAI;
    Enemi_health enemyHealth;
    base_enemi_attack enemyAttack;

    [HideInInspector] public Animator anim;

    void Awake()
    {
        enemyAgent = enemyToControl.GetComponent<NavMeshAgent>();
        enemyAI = enemyToControl.GetComponent<Base_enemy>();
        enemyHealth = enemyToControl.GetComponent<Enemi_health>();
        enemyAttack = enemyToControl.GetComponentInChildren<base_enemi_attack>();

        anim = GetComponent<Animator>();
    }


    void Update()
    {

        if (enemyAI.volnereble && !anim.GetBool("Vulnerable"))
        {
            anim.SetBool("Vulnerable", true);
        }
        else if (!enemyAI.volnereble && anim.GetBool("Vulnerable"))
        {
            anim.SetBool("Vulnerable", false);
        }

        if (enemyHealth.Harponed && !anim.GetBool("Harpooned"))
        {
            anim.SetBool("Harpooned", true);
        }
        else if (!enemyHealth.Harponed && anim.GetBool("Harpooned"))
        {
            anim.SetBool("Harpooned", false);
        }

        if (enemyAttack.ElastickAnim && !anim.GetBool("Punch"))
        {
            anim.SetBool("Punch", true);
        }
        else if(!enemyAttack.ElastickAnim && anim.GetBool("Punch"))
        {
            anim.SetBool("Punch", false);
        }

        if (enemyAgent.velocity.magnitude > 0.1 && !anim.GetBool("Walking") && !anim.GetBool("Punch"))
        {
            anim.SetBool("Walking", true);
        }
        else if (enemyAgent.velocity.magnitude <= 0.1 && anim.GetBool("Walking"))
        {
            anim.SetBool("Walking", false);
        }
    }
}
