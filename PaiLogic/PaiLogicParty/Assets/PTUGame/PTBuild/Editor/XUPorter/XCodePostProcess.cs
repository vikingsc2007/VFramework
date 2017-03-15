using UnityEngine;
using PTGame.Builder;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.XCodeEditor;
#endif
using System.IO;

public static class XCodePostProcess
{

#if UNITY_EDITOR
	[PostProcessBuild(999)]
	public static void OnPostProcessBuild( BuildTarget target, string pathToBuiltProject )
	{
		if (target != BuildTarget.iOS) {
			Debug.LogWarning("Target is not iPhone. XCodePostProcess will not run");
			return;
		}

		// Create a new project object from build target
		XCProject project = new XCProject( pathToBuiltProject );

		// Find and run through all projmods files to patch the project.
		// Please pay attention that ALL projmods files in your project folder will be excuted!
		string[] files = Directory.GetFiles( Application.dataPath, "*.projmods", SearchOption.AllDirectories );
		foreach( string file in files ) {
			UnityEngine.Debug.Log("ProjMod File: "+file);
			project.ApplyMod( file );
		}

		//TODO implement generic settings as a module option
		project.overwriteBuildSetting("PROVISIONING_PROFILE", PTBuilderCore.PROVISIONING_PROFILE, "Debug");
		project.overwriteBuildSetting("PROVISIONING_PROFILE", PTBuilderCore.PROVISIONING_PROFILE, "Release");
		project.overwriteBuildSetting("CODE_SIGN_IDENTITY", PTBuilderCore.CODE_SIGN_IDENTITY, "Debug");
		project.overwriteBuildSetting("CODE_SIGN_IDENTITY", PTBuilderCore.CODE_SIGN_IDENTITY, "Release");
		project.overwriteBuildSetting("CODE_SIGN_ENTITLEMENTS", "../KeychainAccess.plist");

		project.overwriteBuildSetting ("ENABLE_BITCODE","NO");


		
		// Finally save the xcode project
		project.Save();

	}
#endif

	public static void Log(string message)
	{
		UnityEngine.Debug.Log("PostProcess: "+message);
	}
}
