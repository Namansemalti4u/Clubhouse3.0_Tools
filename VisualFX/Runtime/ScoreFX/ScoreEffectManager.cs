using MoreMountains.Feedbacks;
using UnityEngine;

namespace Clubhouse.Tools.VisualEffects
{
    public partial class VfxManager
    {
        [Header("ScoreFX")]
        [SerializeField] private MMF_Player scoreFeedbackPlayer;

        // public void ShowScoreEffect(float a_score, Transform a_target, Color a_color = default, float a_Delay = 0f)
        // => Feel.ShowFloatingText(scoreFeedbackPlayer, a_color: a_color, a_Delay: a_Delay, action: (feedback) =>
        // {
        //     if (a_score >= 0)
        //     {
        //         feedback.Value = "+" + a_score.ToString("F2");
        //         feedback.Direction = Vector3.zero;
        //     }
        //     else
        //     {
        //         feedback.Value = a_score.ToString("F2");
        //         feedback.Direction = new Vector3(0, -2, 0);
        //     }
        //     feedback.TargetTransform = a_target;
        // });

        public void ShowScoreEffect(float a_score, Transform a_target, Color a_color = default, float a_Delay = 0f, float a_duration = 1f, float a_distance = 1f)
        {
            ScoreText scoreText = ScoreTextPooler.Instance.GetScoreText();
            string text = a_score >= 0 ? "+" + a_score.ToString("F2") : a_score.ToString("F2");
            bool isUp = a_score >= 0;
            scoreText.Initialize(text, a_target, isUp, a_color, a_Delay, a_duration, a_distance);
        }

        public void ShowScoreEffectOnRect(float a_score, RectTransform a_target, Color a_color = default, float a_Delay = 0f, float a_duration = 1f, float a_distance = 1f)
        {
            ScoreText scoreText = ScoreTextPooler.Instance.GetScoreText(a_target.parent);
            string text = a_score >= 0 ? "+" + a_score.ToString("F2") : a_score.ToString("F2");
            bool isUp = a_score >= 0;
            scoreText.InitializeRect(text, a_target, isUp, a_color, a_Delay, a_duration, a_distance);
        }

        public void ShowScoreEffect(MMF_Player a_feedbackPlayer, float a_score, Color a_color = default, float a_Delay = 0f)
        => Feel.ShowFloatingText(a_feedbackPlayer, a_color: a_color, a_Delay: a_Delay, action: (feedback) =>
        {
            if (a_score >= 0)
            {
                feedback.Value = "+" + a_score.ToString("F2");
            }
            else
            {
                feedback.Value = a_score.ToString("F2");
                feedback.Direction = new Vector3(0, -2, 0);
            }
        });
    }
}
