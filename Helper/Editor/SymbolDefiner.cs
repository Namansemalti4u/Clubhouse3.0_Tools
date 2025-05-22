#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Clubhouse.Helper.Editor
{
    [InitializeOnLoad]
    public static class SymbolDefiner
    {
        // List of target groups you want to modify
        private static readonly BuildTargetGroup[] TargetGroups = new[]
        {
            BuildTargetGroup.Android,
            BuildTargetGroup.Standalone,
            BuildTargetGroup.iOS
        };

        static SymbolDefiner()
        {
            DefineSymbol("CLUBHOUSE_HELPER");
        }

        public static void DefineSymbol(string a_symbol)
        {
            foreach (var group in TargetGroups)
            {
                string symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(group);
                if (HasSymbol(group, a_symbol)) continue;

                symbols += ";" + a_symbol;
                PlayerSettings.SetScriptingDefineSymbolsForGroup(group, symbols);
                Debug.Log($"Added define symbol: {a_symbol} on platform: {group}");
            }
        }

        public static void RemoveSymbol(string a_symbol)
        {
            foreach (var group in TargetGroups)
            {
                string symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(group);
                if (!HasSymbol(group, a_symbol)) continue;

                var newSymbols = symbols.Replace(a_symbol, "").Replace(";;", ";").Trim(';');
                PlayerSettings.SetScriptingDefineSymbolsForGroup(group, newSymbols);
                Debug.Log($"[Package] Removed define symbol: {a_symbol}");
            }
        }

        public static bool HasSymbol(BuildTargetGroup a_group, string a_symbol)
        {
            string symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(a_group);
            return symbols.Contains(a_symbol);
        }
    }
}
#endif

