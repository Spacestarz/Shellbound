using Spine.Unity;
using UnityEngine;
using UnityEngine.AI;

public class MantisAnimator : MonoBehaviour
{
    public GameObject enemyToControl;
    NavMeshAgent enemyAgent;
    Base_enemy enemyAI;
    Enemi_health enemyHealth;

    [HideInInspector] Animator anim;

    void Awake()
    {
        enemyAgent = enemyToControl.GetComponent<NavMeshAgent>();
        enemyAI = enemyToControl.GetComponent<Base_enemy>();
        enemyHealth = enemyToControl.GetComponent<Enemi_health>();

        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (enemyAgent.velocity.magnitude > 0.1 && !anim.GetBool("Walking"))
        {
            anim.SetBool("Walking", true);
        }
        else if (enemyAgent.velocity.magnitude <= 0.1 && anim.GetBool("Walking"))
        {
            anim.SetBool("Walking", false);
        }

        if(enemyAI.volnereble && !anim.GetBool("Vulnerable"))
        {
            anim.SetBool("Vulnerable", true);
        }
        else if(!enemyAI.volnereble && anim.GetBool("Vulnerable"))
        {
            anim.SetBool("Vulnerable", false);
        }
    }
}
