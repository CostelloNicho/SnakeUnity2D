// Copyright 2014 Nicholas Costello <NicholasJCostello@gmail.com>

using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class Spawner : Singleton<Spawner>
    {
        // prefab to spawn

        //buffer around the edge of the screen where nothing can spawn
        private const float EdgeBufferZone = 1;
        public GameObject MousePrefab;


        // Use this for initialization
        protected void Start()
        {
            SpawnMouse();
        }

        // Update is called once per frame
        public void SpawnMouse()
        {
            float x = Random.Range(
                -(ResolutionManager.HalfWidth - EdgeBufferZone),
                (ResolutionManager.HalfWidth - EdgeBufferZone)
                );
            float y = Random.Range(
                -(ResolutionManager.HalfHeight - EdgeBufferZone),
                ResolutionManager.HalfHeight - EdgeBufferZone
                );
             x = (float)Math.Round(x*2, MidpointRounding.AwayFromZero)/2;
             y = (float)Math.Round(y * 2, MidpointRounding.AwayFromZero) / 2;
            var spawnLocation = new Vector3(Mathf.CeilToInt(x), Mathf.CeilToInt(y), 0f);
            Instantiate(MousePrefab, spawnLocation, Quaternion.identity);
        }
    }
}