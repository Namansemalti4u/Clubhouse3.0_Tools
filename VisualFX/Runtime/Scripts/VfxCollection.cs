using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Clubhouse.Tools.VisualEffects
{
    [CreateAssetMenu(fileName = "VfxCollection", menuName = "VisualFX/VFX Collection")]
    public class VfxCollection : ScriptableObject
    {
        public VfxPackType collectionName;
        public VisualEffectRef[] vfxList;
        private Dictionary<string, VisualEffectRef> vfxDictionary;

        public void Initialize()
        {
            vfxDictionary = vfxList.ToDictionary(vfx => vfx.name, vfx => vfx);
        }

        public VisualEffectRef GetVfx(string name)
        {
            if (vfxDictionary == null)
            {
                Initialize();
            }

            if (vfxDictionary.ContainsKey(name))
            {
                return vfxDictionary[name];
            }

            Debug.LogError($"Vfx with name {name} not found in collection {collectionName}");
            return default;
        }
    }
}
