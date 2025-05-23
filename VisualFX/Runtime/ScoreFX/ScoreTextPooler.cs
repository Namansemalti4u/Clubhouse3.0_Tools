using Clubhouse.Helper;
using UnityEngine;

public class ScoreTextPooler : Singleton<ScoreTextPooler>
{
    [SerializeField] private GameObject scoreTextPrefab;
    [SerializeField] private RectTransform rect;

    private ObjectPoolManager<ScoreText> scoreTextPool;

    protected override void Awake()
    {
        base.Awake();
        scoreTextPool = new ObjectPoolManager<ScoreText>(scoreTextPrefab.GetComponent<ScoreText>(), transform, 10);
    }

    public ScoreText GetScoreText(Transform a_parent = null)
    {
        return scoreTextPool.Get(a_parent ?? transform);
    }

    public void ReturnScoreText(ScoreText a_scoreText)
    {
        scoreTextPool.Return(a_scoreText);
    }

    public Vector2 GetAnchoredPosition(Transform a_target)
    {
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(a_target.position) - new Vector3((float)Screen.width / 2, (float)Screen.height / 2, 0);
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, screenPoint, Camera.main, out localPoint);
        return screenPoint;
    }
}
