using UnityEngine;

public class HarpoonAnimator : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }


    public void Fire()
    {
        anim.SetTrigger("Fire");
    }

    public void Return()
    {
        anim.SetTrigger("Return");
    }
}
