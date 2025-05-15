using Clubhouse.Helper.Editor;
using UnityEditor;

namespace Clubhouse.Tools.VisualEffects.Editor
{
    [InitializeOnLoad]
    public static class VfxSymbolDefiner
    {
        static VfxSymbolDefiner()
        {
            SymbolDefiner.DefineSymbol("CLUBHOUSE_VFX");
        }
    }
}
