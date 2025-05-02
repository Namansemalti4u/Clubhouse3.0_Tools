using UnityEngine;
using Clubhouse.Helper;

namespace Clubhouse.Tools.Audio
{
    public static class OnLoad
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnRuntimeMethodLoad()
        {
#if !CLUBHOUSE_MAIN
            InitializeAudioManager();
#endif
        }

        private static void InitializeAudioManager()
        {
            Loader.InstantiateFromResources("AudioManager");
        }
    }
}
