using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weekpoint : MonoBehaviour
{
    Base_enemy enemy;
    Boss1_AI AI;
    public base_enemi_attack attack;
    MeshRenderer meshRenderer;
    Collider _collider;

    bool isVisible;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();
        enemy = GetComponentInParent<Base_enemy>();
        AI = GetComponentInParent<Boss1_AI>();
        //attack = transform.parent.parent.GetComponentInChildren<base_enemi_attack>();
    }

    private void Update()
    {
        if(meshRenderer)
        {
            if(AI.phase.stunable && !isVisible)
            {
                isVisible = true;

                meshRenderer.enabled = true;
                _collider.enabled = true;
            }
            else if(!AI.phase.stunable && isVisible)
            {
                isVisible = false;

                meshRenderer.enabled = false;
                _collider.enabled = false;

            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (AI.phase.stunable && collision.CompareTag("Dart"))
        {
            Debug.Log("shuld be week");
            enemy.wekend();
        }
    }
}
