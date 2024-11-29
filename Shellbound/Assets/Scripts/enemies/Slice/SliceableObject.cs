using UnityEngine;

public class SliceableObject : MonoBehaviour
{
    public enum Type
    {
        Enemy,
        Trigger
    }

    public Type type;

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
        switch (type)
        {
            case Type.Enemy:
                GetComponent<HealthSystem>().TakeDamage(5);
                break;
        }
    }
}
