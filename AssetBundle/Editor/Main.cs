using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Clubhouse.Tools.AssetBundle.Editor
{
    public static class Main
    {
        private static string assetBundlesPath = "AssetBundles"; // Relative to project

        [MenuItem("Tools/Asset Bundle/Build For All Platforms")]
        public static void CreateBundle()
        {
            Debug.Log("Creating bundle");

            BuildForWindows();
            BuildForAndroid();
            BuildForIOS();
        }

        [MenuItem("Tools/Asset Bundle/Build For Windows")]
        public static void BuildForWindows() => BuildAndUpload(BuildTarget.StandaloneWindows64);

        [MenuItem("Tools/Asset Bundle/Build For Android")]
        public static void BuildForAndroid() => BuildAndUpload(BuildTarget.Android);

        [MenuItem("Tools/Asset Bundle/Build For iOS")]
        public static void BuildForIOS() => BuildAndUpload(BuildTarget.iOS);

        private static void Experiment()
        {
            string path = GetNASPath("WindowsTest");
            Debug.Log($"NAS path: {path} exists: {Directory.Exists(path)}");
            // string path2 = Path.Combine(Path.Combine(assetBundlesPath, "Windows"), "swipeit");
            // Debug.Log($"File exists: {File.Exists(path2)} at {path2}");
        }

        private static void BuildAndUpload(BuildTarget a_target)
        {
            string outputPath = GetOutputPath(
                a_target switch
                {
                    BuildTarget.StandaloneWindows64 => "StandaloneWindows",
                    BuildTarget.Android => "Android",
                    BuildTarget.iOS => "iOS",
                    _ => throw new Exception($"Unsupported target: {a_target}")
                }
            );
            
            string[] bundles = BuildForPlatform(a_target, outputPath);
        
            string nasPath = GetNASPath(
                a_target switch
                {
                    BuildTarget.StandaloneWindows64 => "WindowsTest",
                    BuildTarget.Android => "Android",
                    BuildTarget.iOS => "IOS",
                    _ => throw new Exception($"Unsupported target: {a_target}")
                }
            );

            foreach (var bundle in bundles)
            {
                string srcFilePath = Path.Combine(outputPath, bundle);
                string dstFilePath = Path.Combine(nasPath, bundle + a_target switch
                {
                    BuildTarget.StandaloneWindows64 => "",
                    BuildTarget.Android => "",
                    BuildTarget.iOS => "IOS",
                    _ => throw new Exception($"Unsupported target: {a_target}")
                });

                CopyFile(srcFilePath, dstFilePath);
            }
        }

        private static string[] BuildForPlatform(BuildTarget a_target, string a_outputPath)
        {
            if (!Directory.Exists(a_outputPath))
                Directory.CreateDirectory(a_outputPath);

            ClearDirectory(a_outputPath);

            AssetBundleManifest manifest = BuildPipeline.BuildAssetBundles(a_outputPath, BuildAssetBundleOptions.None, a_target);
            Debug.Log($"✅ AssetBundles built for {a_target} at: {a_outputPath}");

            return manifest.GetAllAssetBundles();
        }

        private static string GetOutputPath(string a_folderName)
        {
            return Path.Combine(assetBundlesPath, a_folderName);
        }

        private static string GetNASPath(string a_folderName)
        {
            string root = "";
#if UNITY_EDITOR
            if (Application.platform == RuntimePlatform.WindowsEditor)
                root = @"\\10.1.1.165\Gaming\Clubhouse\Asset Bundle Update";
            else if (Application.platform == RuntimePlatform.OSXEditor)
                root = "/Volumes/Gaming/Clubhouse/Asset Bundle Update";
            else
                throw new System.Exception("Unsupported platform for NAS path");
#endif
            return Path.Combine(root, a_folderName);
        }

        private static void ClearDirectory(string a_path)
        {
            var files = Directory.GetFiles(a_path);
            foreach (var file in files)
            {
                File.Delete(file);
            }

            var dirs = Directory.GetDirectories(a_path);
            foreach (var dir in dirs)
            {
                Directory.Delete(dir, true);
            }
        }

        private static void CopyFile(string oldFilePath, string newFilePath)
        {
            if (File.Exists(oldFilePath))
            {
                File.Copy(oldFilePath, newFilePath, true);
                Debug.Log($"File copied from {oldFilePath} to {newFilePath}");
            }
            else
            {
                Debug.LogError($"File not found: {oldFilePath}");
            }
        }
    }
}
