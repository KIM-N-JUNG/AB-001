using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

/*
public class BuildScript
{
    public static void BuildGame()
    {
        var scenes = new string[] { "Assets/game/Scens/MainMenu", "Assets/game/Scens/AvoidBullets" };
        var flags = BuildOptions.Development;
        BuildPipeline.BuildPlayer(scenes, "AB001.apk", BuildTarget.Android, flags);
    }
}
*/

public static class BuildScript 
{
    public static void BuildAndroid() 
    {
        //Console.Out.WriteLine("[LOG] BuildAndroid Start");

        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.Android);

        List<string> enableScenePathList = new List<string>();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (false == scene.enabled) { continue; }
            enableScenePathList.Add(scene.path);
            //Console.Out.WriteLine("[LOG] EnabledBuildScene " + scene);
        }

        string binaryFilePath = UnityEngine.Application.dataPath + "/../" + "Binary/Android/binary.apk";
        if (false == File.Exists(binaryFilePath))
        {
            FileInfo fileInfo = new FileInfo(binaryFilePath);
            fileInfo.Directory.CreateSubdirectory(fileInfo.DirectoryName);
        }

        BuildTarget buildTarget = BuildTarget.Android;
        BuildOptions buildOption = BuildOptions.None;

        //Console.Out.WriteLine("[LOG] Binary Path : " + binaryFilePath);
        //Console.Out.WriteLine("[LOG] BuildTarget : " + buildTarget.ToString());
        //Console.Out.WriteLine("[LOG] BuildOptions : " + buildOption.ToString());

        BuildPipeline.BuildPlayer(enableScenePathList.ToArray(), binaryFilePath, buildTarget, buildOption);
        //UnityEditor.Build.Reporting.BuildReport
        //Console.Out.WriteLine("[LOG] BuildResult " + result);
    }
}

/*
public class BuildScript {
    static void IncrementBundleVersion() {
        string versionText = System.IO.File.ReadAllText("version.txt");

        if (versionText != null) {
            versionText = versionText.Trim(); //clean up whitespace if necessary
            string[] lines = versionText.Split('.');

            int MajorVersion = int.Parse(lines[0]);
            int MinorVersion = int.Parse(lines[1]);
            int SubMinorVersion = int.Parse(lines[2]) + 1; //increment here

            versionText = MajorVersion.ToString() + "." +
                MinorVersion.ToString() + "." +
                SubMinorVersion.ToString();

            PlayerSettings.bundleVersion = versionText;
            PlayerSettings.Android.bundleVersionCode = SubMinorVersion;
            System.IO.File.WriteAllText("version.txt",versionText);
        }
    }

    public static void DoCommonBuildStuff(string outPath) {
        string[] levels = {"Assets/game/Scenes/MainMenu.unity", "Assets/game/Scenes/AvoidBullets.unity" };

        PlayerSettings.applicationIdentifier = "com.kimnjung.AB-001";
        //PlayerSettings.Android.keystorePass = "KEYSTORE_MASTER_PASSWORD_GOES_HERE";
        //PlayerSettings.Android.keyaliasName = "AB-001";
        //PlayerSettings.Android.keyaliasPass = "KEY_PASSWORD_GOES_HERE";

        IncrementBundleVersion();

        BuildPipeline.BuildPlayer(levels, outPath, BuildTarget.Android, BuildOptions.None);
    }

    public static void BuildAndroid() {
        //PlayerSettings.Android.keystoreName = "/home/geoff/dev/Keystore/My_Publishing_Keys.keystore";
        DoCommonBuildStuff("/Users/jiunpapa/devel/git/AB-001/testapp.apk");
    }
}
*/
