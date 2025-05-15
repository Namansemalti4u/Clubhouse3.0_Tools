using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Clubhouse.Tools.VisualEffects
{
    public class VfxCollection : ScriptableObject
    {
        public VfxManager.VfxPackage collectionName;
        public VisualEffect[] vfxList;
        private Dictionary<string, VisualEffect> vfxDictionary;

        public void Initialize()
        {
            vfxDictionary = vfxList.ToDictionary(vfx => vfx.name, vfx => vfx);
        }

        public VisualEffect GetVfx(string name)
        {
            if (vfxDictionary == null)
            {
                Initialize();
            }

            if (!vfxDictionary.ContainsKey(name))
            {
                Debug.LogError($"Vfx with name {name} not found in collection {collectionName}");
                return default;
            }

            return vfxDictionary[name];
        }
    }
}
