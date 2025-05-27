using MoreMountains.Feedbacks;
using UnityEngine;

namespace Clubhouse.Tools.VisualEffects
{
    public class ScoreFXManager
    {
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
    }
}
