using UnityEngine;
using UnityEngine.Events;

public class HookableObject : MonoBehaviour
{
    public enum Type
    {
        Trigger,
        Enemy,
        Other
    }
    public enum HookBehavior
    {
        Pull,
        Static,
        TakeDamage
    }
    
    [SerializeField] private UnityEvent Event;

    private Vector3 caughtLocation = Vector3.zero;
    private Transform player;
    private Fire fire;

    private float distance;
    
    public Type type;
    public HookBehavior hookBehavior;
    
    [HideInInspector] public SliceableObject sliceableObject;
    public float pullSpeed = 5;
    public bool isCaught;


    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        fire = player.GetComponent<Fire>();
        sliceableObject = GetComponent<SliceableObject>();
    }


    void Update()
    {
        caughtLocation = transform.position;
        distance = Vector3.Distance(caughtLocation, player.position);
        if (isCaught && distance > 5f)
        {
            transform.position = Vector3.MoveTowards(caughtLocation, player.position, pullSpeed * Time.deltaTime);
            
            GetComponent<Boss1_AI>().phase.resetpositon();
        }
    }


    public void GetHit()
    {
        switch (type)
        {
            case Type.Trigger:
                Event.Invoke();
                break;

            default:
                break;
        }

        switch (hookBehavior)
        {
            case HookBehavior.Pull:
                GetCaught();
                break;

            case HookBehavior.TakeDamage:
                GetComponent<Enemi_health>().TakeDamage(1);
                break;

            case HookBehavior.Static:
                fire.ReturnHarpoon();
                break;
        }
    }


    void GetCaught()
    {
        if (type == Type.Enemy)
        {
            if (GetComponent<Base_enemy>().volnereble)
            {
                GetComponent<Enemi_health>().DisableAI();
                GetComponent<Base_enemy>().StopWeakTimer();
                
                Harpoon.SetCaughtObject(this);
                isCaught = true;
                fire.goingAway = false;
            }
            else
            {
                fire.ReturnHarpoon();
            }
        }
        else
        {
            Harpoon.SetCaughtObject(this);
            isCaught = true;
            fire.goingAway = false;
        }
    }


    public void Uncaught()
    {
        isCaught = false;
    }


    public bool IsHookable()
    {
        if(!isCaught)
        {
            if (type == Type.Enemy && GetComponent<Base_enemy>().volnereble)
            {
                return true;
            }
            else if(type == Type.Enemy)
            {
                return false;
            }

            return true;
        }
        else
        {
            return false;
        }
    }
}
