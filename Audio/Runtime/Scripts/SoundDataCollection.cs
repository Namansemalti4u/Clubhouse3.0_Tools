using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Clubhouse.Tools.Audio
{
    [Serializable]
    public struct SoundDataCategory
    {
        public string name;
        public SoundData sounds;
    }
    
    [CreateAssetMenu(fileName = "CentralizedSoundCollection", menuName = "Audio/Central Sound Collection")]
    public class SoundDataCollection : ScriptableObject
    {
        public SoundDataCategory[] categories;
        private Dictionary<string, SoundData> categoryDictionary;

        public void Initialize()
        {
            categoryDictionary = categories.ToDictionary(r=>r.name, r=>r.sounds);
            foreach (SoundData category in categoryDictionary.Values)
            {
                category?.Initialize();
            }
        }

        public SoundDataCategory[] GetAllCategories() => categories;

        public SoundData GetCollection(string categoryName)
        {
            if(categoryDictionary == null) Initialize();
            if(!categoryDictionary.ContainsKey(categoryName)) return null;
            return categoryDictionary[categoryName];
        }

        public Sound GetSound(string categoryName, string soundName)
        {
            return GetCollection(categoryName).GetSound(soundName);
        }

        public void ResetSoundCollection()
        {
            if(categoryDictionary == null) return;
            foreach (SoundData category in categoryDictionary.Values)
            {
                category?.ResetSoundData();
            }
        }
    }
}