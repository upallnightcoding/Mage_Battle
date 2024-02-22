using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
            Vector3 point = UnityEngine.Random.insideUnitCircle * distance;

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

    public static class EventManager<TEventArgs>
    {
        private static Dictionary<EventKey, Action<TEventArgs>> eventDictionary = new Dictionary<EventKey, Action<TEventArgs>>();

        public static void RegisterEvent(EventKey eventType, Action<TEventArgs> eventHandler)
        {
            if (!eventDictionary.ContainsKey(eventType))
            {
                eventDictionary[eventType] = eventHandler;
            }
            else
            {
                eventDictionary[eventType] += eventHandler;
            }
        }

        public static void UnregisterEvent(EventKey eventType, Action<TEventArgs> eventHandler)
        {
            if (eventDictionary.ContainsKey(eventType))
            {
                eventDictionary[eventType] -= eventHandler;
            }
        }

        public static void TriggerEvent(EventKey eventType, TEventArgs eventArgs)
        {
            if (eventDictionary.ContainsKey(eventType))
            {
                eventDictionary[eventType]?.Invoke(eventArgs);
            }
        }
    }

    public enum EventKey
    {
        EVENT_UI_COOL_DOWN
    }

    public class EventUIData
    {
        public int Slot { get; set; } = -1;
        public float Percentage { get; set; } = 0.0f;

        public EventUIData(int slot, float percentage)
        {
            Slot = slot;
            Percentage = percentage;
        }
    }
}


