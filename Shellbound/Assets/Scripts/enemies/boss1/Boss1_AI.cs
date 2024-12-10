using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_AI : MonoBehaviour
{
    //statemachine
    public List<BasePhaseScript> phases;
    public BasePhaseScript phase;
    Enemi_health health;
    Base_enemy enemy;
    public AudioClip PhaseSwitchSound;
    public bool PhaseSwitch = false;
    public int activePhase = 0;
    public float phase2HPPercent = 0.7f;
    public float phase3HPPercent = 0.3f;

    private void Awake()
    {
        phase = phases[activePhase];
        health = GetComponent<Enemi_health>();
        enemy = GetComponent<Base_enemy>();
    }
    public void Update()
    {
        phase.phase();
        if (health.currentHP < health.MaxHP * phase3HPPercent && phase == phases[1] && !PhaseSwitch)
        {
            //phase = phases[2];
            activePhase = 2;
            StartCoroutine(wait(4, activePhase));
        }
        else if (health.currentHP < health.MaxHP * phase2HPPercent && phase == phases[0] && !PhaseSwitch)
        {
            //phase = phases[1];
            activePhase = 1;
            StartCoroutine(wait(4, activePhase));
        }
    }

    IEnumerator wait(float time, int NewPhase)
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
    }

}
