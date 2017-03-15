using UnityEngine;
using System.Collections;

/// <summary>
/// 类明不可以更改
/// </summary>
public class PTCustomPreBuild {


	public static void PreBuildXcodeDevice(){
		//processes before call ptbuild
		Debug.Log ("PreBuildXcodeDevice");
	}
	public static void PreBuildXcodeAppStore(){
		//processes before call ptbuild
		Debug.Log ("PreBuildXcodeAppStore");
	}
	public static void PreBuildXcodeFirm(){
		//processes before call ptbuild
		Debug.Log ("PreBuildXcodeFirm");
	}


}
