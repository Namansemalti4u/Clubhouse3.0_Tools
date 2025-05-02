using UnityEngine;

namespace Clubhouse.Helper
{
    /// <summary>
    /// Base class for static MonoBehaviour instances that can be accessed globally.
    /// </summary>
    /// <typeparam name="T">The type of MonoBehaviour to be made static.</typeparam>
    public abstract class StaticMonobehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        /// <summary>
        /// Static instance of the MonoBehaviour that can be accessed globally.
        /// </summary>
        public static T Instance { get; private set; }

        /// <summary>
        /// Sets up the static instance on Awake.
        /// </summary>
        protected virtual void Awake()
        {
            Instance = this as T;
        }

        /// <summary>
        /// Cleans up the static instance when the application quits.
        /// </summary>
        protected virtual void OnApplicationQuit()
        {
            Instance = null;
        }
    }

    /// <summary>
    /// Implements the Singleton pattern, ensuring only one instance exists.
    /// Destroys any additional instances that are created.
    /// </summary>
    /// <typeparam name="T">The type of MonoBehaviour to be made into a singleton.</typeparam>
    public abstract class Singleton<T> : StaticMonobehaviour<T> where T : MonoBehaviour
    {
        /// <summary>
        /// Ensures only one instance exists by destroying any duplicate instances.
        /// </summary>
        protected override void Awake()
        {
            if (Instance != null)
                Destroy(gameObject);
            else
                base.Awake();
        }
    }

    /// <summary>
    /// Implements a persistent Singleton pattern that survives scene loads.
    /// The instance will not be destroyed when loading new scenes.
    /// </summary>
    /// <typeparam name="T">The type of MonoBehaviour to be made into a persistent singleton.</typeparam>
    public abstract class SingletonPersistent<T> : Singleton<T> where T : MonoBehaviour
    {
        /// <summary>
        /// Marks the instance to persist between scene loads after ensuring singleton status.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
    }
}