using UnityEngine;
using Clubhouse.Helper;

namespace Clubhouse.Tools.VisualEffects
{
    public static class OnLoad
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnRuntimeMethodLoad()
        {
#if !CLUBHOUSE_MAIN
            Loader.InstantiateFromResources("VfxManager");
#endif
        }
    }
}
