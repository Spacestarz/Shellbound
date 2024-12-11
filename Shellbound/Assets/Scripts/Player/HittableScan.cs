using DG.Tweening;
using UnityEngine;

public class HittableScan : MonoBehaviour
{
    Fire fire;
    Vector3 rayOrigin;
    
    public LayerMask layerMask;
    public CrosshairSpinner crosshair;

    
    bool isHookable;

    void Awake()
    {
        fire = GetComponent<Fire>();
        rayOrigin = fire.Anchor.transform.position;
    }

    void Update()
    {
        rayOrigin = fire.Anchor.transform.position;
        CastRay();
    }

    void CastRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, ray.direction, out hit, fire.maxDistancefromAnchor, ~layerMask) && hit.collider.GetComponent<HookableObject>())
        {
            isHookable = hit.collider.GetComponent<HookableObject>().IsHookable();
        }
        else if(isHookable)
        {
            isHookable = false;
        }
        
        Debug.DrawRay(rayOrigin, ray.direction * fire.maxDistancefromAnchor, Color.yellow);

        SetCrosshairSpin();
    }
    

    void SetCrosshairSpin()
    {
        crosshair.SetSpinning(isHookable);
        //if(isHookable && !crosshair.isSpinning)
        //{
        //    crosshair.SetSpinning(true);
        //}
        //else if(!isHookable && crosshair.isSpinning)
        //{
        //    crosshair.SetSpinning(false);
        //    crosshair.ResetRotation();
        //}
    }
}
