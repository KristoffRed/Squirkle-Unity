using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;
using System;
using System.IO.Compression;

namespace Squirkle.Editor
{
    public class BuildVersionHelper
    {
        [PostProcessBuild(1)]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
        {
            Debug.Log($"Created version file in: {pathToBuiltProject + "/version.json"}");
            File.WriteAllText(pathToBuiltProject + "/version.json", "{\n" + $"\t\"buildDate\": \"{DateTime.UtcNow}\"\n" + "}");

            ZipFiles(target, pathToBuiltProject);
        }

        public static void ZipFiles(BuildTarget target, string pathToBuiltProject)
        {
            // pathToBuiltProject is the root directory of your build
            string rootBuildPath = pathToBuiltProject;

            // 1. Path to the index.html file
            string indexHtmlPath = Path.Combine(rootBuildPath, "index.html");

            if (File.Exists(indexHtmlPath))
            {
                File.Delete(indexHtmlPath);
                Debug.Log("Post-Build: Removed index.html");
            }

            // 2. Identify the internal 'Build' folder
            string internalBuildFolder = Path.Combine(rootBuildPath, "Build");

            if (!Directory.Exists(internalBuildFolder))
            {
                Debug.LogWarning("Post-Build: Internal 'Build' folder not found. Skipping zip.");
                return;
            }

            // 3. Define the Zip path
            string zipPath = Path.Combine(rootBuildPath, "Game.zip");

            if (File.Exists(zipPath))
            {
                File.Delete(zipPath);
            }

            // 4. Zip the internal Build folder
            try
            {
                Debug.Log($"Post-Build: Zipping {internalBuildFolder}...");
                ZipFile.CreateFromDirectory(internalBuildFolder, zipPath);
                Debug.Log("Post-Build: Zip created successfully.");

                // 5. Delete the internal Build folder after successful zipping
                if (File.Exists(zipPath))
                {
                    // Recursive: true ensures all subfolders/files inside are deleted
                    Directory.Delete(internalBuildFolder, true);
                    Debug.Log("Post-Build: Cleaned up internal 'Build' folder.");
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Post-Build: Failed to zip or delete folder. Error: {e.Message}");
            }
        }
    }

}
