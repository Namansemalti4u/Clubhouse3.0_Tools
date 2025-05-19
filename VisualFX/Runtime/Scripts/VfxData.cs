using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Clubhouse.Tools.VisualEffects
{
    [CreateAssetMenu(fileName = "VfxData", menuName = "VisualFX/VFX Data")]
    public class VfxData : ScriptableObject
    {
        public string[] vfxNames;
    }
}
