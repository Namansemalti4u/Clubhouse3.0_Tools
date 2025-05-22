using Clubhouse.Helper.Editor;
using UnityEditor;

namespace Clubhouse.Tools.VisualEffects.Editor
{
    [InitializeOnLoad]
    public static class VfxSymbolDefiner
    {
        static VfxSymbolDefiner()
        {
            AddSymbol();
        }

        [MenuItem("Tools/VisualFX/Define Symbol")]
        static void DefineSymbol()
        {
            AddSymbol();
        }

        private static void AddSymbol()
        {
            SymbolDefiner.DefineSymbol("CLUBHOUSE_VFX");
        }
    }
}
