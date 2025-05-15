using UnityEngine;
using Clubhouse.Helper;

namespace Clubhouse.Tools
{
    public static class OnLoad
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnRuntimeMethodLoad()
        {
#if !CLUBHOUSE_MAIN
            Loader.InstantiateFromResources("HapticManager");
#endif
        }
    }
}
