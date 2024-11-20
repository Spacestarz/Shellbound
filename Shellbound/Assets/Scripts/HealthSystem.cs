using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public float MaxHP;
    public float currentHP;

    // Start is called before the first frame update
    void Start()
    {      
        currentHP = MaxHP;
    }

    public void TakeDamage(int damageTaken)
    {
        currentHP -= damageTaken;
       
        Debug.Log(currentHP + "/" + MaxHP);

        if (currentHP <= 0)
        {
            
            Debug.Log("Boy's dead");  
        }
    }
}
