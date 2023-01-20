﻿using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class Util
    {

        public static double DistanceSquared(Vector3 loc1, Vector3 loc2)
        {
            return Math.Pow(loc1.x - loc2.x, 2) + 
               Math.Pow(loc1.y - loc2.y, 2) + 
               Math.Pow(loc1.z - loc2.z, 2);
        }

        public static Vector3 randomLocation(int xRange = 100, int zRange = 100)
        {
            return new Vector3(Random.Range(-xRange, xRange), 0, Random.Range(-zRange, zRange));
        }
        
        
    }
}