using System;
using UnityEngine;

namespace Clubhouse.Tools.VisualEffects
{
    [Serializable]
    public struct VisualEffectRef
    {
        public string name;
        public GameObject prefab;
    }
    
    [RequireComponent(typeof(ParticleSystem))]
    public class VisualFX : MonoBehaviour
    {
        private ParticleSystem ps;
        private ObjectPoolManager<VisualFX> pool;

        public void Play(ObjectPoolManager<VisualFX> a_pool, Action<ParticleSystem> a_beforePlay = null)
        {
            pool = a_pool;
            ps = GetComponent<ParticleSystem>();
            a_beforePlay?.Invoke(ps);
            ps.Play();
        }

        public void Stop()
        {
            ps.Stop();
            pool.Return(this);
        }

        void Update()
        {
            if (ps == null) return;
            if(!ps.IsAlive())
            {
                Stop();
            }
        }
    }
}
