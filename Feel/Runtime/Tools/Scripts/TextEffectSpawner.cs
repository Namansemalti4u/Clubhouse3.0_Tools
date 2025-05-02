using UnityEngine;
using Clubhouse.Helper;
using UnityEngine.UI;

namespace Clubhouse.Tools
{
    public class TextEffectSpawner : Singleton<TextEffectSpawner>
    {
        [SerializeField] private CanvasScaler canvasScaler;
        private ObjectPoolManager<TextEffect> pool;

        protected override void Awake()
        {
            base.Awake();
            GameObject prefab = Resources.Load<GameObject>("TextEffect");
            pool = new ObjectPoolManager<TextEffect>(prefab.GetComponent<TextEffect>(), transform);
        }

        public TextEffect Spawn()
        {
            TextEffect effect = pool.Get(transform);
            return effect;
        }

        public void Despawn(TextEffect a_textEffect)
        {
            pool.Return(a_textEffect);
        }

        public void SetOrientation(bool a_isLandScape)
        {
            canvasScaler.referenceResolution = a_isLandScape ? new Vector2(1920f, 1080f) : new Vector2(1080f, 1920f);
        }
    }
}
