using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XLib
{
    /**
     * System - 
     * 
     * RandomPoint() - Return random point within a circle
     */
    public class System 
    {
        /**
         * RandomPoint() -
         */
        public static Vector3 RandomPoint(Vector3 center, float distance)
        {
            Vector3 point = Random.insideUnitCircle * distance;

            return (new Vector3(point.x, 0.0f, point.y));
        }
    }
}


