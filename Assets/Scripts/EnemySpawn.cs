using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameOne {
    public class EnemySpawn : MonoBehaviour
    {
        [SerializeField] private GameObject zoin;
        [SerializeField] private GameObject spitter;
        [SerializeField] private GameObject eye;
        [SerializeField] private Transform[] zoinSpawnPoints;
        [SerializeField] private Transform[] spitterSpawnPoints;
        [SerializeField] private Transform[] eyeSpawnPoints;
        [SerializeField] private bool pause;
        [SerializeField] private int enemySpawnSimultaneosly;

        private bool haveSpitter;
        public bool HaveSpitter {
            get {
                return haveSpitter;
            }

            set {
                haveSpitter = value;
            }
        }

        private float spitterSpawnTimer;

        // Start is called before the first frame update
        void Start()
        {
            InvokeRepeating("SpawnZoin", 1f, 2f);
            InvokeRepeating("SpawnSpitter", 10f, 10f);
        }

        void SpawnZoin()
        {
            if (pause) { return; }


            for (int i = 0; i < Random.Range(1, zoinSpawnPoints.Length); i++)
            {
                int spawnPointIndex = Random.Range(0, zoinSpawnPoints.Length);
                Instantiate(zoin, zoinSpawnPoints[spawnPointIndex].position, zoinSpawnPoints[spawnPointIndex].rotation);
            }
        }

        void SpawnSpitter()
        {
            if (pause) { return; }

            if (!haveSpitter) {
                spitterSpawnTimer += Time.deltaTime;
                if (spitterSpawnTimer >= 10f)
                {
                    int spitterSpawnPointIndex = Random.Range(0, spitterSpawnPoints.Length);
                    Instantiate(spitter, spitterSpawnPoints[spitterSpawnPointIndex].position, Quaternion.identity);
                }
            }
        }
    }
}