using Spine.Unity;
using UnityEngine;
using DG.Tweening;
using Spine;
using System.Collections;

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

    public static bool isSwitching;
    bool weaponInMiddle;

    Vector3 flatVelo;

    Spine.AnimationState animState;
    Spine.AnimationState lastState;

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

        CheckAnchorPos();
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
            weaponInMiddle = true;
            rect.DOAnchorPos(middlePos, 0.189f);
        }
        else if (!fire.fired && harpoonIsFired)
        {
            harpoonIsFired = false;
            if(!isSwitching)
            {
                ReturnToStill(ref harpoonIsFired);
            }
            CheckAnchorPos();
        }
    }


    void CheckDartFired()
    {
        if (dartFire.hasShot && !dartIsFired)
        {

            dartIsFired = true;
            skeletonGraphic.AnimationState.SetAnimation(0, "Dart bolt shot", false);
        }
        else if (!dartFire.hasShot && dartIsFired)
        {
            dartIsFired = false;
            if(!isSwitching)
            {
                ReturnToStill(ref dartIsFired);
            }
            CheckAnchorPos();
        }
    }


    void CheckPlayerWalking()
    {
        CheckAnchorPos();
        if (harpoonIsActive && !isSwitching)
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
        else if (!isSwitching)
        {
            if (flatVelo.magnitude > 0.1 && !playerIsWalking && !dartIsFired)
            {
                playerIsWalking = true;
                skeletonGraphic.AnimationState.SetAnimation(0, "Dart bolt walking bob", true);
            }
            else if (flatVelo.magnitude < 0.1 && playerIsWalking && !dartIsFired)
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
        weaponInMiddle = false;
        if(harpoonIsActive)
        {
            skeletonGraphic.AnimationState.SetAnimation(0, "Hook still", false);
        }
        else if(!PlayerSlice.instance.caughtObject)
        {
            skeletonGraphic.AnimationState.SetAnimation(0, "Dart bolt still", false);
            skeletonGraphic.AnimationState.GetCurrent(0).TimeScale = 3.0f;
        }

        CheckAnchorPos();
    }


    void CheckAnchorPos()
    {
        if (!PlayerSlice.SliceMode() && !weaponInMiddle && rect.anchoredPosition != (Vector2)originalPos)
        {
            rect.DOAnchorPos(originalPos, 0.189f);
        }
    }


    void CheckIfTimeToSwitch()
    {
        harpoonIsActive = controller.harpoontime;
        if(harpoonIsActive != harpoonWasActive)
        {
            StopCoroutine(SwitchTimer());
            StartCoroutine(SwitchTimer());
            if(controller.harpoontime)
            {
                SwitchToHarpoon();
                CheckAnchorPos();
            }
            else if(!controller.harpoontime && !PlayerSlice.SliceMode())
            {
                SwitchToDart();
                CheckAnchorPos();
            }
        }
        harpoonWasActive = harpoonIsActive;
    }


    void SwitchToHarpoon()
    {
        skeletonGraphic.AnimationState.SetAnimation(0, "Switch Dart bolt to Hook", false);
    }


    void SwitchToDart()
    {
        skeletonGraphic.AnimationState.SetAnimation(0, "Switch Hook to Dart bolt", false);
    }


    IEnumerator SwitchTimer()
    {
        isSwitching = true;
        weaponInMiddle = false;
        yield return new WaitForSeconds(0.4f);
        playerIsWalking = false;
        isSwitching = false;
    }
}
