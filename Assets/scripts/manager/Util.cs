using System;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
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
        
        public static Vector3 randomScale(float min = 0f, float max = 1f)
        {
            return new Vector3(Random.Range(min, max), Random.Range(min, max), Random.Range(min, max));
        }


        public static ItemType getRandomItemType(bool canBeNone = false)
        {
            Array resources = Enum.GetValues(typeof(ItemType));
            int randomIndex = Random.Range(canBeNone ? 0 : 1, resources.Length);
            Debug.Log("Random index: " + randomIndex);
            return (ItemType) resources.GetValue(randomIndex);
        }
        
    }
}