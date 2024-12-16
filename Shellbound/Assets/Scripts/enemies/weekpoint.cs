using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weekpoint : MonoBehaviour
{
    Base_enemy enemy;
    Boss1_AI AI;
    public base_enemi_attack attack;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInParent<Base_enemy>();
        AI = GetComponentInParent<Boss1_AI>();
        attack = transform.parent.parent.GetComponentInChildren<base_enemi_attack>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (attack.WaveAnim)
        {
            Debug.Log("shuld be week");
            enemy.wekend();
        }
    }
}
