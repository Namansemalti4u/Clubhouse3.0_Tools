using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Clubhouse.Tools.VisualEffects
{
    [Serializable]
    public enum VfxPackType
    {
        None,
        EpicToonFX,
        CartoonFX,
        HyperCasualFX,
        TextFX,
    }

    [CreateAssetMenu(fileName = "VfxMap", menuName = "VisualFX/VFX Map")]
    public class VfxMap : ScriptableObject
    {

        [Serializable]
        public struct VfxAddress
        {
            public string key;
            public VfxPackType packType;
            public string vfxName;
        }

        [SerializeField] private VfxAddress[] vfxAddresses;
        [SerializeField] private VfxCollection[] vfxCollections;
        private Dictionary<string, VfxAddress> vfxDataDict;
        private Dictionary<VfxPackType, VfxCollection> vfxCollectionDict;

        public void Initialize()
        {
            foreach (var collection in vfxCollections) collection.Initialize();

            vfxDataDict = vfxAddresses.ToDictionary(data => data.key, data => data);
            vfxCollectionDict = vfxCollections.ToDictionary(collection => collection.collectionName, collection => collection);
        }

        public VisualEffectRef GetVfx(string key)
        {
            VfxAddress vfxData = GetVfxData(key);
            VfxCollection vfxCollection = GetVfxCollection(vfxData.packType);
            if (vfxCollection == null) return default;

            return vfxCollection.GetVfx(vfxData.vfxName);
        }

        private VfxAddress GetVfxData(string key)
        {
            if (vfxDataDict == null)
            {
                Initialize();
            }

            if (!vfxDataDict.ContainsKey(key))
            {
                Debug.LogError($"Vfx with key {key} not found");
                return default;
            }

            return vfxDataDict[key];
        }

        public VfxCollection GetVfxCollection(VfxPackType packType)
        {
            if (vfxCollectionDict == null)
            {
                Initialize();
            }

            if (!vfxCollectionDict.ContainsKey(packType))
            {
                Debug.LogError($"Vfx collection with pack type {packType} not found");
                return null;
            }

            return vfxCollectionDict[packType];
        }
    }
}
