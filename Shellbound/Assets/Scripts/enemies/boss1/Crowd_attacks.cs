using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Crowd_attacks : MonoBehaviour
{
    private GameObject player;
    public GameObject preFabCircle;
    private Vector3 preFabCirclePosition;

    private float randomSpawnTime;

    public Boss1_AI Boss1_AI;
    public int BeenHit;
   
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
        preFabCirclePosition = new Vector3(player.transform.position.x, (float)0.04, player.transform.position.z);

        Instantiate(preFabCircle, preFabCirclePosition, Quaternion.Euler(-90, 0, 0)); //the rotation need to be in -90 degree
    }
}








