using Spine.Unity;
using UnityEngine;

public class WeaponAnimator : BaseAnimator
{
    SkeletonGraphic skeletonGraphic;
    [SerializeField] private Rigidbody playerRb;
    private Fire playerFire;

    bool playerIsWalking;
    bool harpoonIsFired;

    Vector3 flatVelo;

    // Start is called before the first frame update
    void Awake()
    {
        skeletonGraphic = GetComponent<SkeletonGraphic>();
        playerFire = playerRb.GetComponent<Fire>();
    }

    // Update is called once per frame
    void Update()
    {
        flatVelo = new Vector3(playerRb.velocity.x, 0, playerRb.velocity.z);
        CheckHarpoonFired();
        CheckPlayerWalking();
    }

    void CheckPlayerWalking()
    {
        if (flatVelo.magnitude > 0.1 && !playerIsWalking && !harpoonIsFired) 
        {
            skeletonGraphic.AnimationState.GetCurrent(0).TimeScale = 1;
            playerIsWalking = true;
            skeletonGraphic.AnimationState.SetAnimation(0, "bob", true);
        }
        else if (flatVelo.magnitude < 0.1 && playerIsWalking && harpoonIsFired)
        {
            playerIsWalking = false;
            skeletonGraphic.AnimationState.GetCurrent(0).TimeScale = 0;
        }
    }

    void CheckHarpoonFired()
    {
        if(playerFire.fired && !harpoonIsFired)
        {
            skeletonGraphic.AnimationState.GetCurrent(0).TimeScale = 1;
            harpoonIsFired = true;
            skeletonGraphic.AnimationState.SetAnimation(0, "shot", false);
        }
        else if(!playerFire && harpoonIsFired)
        {
            harpoonIsFired = false;
            skeletonGraphic.AnimationState.GetCurrent(0).TimeScale = 0;
        }
    }
}
