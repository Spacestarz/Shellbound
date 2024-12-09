using UnityEngine;

public class Enemi_health : HealthSystem
{
    [HideInInspector] public bool Harponed = false;
    Base_enemy enemi;
    public float dragspeed = 5;
    Transform monster;
    Transform player;
    Transform harpon;
    float Distance;
    public AudioClip crack;

    private void Awake()
    {
        monster = transform;
        player = GameObject.Find("Player").transform;
        harpon = GameObject.Find("PlayerHarpoon").transform;
    }
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
    private void Update()
    {
        Distance = Vector3.Distance(monster.position, player.position);
        if (Harponed && Distance > 5f)
        {
            transform.position = Vector3.MoveTowards(monster.position, player.position, dragspeed * Time.deltaTime);
            harpon.position = Vector3.MoveTowards(harpon.position, player.position, dragspeed * Time.deltaTime);
            monster.GetComponent<Boss1_AI>().phase.resetpositon();
        }
    }
}
