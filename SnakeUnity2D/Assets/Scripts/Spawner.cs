// Copyright 2014 Nicholas Costello <NicholasJCostello@gmail.com>

using UnityEngine;

namespace Assets.Scripts
{
    public class Spawner : Singleton<Spawner>
    {
        // prefab to spawn
        public GameObject MousePrefab;
        
        // maximum x position an object can spawn
        private float _xMax;
        //maximum y position an object can spawn
        private float _yMax;
        //buffer around the edge of the screen where nothing can spawn
        private const float EdgeBufferZone = 1;
        

        // Use this for initialization
        protected void Start()
        {
            _xMax = Camera.main.aspect * Camera.main.orthographicSize - EdgeBufferZone;
            _yMax = Camera.main.orthographicSize - EdgeBufferZone;

            SpawnMouse();
        }

        // Update is called once per frame
        public void SpawnMouse()
        {
            float x = Random.Range(-_xMax, _xMax);
            float y = Random.Range(-_yMax, _yMax);
            var spawnLocation = new Vector3(Mathf.CeilToInt(x), Mathf.CeilToInt(y), 0f);

            Instantiate(MousePrefab, spawnLocation, Quaternion.identity);
        }
    }
}