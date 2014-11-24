// Copyright 2014 Nicholas Costello <NicholasJCostello@gmail.com>

using Assets.Scripts.Classes;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class Spawner : Singleton<Spawner>
    {
        public float EdgeBufferOffest = 0.5f;
        public int SpawnAmount = 1;
        public GameObject SpawnPrefab;

        // Use this for initialization
        protected void Start()
        {
            for (var i = 0; i < SpawnAmount; ++i)
                SpawnMouse();
        }

        protected void OnEnable()
        {
            Messenger.AddListener(SnakeEvents.ResetGame, OnResetGame);
        }

        protected void OnDisable()
        {
            Messenger.RemoveListener(SnakeEvents.ResetGame, OnResetGame);
        }

        public void OnResetGame()
        {
            var oldSpawns = GameObject.FindGameObjectsWithTag("Mouse");
            foreach (GameObject oldSpawn in oldSpawns)
            {
                Destroy(oldSpawn);
            }
            Start();
        }

        /// <summary>
        /// Spawn Mouse
        ///     Spawn a mouse within the camera's view
        /// </summary>
        public void SpawnMouse()
        {
            var x = Random.Range(
                -(ResolutionManager.HalfWidth - EdgeBufferOffest),
                (ResolutionManager.HalfWidth - EdgeBufferOffest)
                );
            var y = Random.Range(
                -(ResolutionManager.HalfHeight - EdgeBufferOffest),
                ResolutionManager.HalfHeight - EdgeBufferOffest
                );
            x = Mathf.Round(x*2)/2;
            y = Mathf.Round(y*2)/2;
            var spawnLocation = new Vector3(x, y, 0f);
            Instantiate(SpawnPrefab, spawnLocation, Quaternion.identity);
        }
    }
}