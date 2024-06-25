using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class Spawner : MonoBehaviour
    {
        public List<GameObject> enemiesprefabs;
        private GameObject enemy;


        void Start()
        {
            int randomIndex = Random.Range(0, enemiesprefabs.Count);
            enemy = enemiesprefabs[randomIndex];

            Instantiate(enemy, transform.position, Quaternion.identity);
            GameManager.instance.SpawnEnemy();
        }
    }
