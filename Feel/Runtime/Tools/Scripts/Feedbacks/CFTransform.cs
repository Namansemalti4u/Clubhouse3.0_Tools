using UnityEngine;
using MoreMountains.Feedbacks;

namespace Clubhouse.Tools
{
    public static partial class Feel
    {
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

        #region RotationFeedback
        public static MMF_Rotation Rotation(MMF_Player a_feedbackPlayer, float a_delay = 0f) => Play<MMF_Rotation>(a_feedbackPlayer, a_delay: a_delay);
        public static MMF_Rotation Rotation(MMF_Player a_feedbackPlayer, float a_duration, float a_delay = 0f)
        => Play<MMF_Rotation>(a_feedbackPlayer, (feedback) =>
        {
            Rotation(feedback, a_duration);
        }, a_delay: a_delay);
        public static MMF_Rotation Rotation(MMF_Player a_feedbackPlayer, float a_initialAngle, float a_targetAngle, float a_duration = -1f, float a_delay = 0f)
        => Play<MMF_Rotation>(a_feedbackPlayer, (feedback) =>
        {
            Rotation(feedback, a_initialAngle, a_targetAngle, a_duration);
        }, a_delay: a_delay);

        public static void Rotation(MMF_Rotation a_feedback, float a_duration) => Play(a_feedback, () =>
        {
            a_feedback.AnimateRotationDuration = a_duration;
        });
        public static void Rotation(MMF_Rotation a_feedback, float a_initialAngle, float a_targetAngle, float a_duration = -1f)
        => Play(a_feedback, () =>
        {
            a_feedback.RemapCurveZero = a_initialAngle;
            a_feedback.RemapCurveOne = a_targetAngle;
            if (a_duration >= 0f)
                a_feedback.AnimateRotationDuration = a_duration;
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
        public static void Scale(MMF_Scale a_feedback, float a_initialScale, float a_targetScale, float a_duration = -1f)
        => Play(a_feedback, () =>
        {
            a_feedback.RemapCurveZero = a_initialScale;
            a_feedback.RemapCurveOne = a_targetScale;
            if (a_duration >= 0f)
                a_feedback.AnimateScaleDuration = a_duration;
        });
        #endregion

        #region RotationShake
        public static MMF_RotationShake RotationShake(MMF_Player a_feedbackPlayer, float a_delay = 0f)
        => Play<MMF_RotationShake>(a_feedbackPlayer, a_delay: a_delay);
        #endregion
    }
}

