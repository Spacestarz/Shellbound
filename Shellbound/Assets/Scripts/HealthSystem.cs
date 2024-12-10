using DG.Tweening;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float MaxHP;
    public float currentHP;
    public GameObject Player;
    public AudioSource source;
    public AudioClip audioClip;

    public UI uiScript;
    public SpriteRenderer enemySprite;


    // Start is called before the first frame update
    void Start()
    {      
        currentHP = MaxHP;
        source = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damageTaken)
    {
        currentHP -= damageTaken;
       
        source.PlayOneShot(audioClip, 0.2f);

        if (gameObject.CompareTag("Player") && currentHP <= 0)
        {
            PlayerDead();   
        }

        if (gameObject.CompareTag("Enemy") && currentHP <= 0)
        {
            if(GetComponent<HookableObject>().isCaught)
            {
                PlayerSlice.SetSliceMode(false);
                Camera.main.GetComponent<RotateCamera>().DOKill();
                Camera.main.GetComponent<RotateCamera>().isLocked = false;
            }

            Destroy(gameObject);
        }

    }

    public void Bossdead()
    {
       uiScript.DefeatedBOSS();
    }

    public void PlayerDead()
    {
        //Game over screen for player
        uiScript.GameOver();
    }
}
