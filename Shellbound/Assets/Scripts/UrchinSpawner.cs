using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class UrchinSpawner : MonoBehaviour
{
    public GameObject urchinPreFab;
    private GameObject Player;
    private GameObject Boss;
    private GameObject newUrchin;
    private Boss1_AI bossAiScript;
    private int HowManyUrchinSpawn;

    [SerializeField] float HeightofY = 2;
    [SerializeField] float CircleArea = 13;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        Boss = GameObject.Find("MantisShrimp");
        bossAiScript = Boss.GetComponent<Boss1_AI>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            WhichPhaseForUrchin();
        }      
    }

    public void WhichPhaseForUrchin()
    {
        if (bossAiScript.activePhase == 1)
        {
            Debug.Log("1-2 urchin should spawn;");
            int HowManyUrchinSpawn = UnityEngine.Random.Range(1, 3);
            SpawnUrchins(HowManyUrchinSpawn);
        }

        else if (bossAiScript.activePhase == 2)
        {
            Debug.Log("3-5 urchin should spawn;");
            int HowManyUrchinSpawn = UnityEngine.Random.Range(3, 5);
            SpawnUrchins(HowManyUrchinSpawn);
        }

       
    }

    public void SpawnUrchins(int urchinAmount)
    { 
        for (int i = 0; i < urchinAmount; i++)
        {
            Vector3 RandomPoint = UnityEngine.Random.insideUnitCircle * CircleArea;

            Vector3 randomPositionInCircle = new Vector3(Player.transform.position.x + RandomPoint.x, HeightofY, Player.transform.position.z + RandomPoint.y);
            newUrchin = Instantiate(urchinPreFab, randomPositionInCircle, Quaternion.identity);
        }
    }
}
