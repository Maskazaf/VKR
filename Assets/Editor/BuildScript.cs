using UnityEditor;
using UnityEngine;

public class BuildScript
{
    public static void PerformBuild()
    {
        string[] scenes = { "Assets/Scenes/VKR.unity" };
        string pathToBuild = "Builds/Android/app.apk";

        BuildPipeline.BuildPlayer(scenes, pathToBuild, BuildTarget.Android, BuildOptions.None);
    }
}
