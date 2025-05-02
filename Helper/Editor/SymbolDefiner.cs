#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Clubhouse.Helper.Editor
{
    [InitializeOnLoad]
    public static class SymbolDefiner
    {
        static SymbolDefiner()
        {
            DefineSymbol("CLUBHOUSE_HELPER");
        }

        public static void DefineSymbol(string a_symbol)
        {
            string symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
            if (symbols.Contains(a_symbol)) return;

            symbols += ";" + a_symbol;
            PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, symbols);
            Debug.Log($"Added define symbol: {a_symbol}");
        }

        public static void RemoveSymbol(string a_symbol)
        {
            string symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
            if (!symbols.Contains(a_symbol)) return;
            
            var newSymbols = symbols.Replace(a_symbol, "").Replace(";;", ";").Trim(';');
            PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, newSymbols);
            Debug.Log($"[Package] Removed define symbol: {a_symbol}");
        }
    }
}
#endif

