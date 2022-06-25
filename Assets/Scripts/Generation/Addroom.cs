using System;
using System.Collections;
using System.Collections.Generic;
using EnemyLogic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Generation
{
    public class Addroom : MonoBehaviour
    {
        [Header("Walls")] 
        public GameObject[] walls;
        public GameObject[] door;
    
        [Header("Enemy")]
        public  GameObject[] enemyTypes;
        public  Transform[] enemySpawns; 

        public GameObject healthPottion;

        public List<GameObject> enemys;

        private bool _spawned;
        private bool _wallDestoyed;
        
        private bool flag = true;
        
        public void Start()
        {
            GameObject.FindWithTag("Rooms").GetComponent<RoomVariants>();
        }

        /*public void Update()
        {
            if (flag)
            {
                var flag1 = true;
                foreach (var i in enemys)
                {
                    if (i != null)
                    {
                        flag1 = false;
                    }
                }
                Debug.Log(flag1);
                if (flag1)
                {
                    enemys.Clear();
                    
                    flag = false;
                }
                
            }

        }*/

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player") && !_spawned)
            {
                _spawned = true;
                foreach (Transform spawner in enemySpawns)
                {
                    int rand = Random.Range(0, 11);
                    if (rand < 9)
                    {
                        GameObject enemyType = enemyTypes[Random.Range(0, enemyTypes.Length)];
                        GameObject enemy = Instantiate(enemyType,spawner.position,Quaternion.identity);
                        enemys.Add(enemy);
                    }
                    else if (rand == 9)
                    {
                        Instantiate(healthPottion, spawner.position, Quaternion.identity);
                    }
                }
                StartCoroutine(CheakEnemys());
            }
        }
        private IEnumerator CheakEnemys()
        {
            yield return new WaitForSeconds(1f);
            yield return new WaitUntil(() =>
            {
                var flag = true;
                foreach (var item in enemys)
                {
                    if (item != null) flag = false;
                }
                return flag;
            });
            DestroyWalls();
        }

        private void DestroyWalls()
        {
            foreach (GameObject wall in walls)
            {
                if (wall != null && wall.transform.childCount != 0)
                {
                    Destroy(wall);
                }
            }
            _wallDestoyed = true;
        }
        private void OnTriggerStay2D(Collider2D other)
        {
            if (_wallDestoyed && other.CompareTag("Wall"))
            {
                Destroy(other.gameObject);
            }
        }
    }
}
