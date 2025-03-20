using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Threading.Tasks;
using MoreMountains.Feedbacks;
using Clubhouse.Helper;

namespace Clubhouse.Tools
{
    using Games.Common;

    public class TextEffect : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI typeText, countText;
        [SerializeField] RectTransform rectTransform;
        [SerializeField] Slider timerSlider;
        [SerializeField] MMF_Player[] feedBacks;

        private int count;
        private string text;
        private float timerDuration;
        private Timer timer;
        private bool isActive, isTimeDependent;

        public bool IsActive => isActive;

        private void Update()
        {
            if (!isActive) return;
            UpdateTimer();
        }

        public void Init(Vector3 a_position, float a_scale, string a_text, bool isCountable, bool a_isTimerVisible, bool a_isTimeDependent, int a_count = 1, float a_timer = 1f)
        {
            transform.position = Camera.main.WorldToScreenPoint(a_position);
            transform.localScale = 0.5f * a_scale * Vector3.one;

            var spawnScaleFeedback = Feel.GetFeedback<MMF_Scale>(feedBacks[0]);
            spawnScaleFeedback.RemapCurveZero = a_scale * 0.5f;
            spawnScaleFeedback.RemapCurveOne = a_scale;

            var despawnScaleFeedback = Feel.GetFeedback<MMF_Scale>(feedBacks[2]);
            despawnScaleFeedback.RemapCurveZero = a_scale;
            despawnScaleFeedback.RemapCurveOne = a_scale * 0.5f;

            countText.gameObject.SetActive(isCountable);
            timerSlider.gameObject.SetActive(a_isTimerVisible);


            timerDuration = a_timer;
            timer = new Timer(timerDuration);
            timer.Enable();

            text = a_text;
            count = a_count;
            isTimeDependent = a_isTimeDependent;

            isActive = true;

            UpdateEffect(text, count);
            feedBacks[0].PlayFeedbacks();
        }

        public void Init(Vector2 a_rectPosition, float a_scale, string a_text, bool isCountable, bool a_isTimerVisible, bool a_isTimeDependent, 
                int a_count = 1, float a_timer = 1f, AnchorType a_anchorType = AnchorType.Center)
        {
            RectHelper.SetRectPosition(rectTransform, a_rectPosition, a_anchorType);
            transform.localScale = 0.5f * a_scale * Vector3.one;

            var spawnScaleFeedback = Feel.GetFeedback<MMF_Scale>(feedBacks[0]);
            spawnScaleFeedback.RemapCurveZero = a_scale * 0.5f;
            spawnScaleFeedback.RemapCurveOne = a_scale;

            var despawnScaleFeedback = Feel.GetFeedback<MMF_Scale>(feedBacks[2]);
            despawnScaleFeedback.RemapCurveZero = a_scale;
            despawnScaleFeedback.RemapCurveOne = a_scale * 0.5f;

            countText.gameObject.SetActive(isCountable);
            timerSlider.gameObject.SetActive(a_isTimerVisible);


            timerDuration = a_timer;
            timer = new Timer(timerDuration);
            timer.Enable();

            text = a_text;
            count = a_count;
            isTimeDependent = a_isTimeDependent;

            isActive = true;

            UpdateEffect(text, count);
            feedBacks[0].PlayFeedbacks();
        }

        public async void Hide(bool isWrong = false)
        {
            timer.Disable();
            isActive = false;

            if (isWrong)
            {
                feedBacks[2].PlayFeedbacks();

                while (feedBacks[2].IsPlaying)
                {
                    await Task.Delay(100);
                }
            }

            TextEffectSpawner.Instance.Despawn(this);
        }

        public void IncrementCount(float newTimerDuration = -1)
        {
            feedBacks[1].PlayFeedbacks();
            UpdateEffect(text, ++count);
            if (newTimerDuration > 0)
            {
                timer.SetTimerDuration(newTimerDuration);
            }
        }

        public void DecrementCount(float newTimerDuration = -1)
        {
            feedBacks[3].PlayFeedbacks();
            UpdateEffect(text, --count);
            if (newTimerDuration > 0)
            {
                timer.SetTimerDuration(newTimerDuration);
            }
        }

        public void UpdateSliderValue(float a_value)
        {
            timerSlider.value = a_value;
        }

        private async void UpdateEffect(string a_text, int a_count)
        {
            typeText.text = a_text;
            countText.text = "x" + a_count;
            await Task.Delay(10);
            UpdateSliderPosition();
            timer.ResetTimer();
        }

        private void UpdateTimer()
        {
            if (!timer.IsRunning || !isTimeDependent) return;

            timer.Update(Time.deltaTime);
            if (timer.IsFinished)
            {
                Hide();
            }
            else
            {
                timerSlider.value = timer.RemainingTime / timerDuration;
            }

            // UpdateSliderPosition();
        }

        private void UpdateSliderPosition()
        {
            var offset = GetSliderHorizontalOffset();

            RectTransform sliderRect = timerSlider.gameObject.GetComponent<RectTransform>();
            sliderRect.offsetMin = new Vector2(offset.x, sliderRect.offsetMin.y);
            sliderRect.offsetMax = new Vector2(-offset.y, sliderRect.offsetMax.y);
        }

        private Vector2 GetSliderHorizontalOffset()
        {
            float edge = rectTransform.rect.width / 2;

            float x1 = typeText.textInfo.characterInfo[0].bottomLeft.x + typeText.GetComponent<RectTransform>().anchoredPosition.x;
            float x2 = countText.textInfo.characterInfo[countText.text.Length - 1].bottomRight.x + countText.GetComponent<RectTransform>().anchoredPosition.x;

            return new Vector2(edge + x1, edge - x2);
        }
    }
}
