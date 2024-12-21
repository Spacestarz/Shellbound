using Spine.Unity;
using UnityEngine;
using DG.Tweening;
using Spine;

public class WeaponAnimator : BaseAnimator
{
    SkeletonGraphic skeletonGraphic;
    [SerializeField] private Rigidbody playerRb;

    private Fire fire;
    PlayerController controller;
    private FireDart dartFire;

    RectTransform rect;
    Vector3 originalPos;
    Vector3 middlePos;

    bool playerIsWalking;

    bool harpoonIsFired;
    bool dartIsFired;

    bool harpoonWasActive;
    public bool harpoonIsActive;

    Vector3 flatVelo;

    // Start is called before the first frame update
    void Awake()
    {
        fire = playerRb.GetComponent<Fire>();
        controller = playerRb.GetComponent<PlayerController>();
        dartFire = playerRb.GetComponent<FireDart>();

        skeletonGraphic = GetComponent<SkeletonGraphic>();
        
        rect = GetComponent<RectTransform>();
        originalPos = rect.anchoredPosition;
        middlePos = new Vector3(-0, -208, 0);
    }

    void Update()
    {
        flatVelo = new Vector3(playerRb.velocity.x, 0, playerRb.velocity.z);

        CheckIfTimeToSwitch();

        CheckPlayerWalking();

        if(harpoonIsActive)
        {
            CheckHarpoonFired();
        }
        else
        {
            CheckDartFired();
        }
    }


    void CheckHarpoonFired()
    {
        if (fire.fired && !harpoonIsFired)
        {
            harpoonIsFired = true;
            
            skeletonGraphic.AnimationState.SetAnimation(0, "Hook shot", false);
            skeletonGraphic.AnimationState.GetCurrent(0).TimeScale = 3.0f;
            
            Invoke(nameof(HasShot), 0.189f);
            rect.DOAnchorPos(middlePos, 0.189f);
        }
        else if (!fire.fired && harpoonIsFired)
        {
            harpoonIsFired = false;
            ReturnToStill(ref harpoonIsFired);
            rect.DOAnchorPos(originalPos, 0.189f);
        }
    }


    void CheckDartFired()
    {
        if (dartFire.hasShot && !dartIsFired)
        {
            dartIsFired = true;

            //skeletonGraphic.AnimationState.SetAnimation(0, "no Hook or Dart bolt", false);
            //skeletonGraphic.AnimationState.SetAnimation(0, "Dart bolt shot", false);
            //skeletonGraphic.AnimationState.GetCurrent(0).TimeScale = 3.0f;

            //Invoke(nameof(HasShot), 0.189f);
            //rect.DOAnchorPos(middlePos, 0.189f);
        }
        else if (!dartFire.hasShot && dartIsFired)
        {
            dartIsFired = false;
            ReturnToStill(ref harpoonIsFired);
            //rect.DOAnchorPos(originalPos, 0.189f);
        }
    }

    void CheckPlayerWalking()
    {
        if (harpoonIsActive)
        {
            if (flatVelo.magnitude > 0.1 && !playerIsWalking && !harpoonIsFired)
            {
                playerIsWalking = true;
                skeletonGraphic.AnimationState.SetAnimation(0, "Hook walking bob", true);
            }
            else if (flatVelo.magnitude < 0.1 && playerIsWalking && !harpoonIsFired)
            {
                ReturnToStill(ref playerIsWalking);
                playerIsWalking = false;
            }
        }
        else
        {
            if (flatVelo.magnitude > 0.1 && !playerIsWalking && !harpoonIsFired)
            {
                playerIsWalking = true;
                skeletonGraphic.AnimationState.SetAnimation(0, "Dart bolt walking bob", true);
            }
            else if (flatVelo.magnitude < 0.1 && playerIsWalking && !harpoonIsFired)
            {
                playerIsWalking = false;
                ReturnToStill(ref playerIsWalking);
            }
        }
    }

    void HasShot()
    {
        skeletonGraphic.AnimationState.SetAnimation(0, "no Hook or Dart bolt", true);
    }

    void ReturnToStill(ref bool animBool)
    {
        animBool = false;
        if(harpoonIsActive)
        {
            skeletonGraphic.AnimationState.SetAnimation(0, "Hook still", false);
        }
        else if(!PlayerSlice.instance.caughtObject)
        {
            skeletonGraphic.AnimationState.SetAnimation(0, "Dart bolt still", false);
            skeletonGraphic.AnimationState.GetCurrent(0).TimeScale = 3.0f;
        }

        CheckIfReturn();
    }


    void CheckIfReturn()
    {
        if (rect.anchoredPosition != (Vector2)originalPos)
        {
            rect.DOAnchorPos(originalPos, 0.189f);
        }
    }


    void CheckIfTimeToSwitch()
    {
        harpoonIsActive = controller.harpoontime;
        if(harpoonIsActive != harpoonWasActive)
        {
            if(controller.harpoontime)
            {
                SwitchToHarpoon();
                CheckIfReturn();
            }
            else if(!controller.harpoontime && !PlayerSlice.instance.caughtObject)
            {
                SwitchToDart();
                CheckIfReturn();
            }
        }
        harpoonWasActive = harpoonIsActive;
    }

    public void SwitchToHarpoon()
    {
        skeletonGraphic.AnimationState.SetAnimation(0, "Switch Dart bolt to Hook", false);
    }

    public void SwitchToDart()
    {
        skeletonGraphic.AnimationState.SetAnimation(0, "Switch Hook to Dart bolt", false);
    }
}
