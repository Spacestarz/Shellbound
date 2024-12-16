using UnityEngine;

public class Harpoon : MonoBehaviour
{
    SpriteRenderer sr;
    MovingLine line;

    public static Harpoon instance;
    
    public HookableObject caughtObject;
    public Fire fire;

    public bool collisionHIT = false;
    public static bool hasCaught;

    public ParticleSystem HitVFX;

    private void Awake()
    {
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
        if (fire.goingAway && other.GetComponent<HookableObject>())
        {
            other.GetComponent<HookableObject>().GetHit();
            HitVFX.Stop();
            HitVFX.Play();
            Debug.Log("VFX HIT");
        }
        else if (fire.goingAway)
        {
            HitVFX.Stop();
            HitVFX.Play();
            Debug.Log("test vfx");
            fire.ReturnHarpoon();
        }
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
