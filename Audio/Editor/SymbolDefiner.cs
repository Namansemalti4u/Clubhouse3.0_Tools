using Clubhouse.Helper.Editor;
using UnityEditor;

namespace Clubhouse.Tools.Audio.Editor
{
    [InitializeOnLoad]
    public static class AudioSymbolDefiner
    {
        static AudioSymbolDefiner()
        {
            SymbolDefiner.DefineSymbol("CLUBHOUSE_AUDIO");
        }
    }
}
