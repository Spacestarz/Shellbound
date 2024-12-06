using System.Collections.Generic;
using UnityEngine;

public class GoblinSharkStateMachine : MonoBehaviour
{
    public List<Phase> phases;
    Phase phase;
    // Start is called before the first frame update
    void Awake()
    {
        phase = phases[0];
    }

    // Update is called once per frame
    void Update()
    {
        phase.PhaseStructure();
        // if boss hp > 50 phase = Phases[1]
    }
}
