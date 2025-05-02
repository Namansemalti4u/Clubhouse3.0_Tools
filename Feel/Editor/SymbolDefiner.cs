using Clubhouse.Helper.Editor;
using UnityEditor;

namespace Clubhouse.Tools.Editor
{
    [InitializeOnLoad]
    public static class FeelSymbolDefiner
    {
        static FeelSymbolDefiner()
        {
            SymbolDefiner.DefineSymbol("CLUBHOUSE_FEEL");
        }
    }
}
