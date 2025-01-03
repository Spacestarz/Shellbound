using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class IntroManager : MonoBehaviour
{
    public static IntroManager instance;
    public static bool isRunning = false;

    public Image blackPanel;
    private Crowd_attacks crowdAttack;

    public GameObject spike;
    public GameObject nameCard;

    private GameObject boss;

    public RectTransform enemyHPBar;
    public RectTransform playerHPBar;
    public RectTransform playerWeapon;
    WeaponAnimator weaponAnimator;

    private BossTimer timer;

    Vector3 enemyHPPosition;
    Vector3 playerHPPosition;
    Vector3 playerWeaponPosition;

    public AudioClip bossRoar;

    void Start()
    {
        isRunning = true;

        if (instance == null)
        {
            instance = this;
        }
        timer = GameObject.Find("BossTimer").GetComponentInChildren<BossTimer>();

        weaponAnimator = instance.playerWeapon.GetComponent<WeaponAnimator>();
        weaponAnimator.enabled = false;

        boss = GameObject.Find("MantisShrimp");
        boss.GetComponent<Boss1_AI>().enabled = false;

        blackPanel.gameObject.SetActive(true);
        blackPanel.color = Color.black;

        crowdAttack = FindAnyObjectByType<Crowd_attacks>();
        crowdAttack.gameObject.SetActive(false);

        instance.crowdAttack.gameObject.SetActive(false);

        StoreUIPositions();
        HideUIElements();

        DisableControls();
        Camera.main.GetComponent<RotateCamera>().IntroStareAtFloor();

        Invoke(nameof(BlackFadeOut), 0.5f);
        Invoke(nameof(LookUp), 4);
    }

    private void Update()
    {
        if (isRunning && Input.GetKeyDown(KeyCode.Escape))
        {
            SceneController.instance.LoadScene("NoIntro");
        }
    }

    private void StoreUIPositions()
    {
        enemyHPPosition = enemyHPBar.anchoredPosition;
        playerHPPosition = playerHPBar.anchoredPosition;
        playerWeaponPosition = playerWeapon.anchoredPosition;
    }

    private void HideUIElements()
    {
        enemyHPBar.anchoredPosition = new Vector2(0, 0);
        playerHPBar.anchoredPosition = new Vector2(-20, -40);
        playerWeapon.anchoredPosition = new Vector2(496, -470);
    }

    private void DisableControls()
    {
        Camera.main.transform.parent.GetComponent<PlayerController>().enabled = false;
        Camera.main.GetComponent<RotateCamera>().isLocked = true;
    }


    private void BlackFadeOut()
    {
        Camera.main.GetComponent<CameraHandler>().isDisorientedFOV = true;
        blackPanel.DOColor(Color.clear, 3f);
    }

    private void LookUp()
    {
        Camera.main.GetComponent<RotateCamera>().IntroLookAtBoss();
    }


    public static void SpawnFirstSpike()
    {
        var spike = Instantiate(instance.spike, new(20, 0f, 16), Quaternion.identity);
        spike.GetComponent<AudioSource>().spatialBlend = 0f;
        spike.GetComponent<AudioSource>().panStereo = 0.75f;
        instance.Invoke(nameof(SpawnSecondSpike), 0.28f);
    }


    private void SpawnSecondSpike()
    {
        var spike = Instantiate(instance.spike, new(15.5f, 0f, 20), Quaternion.identity);
        spike.GetComponent<AudioSource>().spatialBlend = 0f;
        spike.GetComponent<AudioSource>().panStereo = -0.75f;
        Invoke(nameof(BossRoar), 3f);

    }


    private void BossRoar()
    {
        instance.boss.GetComponent<AudioSource>().spatialBlend = 0;
        instance.boss.GetComponentInChildren<Animator>().SetTrigger("NewPhase");
        instance.boss.GetComponent<AudioSource>().PlayOneShot(instance.bossRoar, 0.5f);
        Invoke(nameof(WeakRoarShake), 1.3f);
        Invoke(nameof(RoarCameraShake), 2f);
    }

    private void WeakRoarShake()
    {
        Image[] imgs = instance.nameCard.GetComponentsInChildren<Image>();
        foreach (Image image in imgs)
        {
            image.DOFade(1, 0.4f);
        }
        Camera.main.GetComponent<CameraHandler>().WeakBossRoar();
    }

    private void RoarCameraShake()
    {
        Camera.main.GetComponent<CameraHandler>().IntroBossRoar(this);
    }

    public void ResetSpatialBlend()
    {
        instance.boss.GetComponent<AudioSource>().spatialBlend = 1;
    }

    public void InvokeRestoreUIElements()
    {
        instance.Invoke(nameof(RestoreUIElements), 1.75f);
    }


    private void RestoreUIElements()
    {
        Image[] imgs = instance.nameCard.GetComponentsInChildren<Image>();
        foreach (Image image in imgs)
        {
            image.DOFade(0, 0.8f);
        }

        RestorePlayerWeapon();
        instance.Invoke(nameof(RestorePlayerHP), 0.2f);
        instance.Invoke(nameof(RestoreEnemyHP), 0.4f);
    }

    private void RestorePlayerWeapon()
    {
        playerWeapon.DOAnchorPos(playerWeaponPosition, 0.75f);
    }

    private void RestorePlayerHP()
    {
        playerHPBar.DOAnchorPos(playerHPPosition, 0.75f);
    }

    private void RestoreEnemyHP()
    {
        enemyHPBar.DOAnchorPos(enemyHPPosition, 0.75f).OnComplete(StartGame);
    }


    public void StartGame()
    {
        boss.GetComponent<Boss1_AI>().enabled = true;

        weaponAnimator.enabled = true;

        crowdAttack.gameObject.SetActive(true);
        crowdAttack.Awake();

        Invoke(nameof(ControlUnlock), 0.5f);
        isRunning = false;
    }


    void ControlUnlock()
    {
        ControlsTutorial.instance.StartTutorial();

        Camera.main.GetComponent<RotateCamera>().isLocked = false;
        Camera.main.transform.parent.GetComponent<PlayerController>().enabled = true;

        timer.TimerRunning = true;
    }
}
