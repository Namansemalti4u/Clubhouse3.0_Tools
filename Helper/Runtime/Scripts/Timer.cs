#if CLUBHOUSE_MAIN
using CodeStage.AntiCheat.ObscuredTypes;
#endif

namespace Clubhouse.Games.Common
{
    /// <summary>
    /// Manages a countdown timer with text display functionality.
    /// </summary>
    public class Timer
    {
        #region Fields
        /// <summary>
        /// The total duration of the timer in seconds.
        /// </summary>
        #if CLUBHOUSE_MAIN
        private ObscuredFloat timerDuration;
        #else
        private float timerDuration;
        #endif

        /// <summary>
        /// The current time remaining on the timer.
        /// </summary>
        #if CLUBHOUSE_MAIN
        private ObscuredFloat currentTime;
        #else
        private float currentTime;
        #endif

        /// <summary>
        /// Indicates whether the timer is currently running.
        /// </summary>
        private bool isEnabled = false;
        #endregion

        #region Properties
        /// <summary>
        /// Gets whether the timer has reached zero.
        /// </summary>
        public bool IsFinished => currentTime <= 0 ;

        /// <summary>
        /// Gets whether the timer is running.
        /// </summary>
        public bool IsRunning => isEnabled ;

        /// <summary>
        /// Gets the current time remaining on the timer.
        /// </summary>
        public float RemainingTime => currentTime;

        /// <summary>
        /// Gets the total duration of the timer.
        /// </summary>
        public float Duration => timerDuration;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the Timer class.
        /// </summary>
        /// <param name="a_time">The duration of the timer in seconds.</param>
        public Timer(float a_time)
        {
            timerDuration = a_time;
            currentTime = timerDuration;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Enables the timer.
        /// </summary>
        public void Enable()
        {
            isEnabled = true;
        }

        /// <summary>
        /// Disables the timer.
        /// </summary>
        public void Disable()
        {
            isEnabled = false;
        }

        /// <summary>
        /// Updates the timer if it is enabled and not finished.
        /// </summary>
        public void Update(float a_deltaTime)
        {
            if (!isEnabled) return;
            if (IsFinished) return;
            currentTime -= a_deltaTime;
        }

        /// <summary>
        /// Resets the timer to its initial duration.
        /// </summary>
        public void ResetTimer()
        {
            currentTime = timerDuration;
        }

        /// <summary>
        /// Adds time to the current timer value.
        /// </summary>
        /// <param name="a_time">The amount of time to add in seconds.</param>
        public void IncrementTimer(float a_time)
        {
            currentTime += a_time;
        }

        /// <summary>
        /// Sets a new duration for the timer and resets it.
        /// </summary>
        /// <param name="a_time">The new duration in seconds.</param>
        public void SetTimerDuration(float a_time)
        {
            timerDuration = a_time;
            ResetTimer();
        }
        #endregion
    }
}