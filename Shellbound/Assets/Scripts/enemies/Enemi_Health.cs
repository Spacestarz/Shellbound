using UnityEngine;

public class Enemi_health : HealthSystem
{
    [HideInInspector] public bool Harponed = false;
    Base_enemy enemi;
    public float dragspeed = 5;

    public AudioClip crack;

    public void DisableAI()
    {
        if (GetComponent<Base_enemy>() != null)
        {
            Harponed = true;
            enemi = GetComponent<Base_enemy>();
            enemi.stop();
            enemi.enabled = false;
            source.PlayOneShot(crack);
        }

    }
    public void EnableAI()
    {
        if (GetComponent<Base_enemy>() != null)
        {
            Harponed = false;
            enemi = GetComponent<Base_enemy>();
            enemi.enabled = true;
            enemi.start();
        }
    }
}
