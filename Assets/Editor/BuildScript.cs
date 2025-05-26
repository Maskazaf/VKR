using UnityEditor;

public class BuildScript
{
    public static void BuildAndroid()
    {
        BuildPipeline.BuildPlayer(
            new[] { "Assets/Scenes/MainScene.unity" },
            "build/Android/ARApp.apk",
            BuildTarget.Android,
            BuildOptions.None
        );
    }
}
