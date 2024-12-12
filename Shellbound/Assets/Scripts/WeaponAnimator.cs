using Spine.Unity;
using UnityEngine;
using DG.Tweening;

public class WeaponAnimator : BaseAnimator
{
    SkeletonGraphic skeletonGraphic;
    [SerializeField] private Rigidbody playerRb;
    private Fire playerFire;

    RectTransform rect;
    Vector3 originalPos;
    Vector3 middlePos;

    bool playerIsWalking;
    bool harpoonIsFired;

    Vector3 flatVelo;

    // Start is called before the first frame update
    void Awake()
    {
        playerFire = playerRb.GetComponent<Fire>();
        
        skeletonGraphic = GetComponent<SkeletonGraphic>();
        
        rect = GetComponent<RectTransform>();
        originalPos = rect.anchoredPosition;
        middlePos = new Vector3(-400, 32, 0);
    }

    // Update is called once per frame
    void Update()
    {
        flatVelo = new Vector3(playerRb.velocity.x, 0, playerRb.velocity.z);
        CheckHarpoonFired();
        CheckPlayerWalking();
    }


    void CheckHarpoonFired()
    {
        if (playerFire.fired && !harpoonIsFired)
        {
            harpoonIsFired = true;
            
            skeletonGraphic.AnimationState.SetAnimation(0, "shothold", false);
            skeletonGraphic.AnimationState.GetCurrent(0).TimeScale = 3.0f;

            rect.DOAnchorPos(middlePos, 0.189f);
            //skeletonGraphic.AnimationState.AddAnimation(0, "shotstill", false, 0.063f);
            //Invoke(nameof(TriggerShotStill), 0.063f);
        }
        else if (!playerFire.fired && harpoonIsFired)
        {
            ReturnToStill(ref harpoonIsFired);
            rect.DOAnchorPos(originalPos, 0.189f);
        }
    }

    void CheckPlayerWalking()
    {
        if (flatVelo.magnitude > 0.1 && !playerIsWalking && !harpoonIsFired)
        {
            playerIsWalking = true;
            skeletonGraphic.AnimationState.SetAnimation(0, "bob", true);
        }
        else if (flatVelo.magnitude < 0.1 && playerIsWalking && !harpoonIsFired)
        {
            playerIsWalking = false;
            ReturnToStill(ref playerIsWalking);
        }
    }

    void ReturnToStill(ref bool animBool)
    {
        animBool = false;
        skeletonGraphic.AnimationState.SetAnimation(0, "still", false);
    }
}
