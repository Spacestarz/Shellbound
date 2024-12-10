using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2AI : MonoBehaviour
{
    //statemachine
    public List<Phase> phases;
    Phase phase;
    Enemi_health health;
    Base_enemy enemy;
    public bool PhaseSwitch = false;
    public int activePhase = 0;
    // Start is called before the first frame update
    void Start()
    {
        phase = phases[activePhase];
    }

    // Update is called once per frame
    void Update()
    {
        phase.phase();
        if (health.currentHP < health.MaxHP / 0.3)
        {
            activePhase = 2;
            phase = phases[activePhase];
        }
        else if (health.currentHP < health.MaxHP / 0.6)
        {
            activePhase = 1;
            phase = phases[activePhase];
        }
        // if boss hp > 50 phase = Phases[1]
    }
    /*IEnumerator wait(float time, int NewPhase)
    {
        enemy.stop();
        enemy.atta = false;
        PhaseSwitch = true;
        health.source.PlayOneShot(PhaseSwitchSound);
        enemy.GetComponentInChildren<MantisAnimator>().anim.SetTrigger("NewPhase");
        yield return new WaitForSeconds(time);
        phase = phases[NewPhase];
        PhaseSwitch = false;
        enemy.atta = true;
        enemy.start();
    }*/
}
