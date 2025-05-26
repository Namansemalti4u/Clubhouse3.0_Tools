using System;
using System.Collections.Generic;
using UnityEngine;
using CartoonFX;

namespace Clubhouse.Tools.VisualEffects
{
    public enum TextEffectType
    {
        Perfect,
        Nice,
        Amazing,
        Miss,
        Combo,
        Scorex2,
        Scorex3,
        Scorex5,
        Type1,
        Type2,
        Type3,
        Type4,
        
    }
    public partial class VfxManager
    {
        [Header("TextFX")]
        [SerializeField] private Transform textFXPoolParent;
        private VfxCollection textFXCollection;
        private Dictionary<TextEffectType, ObjectPoolManager<VisualFX>> textFXDict;

        private void InitializeTextFX()
        {
            TextEffectType[] typeList = (TextEffectType[])Enum.GetValues(typeof(TextEffectType));
            textFXDict = new Dictionary<TextEffectType, ObjectPoolManager<VisualFX>>();
            textFXCollection = vfxMap.GetVfxCollection(VfxPackType.TextFX);

            foreach (var type in typeList)
            {
                textFXDict[type] = new ObjectPoolManager<VisualFX>(
                    textFXCollection.GetVfx(type.ToString()).prefab.GetComponent<VisualFX>(),
                    textFXPoolParent,
                    2
                );
            }
        }

        public void ShowTextEffect(TextEffectType a_type, VfxPlayParams a_params = default)
        {
            if (a_params == null) a_params = new VfxPlayParams();
            VisualFX textFX = textFXDict[a_type].Get(a_params.parent == null ? textFXPoolParent : a_params.parent);
            PlayVfx(textFX, textFXDict[a_type], a_params);
        }

        public void ShowTextEffect(TextEffectType a_type, int a_count, VfxPlayParams a_params = default)
        {
            string text = a_type switch
            {
                TextEffectType.Combo => "COMBO",
                TextEffectType.Perfect => "PERFECT",
                _ => a_type.ToString()
            };
            ShowTextEffect(a_type, text, a_count, a_params);
        }

        public void ShowTextEffect(TextEffectType a_type, string a_text, int a_count = 0, VfxPlayParams a_params = default)
        {
            if (a_params == null) a_params = new VfxPlayParams();
            VisualFX textFX = textFXDict[a_type].Get(a_params.parent == null ? textFXPoolParent : a_params.parent);
            CFXR_ParticleText particleText = textFX.gameObject.GetComponent<CFXR_ParticleText>();
            if (particleText != null && particleText.isDynamic)
            {
                if (a_count > 0)
                    particleText.UpdateText(a_text + " X" + a_count);
                else
                    particleText.UpdateText(a_text);
            }
            PlayVfx(textFX, textFXDict[a_type], a_params);
        }
    }
}
