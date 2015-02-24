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

        private Dictionary<string, List<GameEventListener>> listeners = new Dictionary<string, List<GameEventListener>>();

        #region PushEvent

        /// <summary>
        /// Pushes an event to all objects listening for this specific event.
        /// </summary>
        /// <param name="e">The event object</param>
        /// <param name="type">The type of event it is</param>
        /// <param name="sender">The object sending the event</param>
        public void PushEvent(object sender, string type, GameEvent e)
        {
            if (listeners.ContainsKey(type))
            {
                foreach (GameEventListener listener in listeners[type])
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
        public void AddListener(string type, GameEventListener l)
        {
            if (!listeners.ContainsKey(type))
                listeners[type] = new List<GameEventListener>();

            if (!listeners[type].Contains(l))
                listeners[type].Add(l);
        }

        /// <summary>
        /// Remove a listener from the game event manager.
        /// </summary>
        /// <param name="type">Type of event we are removing.</param>
        /// <param name="l">The listener that will not receive these events.</param>
        public void RemoveListener(string type, GameEventListener l)
        {
            if (listeners.ContainsKey(type))
            {
                if (listeners[type].Contains(l))
                    listeners[type].Remove(l);
            }
        }

        
    }

    #region Sub Classes

    public class GameEvent
    {
        private Vector3 location;

        public GameEvent() : this(Vector3.zero) { }

        public GameEvent(Vector3 location)
        {
            this.location = location;
        }

        public Vector3 Location
        {
            get { return location; }
        }
    }

    public interface GameEventListener
    {
        void PushEvent(object sender, string type, GameEvent e);
    }
    #endregion
}