using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace Clubhouse.Helper
{
    public static class Terminal
    {
        public static Task<string> RunCommand(string cmd, string path = "")
        {
            var tcs = new TaskCompletionSource<string>(); // Task to hold the result
            Process process = new Process();

#if UNITY_EDITOR_WIN
            string shellPath = @"C:\Program Files\Git\bin\bash.exe";
            if (!File.Exists(shellPath))
            {
                UnityEngine.Debug.LogError("Git Bash not found at: " + shellPath);
                return tcs.Task;
            }
            process.StartInfo.FileName = shellPath;
#elif UNITY_EDITOR_OSX || UNITY_EDITOR_LINUX
            process.StartInfo.FileName = "/bin/bash";
#endif

            process.StartInfo.Arguments = $"-c \"{cmd}\"";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            if (!string.IsNullOrEmpty(path))
                process.StartInfo.WorkingDirectory = path;

            string output = "", error = "";

            process.OutputDataReceived += (sender, args) =>
            {
                if (!string.IsNullOrEmpty(args.Data))
                    output += args.Data + "\n";
            };

            process.ErrorDataReceived += (sender, args) =>
            {
                if (!string.IsNullOrEmpty(args.Data))
                    error += args.Data + "\n"; ;
            };

            process.Exited += (sender, args) =>
            {
                tcs.SetResult(output.Trim()); // Set the output when the process exits
            };

            process.EnableRaisingEvents = true;
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            if (!string.IsNullOrEmpty(error)) UnityEngine.Debug.LogError(error.Trim());

            return tcs.Task;
        }
    }
}