using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Clubhouse.Tools.VisualEffects
{
    public class TextEffectData : ScriptableObject
    {
        public struct TextData
        {
            public TextEffectType textEffectType;
            public GameObject effectPrefab;
        }

        public TextData[] textDatas;
        private Dictionary<TextEffectType, ObjectPoolManager<TextEffect>> textEffectDictionary;

        // private void Initialize()
        // {
        //     textEffectDictionary = textDatas.ToDictionary(r => r.textEffectType, r => new ObjectPoolManager<TextEffect>());
        // }

        // public GameObject GetEffect(TextEffectType a_textEffectType)
        // {
        //     if (textEffectDictionary == null) Initialize();
        //     if (textEffectDictionary.ContainsKey(a_textEffectType))
        //     {
        //         return textEffectDictionary[a_textEffectType];
        //     }
        //     return null;
        // }
    }
}
