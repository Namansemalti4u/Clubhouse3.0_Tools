using UnityEngine;
using MoreMountains.Feedbacks;
using System;
using Clubhouse.Helper;

namespace Clubhouse.Tools
{
    public static partial class Feel
    {
        #region Core
        public static bool IsNull(MMF_Player a_feedbackPlayer)
        {
            bool isNull = a_feedbackPlayer == null;
            if (isNull) Debug.LogError("Feedback Player is null");
            return isNull;
        }

        public static bool IsNull(MMF_Feedback a_feedback)
        {
            bool isNull = a_feedback == null;
            if (isNull) Debug.LogError("Feedback is null");
            return isNull;
        }

        public static T GetFeedback<T>(MMF_Player a_feedbackPlayer) where T : MMF_Feedback
        {
            if (IsNull(a_feedbackPlayer)) return null;
            return a_feedbackPlayer.GetFeedbackOfType<T>(MMF_Player.AccessMethods.First, 0);
        }

        public static T GetFeedback<T>(MMF_Player a_feedbackPlayer, int a_index) where T : MMF_Feedback
        {
            if (IsNull(a_feedbackPlayer)) return null;
            if (a_feedbackPlayer.FeedbacksList[a_index] is not T)
            {
                Debug.LogError($"Feedback is not of type {typeof(T).Name}");
                return null;
            }
            return (T)a_feedbackPlayer.FeedbacksList[a_index];
        }

        public static void PlayAllFeedbacks(MMF_Player a_feedbackPlayer)
        {
            if (IsNull(a_feedbackPlayer)) return;
            a_feedbackPlayer.PlayFeedbacks();
        }

        public static void Play(MMF_Feedback a_feedback, Vector3 a_position = default, float a_feedbacksIntensity = 1f)
        {
            if (IsNull(a_feedback)) return;
            a_feedback.Play(a_position, a_feedbacksIntensity);
        }

        public static void Play(MMF_Feedback a_feedback, Action callback, Vector3 a_position = default, float a_feedbacksIntensity = 1f)
        {
            if (IsNull(a_feedback)) return;
            callback?.Invoke();
            a_feedback.Play(a_position, a_feedbacksIntensity);
        }

        public static T Play<T>(MMF_Player a_feedbackPlayer, Vector3 a_position = default, float a_feedbacksIntensity = 1f, float a_delay = 0f) where T : MMF_Feedback
        {
            if (IsNull(a_feedbackPlayer)) return null;
            T feedback = GetFeedback<T>(a_feedbackPlayer);
            feedback.SetInitialDelay(a_delay);
            Play(feedback, a_position, a_feedbacksIntensity);
            return feedback;
        }

        public static T Play<T>(MMF_Player a_feedbackPlayer, Action<T> callback, float a_delay = 0f) where T : MMF_Feedback
        {
            if (IsNull(a_feedbackPlayer)) return null;
            T feedback = GetFeedback<T>(a_feedbackPlayer);
            feedback.SetInitialDelay(a_delay);
            callback?.Invoke(feedback);
            return feedback;
        }
        #endregion
    }
}
