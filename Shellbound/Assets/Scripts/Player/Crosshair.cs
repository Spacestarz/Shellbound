using UnityEngine;

public class Crosshair : MonoBehaviour
{
    Fire fire;
    public LayerMask layerMask;

    Vector3 rayOrigin;
    Vector3 rayDirection;
    
    bool isHookable;

    void Awake()
    {
        fire = GetComponent<Fire>();
        rayOrigin = fire.Anchor.transform.position;
    }

    void Update()
    {
        rayOrigin = fire.Anchor.transform.position;
        //rayDirection = Camera.main.transform.forward;
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
    }
    
    void SpinCrosshair()
    {

    }
}
