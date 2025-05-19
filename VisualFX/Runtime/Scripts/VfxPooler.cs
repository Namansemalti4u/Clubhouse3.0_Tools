using System.Collections;
using System.Collections.Generic;
using Clubhouse.Helper;
using UnityEngine;

namespace Clubhouse.Tools.VisualEffects
{
    public class VfxPooler : StaticMonobehaviour<VfxPooler>
    {
        [SerializeField] private VfxData vfxData;
        private Dictionary<string, ObjectPoolManager<VisualEffect>> vfxDict;

        protected override void Awake()
        {
            base.Awake();
            vfxDict = new Dictionary<string, ObjectPoolManager<VisualEffect>>();

            foreach (string vfx in vfxData.vfxNames)
            {
                vfxDict[vfx] = new ObjectPoolManager<VisualEffect>(
                    VfxManager.Instance.GetVfx(vfx).GetComponent<VisualEffect>(),
                    transform,
                    2
                );
            }
        }

        public (VisualEffect, ObjectPoolManager<VisualEffect>) GetVfxFromPool(string a_name, Transform a_parent = null)
        {
            return (vfxDict[a_name].Get(a_parent == null ? transform : a_parent), vfxDict[a_name]);
        }
    }
}
