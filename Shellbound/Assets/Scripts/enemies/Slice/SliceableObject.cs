using UnityEngine;
using UnityEngine.Events;

public class SliceableObject : MonoBehaviour
{
    public SlicePattern sliceBoard;
    public int sliceAmount = 3;
    public AudioClip slicefinish;

    
    public ShrimpCrowd ShrimpCrowdScript;

    public enum Type
    {
        Enemy,
        Trigger
    }

    public Type type;
    [SerializeField] private UnityEvent testEvent;

    public void Awake()
    {
        sliceBoard = GetComponentInChildren<SlicePattern>();
        sliceBoard.sliceAmount = sliceAmount;
    }

    public void SingleSlice()
    {
        switch(type)
        {
            case Type.Enemy:
                GetComponent<HealthSystem>().TakeDamage(1);
                break;
            case Type.Trigger:
                break;
        }
        
    }

    public void FinalSlice()
    {
        if(ShrimpCrowdScript)
        {
            ShrimpCrowdScript.Cheer(); 
        }

        switch (type)
        {
            case Type.Enemy:
                GetComponent<HealthSystem>().TakeDamage(5);
                GetComponent<Enemi_health>().source.PlayOneShot(slicefinish);
                break;
            case Type.Trigger:
                testEvent.Invoke();
                break;
        }
    }
}
