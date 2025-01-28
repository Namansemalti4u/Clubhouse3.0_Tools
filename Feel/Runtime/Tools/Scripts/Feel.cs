using UnityEngine;
using MoreMountains.Feedbacks;
using System;

namespace Clubhouse.Tools
{
    public static class Feel
    {
        #region Initialization
        [RuntimeInitializeOnLoadMethod]
        private static void OnRuntimeMethodLoad()
        {
            GameObject defaultTextSpawner = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("MMTextSpawner"));
        }
        #endregion

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

        #region PositionFeedback
        public static MMF_Position Move(MMF_Player a_feedbackPlayer, float a_delay = 0f) => Play<MMF_Position>(a_feedbackPlayer, a_delay: a_delay);
        public static MMF_Position Move(MMF_Player a_feedbackPlayer, Vector3 a_initialPosition, Vector3 a_targetPosition, float a_duration = -1f, float a_delay = 0f)
        => Play<MMF_Position>(a_feedbackPlayer, (feedback) =>
        {
            Move(feedback, a_initialPosition, a_targetPosition, a_duration);
        }, a_delay: a_delay);

        public static void Move(MMF_Position a_feedback, Vector3 a_initialPosition, Vector3 a_targetPosition, float a_duration = -1f)
        => Play(a_feedback, () =>
        {
            a_feedback.InitialPosition = a_initialPosition;
            a_feedback.DestinationPosition = a_targetPosition;
            if (a_duration >= 0f)
                a_feedback.AnimatePositionDuration = a_duration;
        });
        #endregion

        #region ScaleFeedback
        public static MMF_Scale Scale(MMF_Player a_feedbackPlayer, float a_delay = 0f) => Play<MMF_Scale>(a_feedbackPlayer, a_delay: a_delay);
        public static MMF_Scale Scale(MMF_Player a_feedbackPlayer, float a_duration, float a_delay = 0f)
        => Play<MMF_Scale>(a_feedbackPlayer, (feedback) =>
        {
            Scale(feedback, a_duration);
        }, a_delay: a_delay);
        public static MMF_Scale Scale(MMF_Player a_feedbackPlayer, float a_initialScale, float a_targetScale, float a_duration = -1f, float a_delay = 0f)
        => Play<MMF_Scale>(a_feedbackPlayer, (feedback) =>
        {
            Scale(feedback, a_initialScale, a_targetScale, a_duration);
        }, a_delay: a_delay);

        public static void Scale(MMF_Scale a_feedback, float a_duration) => Play(a_feedback, () =>
        {
            a_feedback.AnimateScaleDuration = a_duration;
        });
        public static void Scale(MMF_Scale a_feedback, float a_initiaScale, float a_targetScale, float a_duration = -1f)
        => Play(a_feedback, () =>
        {
            a_feedback.RemapCurveZero = a_initiaScale;
            a_feedback.RemapCurveOne = a_targetScale;
            if (a_duration >= 0f)
                a_feedback.AnimateScaleDuration = a_duration;
        });
        #endregion

        #region RotationShake
        public static MMF_RotationShake RotationShake(MMF_Player a_feedbackPlayer, float a_delay = 0f)
        => Play<MMF_RotationShake>(a_feedbackPlayer, a_delay: a_delay);
        #endregion

        #region FloatingText
        public static void ShowFloatingText(MMF_Player a_feedbackPlayer) => Play<MMF_FloatingText>(a_feedbackPlayer);

        public static void ShowFloatingText(MMF_Player a_feedbackPlayer, string a_text = default, Color a_color = default, float a_Delay = 0f, Action<MMF_FloatingText> action = null)
        => Play<MMF_FloatingText>(a_feedbackPlayer, (feedback) =>
        {
            if (a_color != default)
            {
                feedback.ForceColor = true;
                SetColorInGradients(feedback.AnimateColorGradient, a_color);
            }
            if (a_Delay != 0f) feedback.SetInitialDelay(a_Delay);
            if(!string.IsNullOrEmpty(a_text)) feedback.Value = a_text; 
            action?.Invoke(feedback);
            Play(feedback);
        });

        public static void ShowScoreEffect(MMF_Player a_feedbackPlayer, int a_score, Color a_color = default, float a_Delay = 0f)
        => ShowFloatingText(a_feedbackPlayer, a_color: a_color, a_Delay: a_Delay, action: (feedback) =>
        {
            if (a_score >= 0)
            {
                feedback.Value = "+" + a_score;
            }
            else
            {
                feedback.Value = a_score.ToString();
                feedback.Direction = new Vector3(0, -2, 0);
            }
        });
        #endregion

        #region Helper
        private static void SetColorInGradients(Gradient a_gradient, Color a_color)
        {
            a_gradient.colorKeys = new GradientColorKey[]
            {
                new GradientColorKey(a_color, 0f)
            };
        }
        #endregion
    }
}
