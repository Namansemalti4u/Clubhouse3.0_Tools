using System.Collections;
using System.Collections.Generic;
using Clubhouse.Helper;
using UnityEngine;

namespace Clubhouse.Tools.VisualEffects
{
    public class VfxPooler : StaticMonobehaviour<VfxPooler>
    {
        [SerializeField] private VfxData vfxData;
        private Dictionary<string, ObjectPoolManager<VisualFX>> vfxDict;

        protected override void Awake()
        {
            base.Awake();
            vfxDict = new Dictionary<string, ObjectPoolManager<VisualFX>>();

            foreach (string vfx in vfxData.vfxNames)
            {
                vfxDict[vfx] = new ObjectPoolManager<VisualFX>(
                    VfxManager.Instance.GetVfx(vfx).GetComponent<VisualFX>(),
                    transform,
                    2
                );
            }
        }

        public (VisualFX, ObjectPoolManager<VisualFX>) GetVfxFromPool(string a_name, Transform a_parent = null)
        {
            return (vfxDict[a_name].Get(a_parent == null ? transform : a_parent), vfxDict[a_name]);
        }
    }
}
