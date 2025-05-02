using System;
using System.Collections.Generic;
using UnityEngine;

namespace Clubhouse.Games.Common
{
    /// <summary>
    /// Base class for implementing a Finite State Machine pattern.
    /// </summary>
    public abstract class StateMachine
    {
        #region Fields
        private Dictionary<int, IState> states;
        private int currentState;
        private bool isEnabled;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the current state identifier.
        /// </summary>
        public int State => currentState;
        /// <summary>
        /// Gets whether the state machine is currently enabled.
        /// </summary>
        public bool IsEnabled => isEnabled;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the StateMachine class.
        /// Sets up initial state as 0 (None state).
        /// </summary>
        public StateMachine()
        {
            currentState = 0;
            states = new Dictionary<int, IState>();
            RegisterState(0, null);
            isEnabled = false;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Called when the state machine is enabled.
        /// Override this method to implement initialization logic.
        /// </summary>
        public virtual void Enable()
        {
            isEnabled = true;
        }

        /// <summary>
        /// Updates the current state if not in None state (0).
        /// Called every frame when the state machine is active.
        /// </summary>
        public virtual void Update()
        {
            if (!isEnabled) return;
            if (currentState != 0)
                states[currentState].Update();
        }

        /// <summary>
        /// Called when the state machine is disabled.
        /// Resets the current state to None (0).
        /// </summary>
        public virtual void Disable()
        {
            isEnabled = false;
            currentState = 0;
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Registers a new state with the state machine.
        /// </summary>
        /// <param name="stateId">Unique identifier for the state.</param>
        /// <param name="state">State implementation to register.</param>
        protected void RegisterState(int stateId, IState state)
        {
            states[stateId] = state;
        }

        /// <summary>
        /// Changes the current state to a new state.
        /// </summary>
        /// <param name="a_newState">The ID of the state to change to.</param>
        /// <exception cref="ArgumentException">Thrown when the new state ID is not registered.</exception>
        protected void ChangeState(int a_newState)
        {
            if (!states.TryGetValue(a_newState, out IState newState))
                throw new ArgumentException($"State {a_newState} is not registered.");


            // 0 means None state
            if (currentState != 0)
            {
                // Debug.Log(currentState + " To " + a_newState);
                states[currentState].Exit();
            }

            currentState = a_newState;
            states[currentState].Enter();
        }
        #endregion
    }

    #region Interfaces
    /// <summary>
    /// Interface defining the required methods for implementing a state in the state machine.
    /// </summary>
    public interface IState
    {
        /// <summary>
        /// Called when entering the state.
        /// </summary>
        void Enter();

        /// <summary>
        /// Called every frame while the state is active.
        /// </summary>
        void Update();

        /// <summary>
        /// Called when exiting the state.
        /// </summary>
        void Exit();
    }
    #endregion
}
