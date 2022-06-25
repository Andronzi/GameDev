/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawn : MonoBehaviour
{
    private bool spawned = false;
    public  GameObject[] enemyTypes;
    public  Transform[] enemySpawns;
    public GameObject healthPottion;
    
    [HideInInspector] public List<GameObject> enamies;
   

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
                    
                }
                else if (rand == 9)
                {
                    Instantiate(healthPottion, spawner.position, Quaternion.identity);
                }
            }
        }

        
    }

    
}
*/
