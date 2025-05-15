using UnityEngine;


namespace Clubhouse.Tools.VisualEffects
{
    public enum TextEffectType
    {
        None,
        Perfect,
        Nice,
        Miss,
    }
    
    public class TextEffect : MonoBehaviour
    {
        private ObjectPoolManager<TextEffect> pool;
        public void Initialize(ObjectPoolManager<TextEffect> a_pool)
        {
            pool = a_pool;
        }

        public void Despawn()
        {
            pool.Return(this);
        }
    }
}