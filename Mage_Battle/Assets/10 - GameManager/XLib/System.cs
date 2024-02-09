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
         * RandomPoint() - Returns a random point that has been determined 
         * within a 2D unit circle.  The 2D point is returned as a 3D point 
         * with y-axis set to zero.
         */
        public static Vector3 RandomPoint(float distance)
        {
            Vector3 point = Random.insideUnitCircle * distance;

            return (new Vector3(point.x, 0.0f, point.y));
        }

        /**
         * TurnToTarget() - 
         */
        public static Quaternion TurnToTarget(Vector3 target, Transform origin, float turningSpeed, float dt)
        {
            Vector3 lookDirection = target - origin.position;
            lookDirection.y = 0.0f;
            Quaternion rotation = Quaternion.LookRotation(lookDirection);
            return(Quaternion.Lerp(origin.rotation, rotation, turningSpeed * dt));
        }
    }
}


