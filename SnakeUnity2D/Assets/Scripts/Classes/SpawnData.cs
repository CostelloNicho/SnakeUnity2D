// Copyright 2014 Nicholas Costello <NicholasJCostello@gmail.com>

using System;
using UnityEngine;

namespace Assets.Scripts.Classes
{
    [Serializable]
    public class SpawnData
    {
        public float EdgeBufferOffest = 0.5f;
        public int SpawnAmount = 1;
        public GameObject SpawnPrefab;
    }
}