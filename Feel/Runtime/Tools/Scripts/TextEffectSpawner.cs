using UnityEngine;

namespace Clubhouse.Tools
{
    using Games.Common;
    public class TextEffectSpawner : SingletonPersistent<TextEffectSpawner>
    {
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
    }
}
