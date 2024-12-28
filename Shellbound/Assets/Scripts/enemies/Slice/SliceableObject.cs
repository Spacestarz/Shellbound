using UnityEditor.UI;
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
        Trigger,
        Sushi
    }

    public Type type;
    [SerializeField] private UnityEvent<int> testEvent;

    public void Awake()
    {
        ShrimpCrowdScript = FindAnyObjectByType<ShrimpCrowd>();
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
            case Type.Sushi:
                testEvent.Invoke(sliceBoard.totalSliced);
                break;
        }
        
    }

    public void FinalSlice()
    {
        switch (type)
        {
            case Type.Enemy:
                GetComponent<HealthSystem>().TakeDamage(5);
                GetComponent<Enemi_health>().source.PlayOneShot(slicefinish, 0.7f);
                if(ShrimpCrowdScript)
                {
                    ShrimpCrowdScript.Cheer(); 
                }
                break;
            case Type.Trigger:
                testEvent.Invoke(0);
                break;
        }
    }
}
