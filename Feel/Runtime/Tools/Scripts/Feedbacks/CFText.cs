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
        #endregion
    }
}
