using UnityEngine;
using UnityEngine.AI;

public abstract class Phase : BasePhaseScript
{
    //public abstract void PhaseStructure();
    public GameObject player;
    public GameObject goblinShark;
    public Boss2_attacks attacks;
    //place referances to attacks here
    public void Awake()
    {
        goblinShark = transform.parent.parent.gameObject;
        attacks =goblinShark.GetComponent<Boss2_attacks>();
        enemy = goblinShark.GetComponent<Base_enemy>();
        agent =goblinShark.GetComponent<NavMeshAgent>();
    }
}
