using System;
using System.Collections.Generic;
using System.Linq;
using Clubhouse.Helper;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Clubhouse.Tools.VisualEffects
{
    public partial class VfxManager : Singleton<VfxManager>
    {
        [Serializable]
        public enum VfxPackage
        {
            None,
            EpicToonFX,
            CartoonFX,
            HyperCasualFX,
        }
        [Serializable]
        public struct VfxData
        {
            public string key;
            public VfxPackage package;
            public string vfxName;
        }

        [SerializeField] private VfxData[] vfxDatas;
        [SerializeField] private VfxCollection[] vfxCollections;
        // private ObjectPoolManager<>[] vfxPoolManager;
        private Dictionary<string, VfxData> vfxDataDict;
        private Dictionary<VfxPackage, VfxCollection> vfxCollectionDict;

        public void Initialize()
        {
            vfxDataDict = vfxDatas.ToDictionary(data => data.key, data => data);
            vfxCollectionDict = vfxCollections.ToDictionary(collection => collection.collectionName, collection => collection);
        }

        public VisualEffect GetVfx(string key)
        {
            VfxData vfxData = GetVfxData(key);
            VfxCollection vfxCollection = GetVfxCollection(vfxData.package);
            if (vfxCollection == null) return default;

            return vfxCollection.GetVfx(vfxData.vfxName);
        }

        private VfxData GetVfxData(string key)
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

        private VfxCollection GetVfxCollection(VfxPackage package)
        {
            if (vfxCollectionDict == null)
            {
                Initialize();
            }

            if (!vfxCollectionDict.ContainsKey(package))
            {
                Debug.LogError($"Vfx collection with package name {package} not found");
                return null;
            }

            return vfxCollectionDict[package];
        }
    }
}
