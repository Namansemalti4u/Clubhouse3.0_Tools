using System;
using Clubhouse.Helper;
using UnityEngine;
using MoreMountains.Feedbacks;

namespace Clubhouse.Tools.VisualEffects
{
    public class VfxManager : Singleton<VfxManager>
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
        
        [Header("TextFX")]
        [SerializeField] private Transform textFXPoolParent;

        private TextFXManager textFXManager;
        private ScoreFXManager scoreFXManager;

        protected override void Awake()
        {
            base.Awake();
            vfxMap.Initialize();
            textFXManager = new TextFXManager(textFXPoolParent, vfxMap, this);
            scoreFXManager = new ScoreFXManager();
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

        public void ShowScoreEffect(float a_score, Transform a_target, Color a_color = default, float a_Delay = 0f, float a_duration = 1f, float a_distance = 1f)
        {
            scoreFXManager.ShowScoreEffect(a_score, a_target, a_color, a_Delay, a_duration, a_distance);
        }
        
        public void ShowScoreEffectOnRect(float a_score, RectTransform a_target, Color a_color = default, float a_Delay = 0f, float a_duration = 1f, float a_distance = 1f)
        {
            scoreFXManager.ShowScoreEffectOnRect(a_score, a_target, a_color, a_Delay, a_duration, a_distance);
        }

        public void ShowTextEffect(TextEffectType a_type, VfxPlayParams a_params = null)
        {
            (VisualFX vfx, ObjectPoolManager<VisualFX> pool, VfxPlayParams vfxparams) = textFXManager.ShowTextEffect(a_type, a_params);
            PlayVfx(vfx, pool, vfxparams);
        }

        public void ShowTextEffect(TextEffectType a_type, int a_count, VfxPlayParams a_params = null)
        {
            (VisualFX vfx, ObjectPoolManager<VisualFX> pool, VfxPlayParams vfxparams) = textFXManager.ShowTextEffect(a_type, a_count, a_params);
            PlayVfx(vfx, pool, vfxparams);
        }

        public void ShowTextEffect(TextEffectType a_type, string a_text, int a_count = 0, VfxPlayParams a_params = null)
        {
            (VisualFX vfx, ObjectPoolManager<VisualFX> pool, VfxPlayParams vfxparams) = textFXManager.ShowTextEffect(a_type, a_text, a_count, a_params);
            PlayVfx(vfx, pool, vfxparams);
        }
    }
}
