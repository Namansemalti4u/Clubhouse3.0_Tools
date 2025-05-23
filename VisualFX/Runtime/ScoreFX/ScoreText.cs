using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private RectTransform rectTransform;
    private float elapsedTime = 0f;
    private float duration = 1f;
    private Vector2 startPosition;
    private Vector2 endPosition;
    private Vector2 centerPosition;

    public void Initialize(string a_text, Transform a_target, bool a_isUp = true, Color a_color = default, float a_delay = 0f, float a_duration = 1f, float a_distance = 1f)
    {
        elapsedTime = 0f;

        // transform.position = Camera.main.WorldToScreenPoint(a_target.position);
        startPosition = ScoreTextPooler.Instance.GetAnchoredPosition(a_target);
        Debug.Log(startPosition);
        endPosition = startPosition + new Vector2(0, a_distance * 200 * (a_isUp ? 1 : -1));
        rectTransform.anchoredPosition = startPosition;
        textMeshProUGUI.text = a_text;
        if (a_color != default)
        {
            textMeshProUGUI.color = a_color;
        }
        duration = a_duration;
    }

    public void InitializeRect(string a_text, RectTransform a_target, bool a_isUp = true, Color a_color = default, float a_delay = 0f, float a_duration = 1f, float a_distance = 1f)
    {
        elapsedTime = 0f;

        // transform.position = Camera.main.WorldToScreenPoint(a_target.position);
        startPosition = a_target.anchoredPosition;
        Debug.Log(startPosition);
        endPosition = startPosition + new Vector2(0, a_distance * 200 * (a_isUp ? 1 : -1));
        rectTransform.anchoredPosition = startPosition;
        textMeshProUGUI.text = a_text;
        if (a_color != default)
        {
            textMeshProUGUI.color = a_color;
        }
        duration = a_duration;
    }



    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= duration)
        {
            ScoreTextPooler.Instance.ReturnScoreText(this);
        }
        else
        {
            float t = elapsedTime / duration;
            float alpha = 1 - t;
            textMeshProUGUI.color = new Color(textMeshProUGUI.color.r, textMeshProUGUI.color.g, textMeshProUGUI.color.b, alpha);
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, t);
        }
    }
}