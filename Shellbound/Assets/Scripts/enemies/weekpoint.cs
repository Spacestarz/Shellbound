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
        //if(meshRenderer)
        //{
        //    if(AI.phase.stunable && !isVisible)
        //    {
        //        isVisible = true;

        //        meshRenderer.enabled = true;
        //        _collider.enabled = true;
        //    }
        //    else if(!AI.phase.stunable && isVisible)
        //    {
        //        isVisible = false;

        //        meshRenderer.enabled = false;
        //        _collider.enabled = false;

        //    }
        //}
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (AI.phase.stunable && collision.CompareTag("Dart"))
        {
            enemy.wekend();
            GetHit();
        }
    }

    public IEnumerator SingleShockwave()
    {
        yield return new WaitForSeconds(2.1f);
        Show();
        yield return new WaitForSeconds(1.1f);
        Hide();
        yield break;
    }

    public IEnumerator DoubleShockwave()
    {
        yield return new WaitForSeconds(1.3f);
        Show();
        yield return new WaitForSeconds(0.7f);
        Hide();
        yield return new WaitForSeconds(0.2f);
        Show();
        yield return new WaitForSeconds(0.8f);
    }

    public IEnumerator Fist()
    {
        yield return new WaitForSeconds(0.9f);
        Show();
        yield return new WaitForSeconds(0.7f);
        Hide();
    }

    private void Show()
    {
        //meshRenderer.enabled = true;
        _collider.enabled = true;
    }


    private void Hide()
    {
        meshRenderer.enabled = false;
        _collider.enabled = false;
    }

    public void GetHit()
    {
        StopAllCoroutines();
        Hide();
    }

    void MoveCollider()
    {

    }
}
