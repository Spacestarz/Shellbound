using UnityEngine;
using UnityEngine.AI;

public abstract class Phase : BasePhaseScript
{
    //public abstract void PhaseStructure();
    
    public GameObject goblinShark;
    public Boss2_attacks attacks;
    [Header("Dash")]
    public float DashDistance = 10;
    public float DashDuration = 2;
    [Header("Mouth")]
    public float MouthSpeed = 20;
    public float MouthReturn = 1;
    public float MouthDistance = 15;
    [Header("shockwave")]
    public float WaveDistance = 28;
    public float WaveDuration = 2;
    [Header("Kirby")]
    public float kurbyrange;
    //place referances to attacks here
    public void Awake()
    {
        goblinShark = transform.parent.parent.gameObject;
        attacks =goblinShark.GetComponent<Boss2_attacks>();
        enemy = goblinShark.GetComponent<Base_enemy>();
        agent =goblinShark.GetComponent<NavMeshAgent>();
    }
    
}
