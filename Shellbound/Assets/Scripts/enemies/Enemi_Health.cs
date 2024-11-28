using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemi_Health : HealthSystem
{
    bool Harponed = false;
    Base_enemy enemi;
    public float dragspeed = 5;
    Transform monster;
    Transform player;
    Transform harpon;
    float Distance;

    private void Awake()
    {
        monster = transform;
        player = GameObject.Find("Player").transform;
        harpon = GameObject.Find("PlayerHarpoon").transform;
    }
    public void DisableAI()
    {
        Harponed = true;
        enemi = GetComponent<Base_enemy>();
        enemi.stop();
        enemi.enabled = false;

    }
    public void EnableAI()
    {
        Harponed = false;
        enemi = GetComponent<Base_enemy>();
        enemi.enabled = true;
        enemi.start();
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
        else if (Harponed && !PlayerSlice.SliceMode())
        {
            PlayerSlice.SetSliceMode(true);
        }
    }
}
