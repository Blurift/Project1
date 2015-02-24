/* Name: Game Event Manager
 * Author: Keirron Stach
 * Created: 28/08/2014
 * Edited: 28/08/2014 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Blurift
{

    public class EntityEventManager : MonoBehaviour
    {

        private Dictionary<string, List<EntityEventListener>> listeners = new Dictionary<string, List<EntityEventListener>>();

        #region PushEvent

        /// <summary>
        /// Pushes an event to all objects listening for this specific event.
        /// </summary>
        /// <param name="e">The event object</param>
        /// <param name="type">The type of event it is</param>
        /// <param name="sender">The object sending the event</param>
        public void PushEvent(object sender, string type, EntityEvent e)
        {
            if (listeners.ContainsKey(type))
            {
                foreach (EntityEventListener listener in listeners[type])
                {
                    listener.PushEvent(sender, type, e);
                }
            }
        }

        #endregion

        /// <summary>
        /// Adds a listener to a specific event type.
        /// </summary>
        /// <param name="type">The type of event to listen for.</param>
        /// <param name="l">The listener that will be notified of events.</param>
        public void AddListener(string type, EntityEventListener l)
        {
            if (!listeners.ContainsKey(type))
                listeners[type] = new List<EntityEventListener>();

            if (!listeners[type].Contains(l))
                listeners[type].Add(l);
        }

        /// <summary>
        /// Remove a listener from the game event manager.
        /// </summary>
        /// <param name="type">Type of event we are removing.</param>
        /// <param name="l">The listener that will not receive these events.</param>
        public void RemoveListener(string type, EntityEventListener l)
        {
            if (listeners.ContainsKey(type))
            {
                if (listeners[type].Contains(l))
                    listeners[type].Remove(l);
            }
        }

        
    }

    #region Sub Classes

    public class EntityEvent
    {
        private Vector3 location;

        public EntityEvent() : this(Vector3.zero) { }

        public EntityEvent(Vector3 location)
        {
            this.location = location;
        }

        public Vector3 Location
        {
            get { return location; }
        }
    }

    public interface EntityEventListener
    {
        void PushEvent(object sender, string type, EntityEvent e);
    }
    #endregion
}