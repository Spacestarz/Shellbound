using UnityEngine;

public class SliceableObject : MonoBehaviour
{
    public SlicePattern sliceBoard;

    public enum Type
    {
        Enemy,
        Trigger
    }

    public Type type;

    public void SingleSlice()
    {
        Debug.Log("SingleSlice");
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
        Debug.Log("FinalSlice");
        switch (type)
        {
            case Type.Enemy:
                GetComponent<HealthSystem>().TakeDamage(5);
                break;
        }
    }
}
