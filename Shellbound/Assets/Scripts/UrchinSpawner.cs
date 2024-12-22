using System.Collections;
using System.Collections.Generic;
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
            int HowManyUrchinSpawn = UnityEngine.Random.Range(1, 3);
            SpawnUrchins(HowManyUrchinSpawn);
        }

        else if (bossAiScript.activePhase == 2)
        {
            int HowManyUrchinSpawn = UnityEngine.Random.Range(3, 5);
            SpawnUrchins(HowManyUrchinSpawn);
        }

       
    }

    public void SpawnUrchins(int urchinAmount)
    { 
        for (int i = 0; i < urchinAmount; i++)
        {
            //var unitCircleCenter = new Vector3(Player.transform.position.x, HeightofY, Player.transform.position.z);

            //Vector3 spawnRange = Random.insideUnitSphere * CircleArea;

            //Vector3 spawnPosition = new Vector3(spawnRange.x, 0, spawnRange.z);
            //spawnPosition += unitCircleCenter;


            Vector3 RandomPoint = Random.insideUnitCircle * CircleArea;

            Vector3 randomPositionInCircle = new Vector3(Player.transform.position.x + RandomPoint.x, HeightofY, Player.transform.position.z + RandomPoint.y);
            newUrchin = Instantiate(urchinPreFab, randomPositionInCircle, Quaternion.identity);
        }
    }
}
