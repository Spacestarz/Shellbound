using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_AI : MonoBehaviour
{
    //statemachine
    public List<base_enemi_attack> phases;
    public base_enemi_attack phase;
    Enemi_health health;
    Base_enemy enemy;
    public AudioClip PhaseSwitchSound;
    public bool PhaseSwitch = false;

    private void Awake()
    {
        phase = phases[0];
        health = GetComponent<Enemi_health>();
        enemy = GetComponent<Base_enemy>();
    }
    public void Update()
    {
        phase.phase();
        if (health.currentHP < health.MaxHP * 0.3 && phase == phases[1] && !PhaseSwitch)
        {
            //phase = phases[2];
            StartCoroutine(wait(4, 2));
        }
        else if (health.currentHP < health.MaxHP * 0.7 && phase == phases[0] && !PhaseSwitch)
        {
            //phase = phases[1];
            StartCoroutine(wait(4, 1));
        }
    }

    IEnumerator wait(float time, int NewPhase)
    {
        enemy.stop();
        enemy.atta = false;
        PhaseSwitch = true;
        health.source.PlayOneShot(PhaseSwitchSound);
        //animation start line goes here I think
        enemy.GetComponentInChildren<MantisAnimator>().anim.SetTrigger("NewPhase");
        yield return new WaitForSeconds(time);
        phase = phases[NewPhase];
        PhaseSwitch = false;
        enemy.atta = true;
        enemy.start();
    }


}
