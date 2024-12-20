using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Crowd_attacks : MonoBehaviour
{
    private GameObject player;
    public GameObject preFabCircle;
    private Vector3 unitCircleCenter;

    private float randomSpawnTime;

    public Boss1_AI Boss1_AI;
   
    /*
    //random range i float
    3-10 sekunder. Ffunkar bra!

    second fas
    ska vara lite snabbare.

    sista fasen ska det vara segare 
    typ 5-12 sekunder

    */

    void Start()
    {

        player = GameObject.Find("Player");
   
        StartCoroutine(nameof(Spawn));
    }
    
    IEnumerator Spawn()
    {
        while (Boss1_AI.activePhase == 0)
        {
            randomSpawnTime = Random.Range(1f, 3f);
            yield return new WaitForSeconds(randomSpawnTime);

            SpawnCircle();
        }  
        
        while (Boss1_AI.activePhase == 1)
        {
            randomSpawnTime = Random.Range(1f, 2f);
            yield return new WaitForSeconds(randomSpawnTime);

            SpawnCircle();
        }

        while (Boss1_AI.activePhase == 2)
        {
            randomSpawnTime = Random.Range(3f, 6f);
            yield return new WaitForSeconds(randomSpawnTime);

            SpawnCircle();
        }

    }

    private void SpawnCircle()
    {
        unitCircleCenter = new Vector3(player.transform.position.x, 0.04f, player.transform.position.z);

        Vector3 circleArea = Random.insideUnitSphere * 3f;

        Vector3 spawnPosition = new Vector3(circleArea.x, 0, circleArea.z);
        spawnPosition += unitCircleCenter;

        Instantiate(preFabCircle, spawnPosition, Quaternion.identity);//Euler(0, 0, 0)); //the rotation need to be in -90 degree
                                                                             //Only because the prefab was 90
    }
}








