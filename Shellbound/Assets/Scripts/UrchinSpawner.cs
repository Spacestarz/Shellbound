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

    [SerializeField] float HeightofY = 2;
    [SerializeField] float CircleArea = 13;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        Boss = GameObject.Find("MantisShrimp");
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            SpawnUrchins();
        }
    }

    public void SpawnUrchins()
    {
        int HowManyUrchinSpawn = UnityEngine.Random.Range(3, 5);

        for (int i = 0; i < HowManyUrchinSpawn; i++)
        {
            Vector3 RandomPoint = UnityEngine.Random.insideUnitCircle * CircleArea;

            Vector3 randomPositionInCircle = new Vector3(Player.transform.position.x + RandomPoint.x, HeightofY, Player.transform.position.z + RandomPoint.y);
            newUrchin = Instantiate(urchinPreFab, randomPositionInCircle, Quaternion.identity);
        }

    }

}
