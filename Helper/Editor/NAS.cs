using System;
using System.IO;
using UnityEngine;

namespace Clubhouse.Helper
{
    public static class NAS
    {
        public enum Platform
        {
            Windows,
            Android,
            iOS,
        }

        public static string RootURI
        {
            get
            {
                if (Application.platform == RuntimePlatform.WindowsEditor)
                    return "file://10.1.1.165/Gaming/Clubhouse";
                else if (Application.platform == RuntimePlatform.OSXEditor)
                    return "file:///Volumes/Gaming/Clubhouse";
                else
                    throw new System.Exception("Unsupported platform for NAS path");
            }
        }

        public static string RootPath
        {
            get
            {
                return new Uri(RootURI).LocalPath;
            }
        }

        public static string AssetBundlesURI
        {
            get
            {
                return Path.Combine(RootURI, "Asset%20Bundle%20Update");
            }
        }

        public static string AssetBundlesPath
        {
            get
            {
                return Path.Combine(RootPath, "Asset Bundle Update");
            }
        }

        public static string GetAssetBundlesPath(Platform a_platform)
        {
            return Path.Combine(AssetBundlesPath, GetPlatformFolderName(a_platform));
        }

        public static string GetAssetBundlesURI(Platform a_platform)
        {
            return Path.Combine(AssetBundlesURI, GetPlatformFolderName(a_platform));
        }   

        private static string GetPlatformFolderName(Platform a_platform)
        {
            return a_platform switch
            {
                Platform.Windows => "WindowsTest",
                Platform.Android => "Android",
                Platform.iOS => "IOS",
                _ => throw new System.Exception("Unsupported platform for NAS path")
            };
        }
    }
}
