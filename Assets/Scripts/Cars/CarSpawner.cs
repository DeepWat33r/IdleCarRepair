using System;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Cars
{
    public class CarSpawner : MonoBehaviour
    {
        public Transform[] spawnPoints;
        public float spawnInterval;
        
        [SerializeField] private string carTag;
        private float _timer;

        public void Update()
        {
            _timer += Time.deltaTime;
            if (_timer >= spawnInterval)
            {
                SpawnCar();
                _timer = 0f;
            }
        }

        private void SpawnCar()
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform carSpawnPoint = spawnPoints[randomIndex];
            ObjectPool.Instance.SpawnFromPool(carTag, carSpawnPoint.position, carSpawnPoint.rotation);
        }
    }
}
