using UnityEngine;

public abstract class Phase : BasePhaseScript
{
    //public abstract void PhaseStructure();
    public GameObject player;
    public GameObject goblinShark;
    public Boss2_attacks attacks;
    //place referances to attacks here
    public void Awake()
    {
        attacks = GetComponentInParent<Boss2_attacks>();
    }
}
