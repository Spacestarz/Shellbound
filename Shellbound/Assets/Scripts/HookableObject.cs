using UnityEngine;
using UnityEngine.Events;

public class HookableObject : MonoBehaviour
{
    [SerializeField] private UnityEvent Event;

    public enum Type
    {
        Trigger,
        Other
    }
    public enum HookBehavior
    {
        Pull,
        Static,
        TakeDamage
    }

    public Type type;
    public HookBehavior hookBehavior;


    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetHit()
    {
        switch(type)
        {
            case Type.Trigger:

                break;
        }
    }
}
