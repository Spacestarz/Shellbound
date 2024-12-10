using UnityEngine;

public class Harpoon : MonoBehaviour
{
    SpriteRenderer sr;
    Rigidbody rb;
    MovingLine line;

    public static Harpoon instance;
    
    public HookableObject caughtObject;
    public Fire fire;

    public bool collisionHIT = false;
    public static bool hasCaught;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        sr = GetComponentInChildren<SpriteRenderer>();
        line = GetComponentInChildren<MovingLine>();
        sr.enabled = false;
        hasCaught = false;

        if (instance == null)
        {
            instance = this;
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        if (fire.goingAway && other.GetComponent<HookableObject>())// && other.CompareTag("Enemy") || other.CompareTag("weakpoint") || other.CompareTag("TutorialHookThis"))
        {
            other.GetComponent<HookableObject>().GetHit();
        }
        else if (fire.goingAway)
        {
            fire.ReturnHarpoon();
        }
    }


    void HarpoonHit(Collider other)
    {
        other.GetComponent<HookableObject>().GetHit();

        //if (other.CompareTag("Enemy") && !other.GetComponent<Base_enemy>().volnereble)
        //{
        //    fire.ReturnHarpoon();
        //}
        //else
        //{
        //    fire.goingAway = false;
        //    collisionHIT = true;

        //    SetVisibility(false);
            
        //}

        //if (other.CompareTag("Enemy") && other.GetComponent<Base_enemy>().volnereble)
        //{
        //    caughtObject.GetComponent<Enemi_health>().DisableAI();
        //    caughtObject.GetComponent<Base_enemy>().StopWeakTimer();
        //}

        //else if (other.CompareTag("weakpoint") && fire.goingAway)
        //{
        //    fire.ReturnHarpoon();
        //}
    }

    public void SetVisibility(bool visibility)
    {
        sr.enabled = visibility;
        if (!hasCaught)
        {
            line.SetVisible(visibility);
        }
    }

    public static void SetCaughtObject(HookableObject hookableObject)
    {
        instance.caughtObject = hookableObject;
        PlayerSlice.SetCaughtObject(instance.caughtObject);
        hasCaught = true;

    }
}
