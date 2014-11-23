// Copyright 2014 Nicholas Costello <NicholasJCostello@gmail.com>

using Assets.Scripts.Classes;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class Spawner : Singleton<Spawner>
    {
        // prefab to spawn
        //buffer around the edge of the screen where nothing can spawn
        public SpawnData SpawnerData;

        // Use this for initialization
        protected void Start()
        {
            for (var i = 0; i < SpawnerData.SpawnAmount; ++i)
                SpawnMouse();
        }

        /// <summary>
        /// Spawn Mouse
        ///     Spawn a mouse within the camera's view
        /// </summary>
        public void SpawnMouse()
        {
            var x = Random.Range(
                -(ResolutionManager.HalfWidth - SpawnerData.EdgeBufferOffest),
                (ResolutionManager.HalfWidth - SpawnerData.EdgeBufferOffest)
                );
            var y = Random.Range(
                -(ResolutionManager.HalfHeight - SpawnerData.EdgeBufferOffest),
                ResolutionManager.HalfHeight - SpawnerData.EdgeBufferOffest
                );
            x = Mathf.Round(x*2)/2;
            y = Mathf.Round(y*2)/2;
            var spawnLocation = new Vector3(x, y, 0f);
            Instantiate(SpawnerData.SpawnPrefab, spawnLocation, Quaternion.identity);
        }
    }
}