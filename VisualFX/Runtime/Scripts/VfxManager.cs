using System;
using Clubhouse.Helper;
using UnityEngine;

namespace Clubhouse.Tools.VisualEffects
{
    public partial class VfxManager : Singleton<VfxManager>
    {
        public class VfxPlayParams
        {
            public Vector3 position = Vector3.zero;
            public Vector3 scale = Vector3.one;
            public Transform parent = null;
            public Action<ParticleSystem> beforePlay = null;

            public VfxPlayParams()
            {
                position = Vector3.zero;
                scale = Vector3.one;
                parent = null;
                beforePlay = null;
            }
        }

        [Header("Main")]
        [SerializeField] private VfxMap vfxMap;

        protected override void Awake()
        {
            base.Awake();
            vfxMap.Initialize();
            InitializeTextFX();
        }

        public void ShowVfx(string a_name, VfxPlayParams a_params = null)
        {
            if(VfxPooler.Instance == null)
            {
                Debug.LogError("VfxPooler is not initialized. Put an Instance of VfxPooler in the scene.");
                return;
            }
            if (a_params == null) a_params = new VfxPlayParams();
            (VisualFX vfx, ObjectPoolManager<VisualFX> pool) = VfxPooler.Instance.GetVfxFromPool(a_name, a_params.parent);
            PlayVfx(vfx, pool, a_params);
        }

        public GameObject GetVfx(string a_name)
        {
            return vfxMap.GetVfx(a_name).prefab;
        }

        private void PlayVfx(VisualFX a_vfx, ObjectPoolManager<VisualFX> a_pool, VfxPlayParams a_params)
        {
            if (a_params.parent == null)
            {
                a_vfx.transform.position = a_params.position;
            }
            else
            {
                a_vfx.transform.localPosition = a_params.position;
            }
            a_vfx.transform.localScale = a_params.scale;
            a_vfx.Play(a_pool, a_params.beforePlay);
        }
    }
}
