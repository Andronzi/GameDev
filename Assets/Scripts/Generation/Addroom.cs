using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Addroom : MonoBehaviour
{
    [Header("Walls")]
    public GameObject[] walls;
    public GameObject[] door;
    
    [Header("Enemy")]
    public  GameObject[] enemyTypes;
    public  Transform[] enemySpawns;

    public GameObject healthPottion;

    [HideInInspector] public List<GameObject> enamies;

    private RoomVariants _variants;
    private bool spawned;
    private bool wallDestoyed;

    public void Start()
    {
        _variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !spawned)
        {
            spawned = true;
            foreach (Transform spawner in enemySpawns)
            {
                int rand = Random.Range(0, 11);
                if (rand < 9)
                {
                    GameObject enemyType = enemyTypes[Random.Range(0, enemyTypes.Length)];
                    GameObject enemy = Instantiate(enemyType,spawner.position,Quaternion.identity) as GameObject;
                    enamies.Add(enemy);
                }
                else if (rand == 9)
                {
                    Instantiate(healthPottion, spawner.position, Quaternion.identity);
                }
            }
        }

        StartCoroutine(CheakEnemys());
    }

     IEnumerator CheakEnemys()
     {
         yield return new WaitForSeconds(1f);
         yield return new WaitUntil(() => enamies.Count == 0);
     }

     public void DestroyWalls()
     {
         foreach (GameObject wall in walls)
         {
             if (wall != null && wall.transform.childCount != 0)
             {
                 Destroy(wall);
             }
         }
         wallDestoyed = true;
     }

     private void OnTriggerStay2D(Collider2D other)
     {
         if (wallDestoyed && other.CompareTag("Wall"))
         {
             Destroy(other.gameObject);
         }
     }
}
