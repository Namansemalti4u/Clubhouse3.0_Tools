using UnityEngine;
using MoreMountains.Feedbacks;
using System;
using Clubhouse.Helper;


namespace Clubhouse.Tools
{
    public static partial class Feel
    {
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

        #region TextEffect
        public static TextEffect ShowText(Vector3 a_position, string a_text, bool a_isCountable, bool a_isTimerVisible = false, 
            int a_count = 1, float a_timer = 1f, float a_scale = 1f, bool a_isTimeDependent = true)
        {
            TextEffect effect = TextEffectSpawner.Instance.Spawn();
            effect.Init(a_position, a_scale, a_text, a_isCountable, a_isTimerVisible, a_isTimeDependent,  a_count, a_timer);
            return effect;
        }
        
        public static TextEffect ShowText(Vector2 a_rectPosition, string a_text, bool a_isCountable, bool a_isTimerVisible = false, int a_count = 1,
         float a_timer = 1f, float a_scale = 1f, bool a_isTimeDependent = true, AnchorType a_anchorType = AnchorType.Center)
        {
            TextEffect effect = TextEffectSpawner.Instance.Spawn();
            effect.Init(a_rectPosition, a_scale, a_text, a_isCountable, a_isTimerVisible, a_isTimeDependent,  a_count, a_timer, a_anchorType);
            return effect;
        }
        #endregion
    }
}
