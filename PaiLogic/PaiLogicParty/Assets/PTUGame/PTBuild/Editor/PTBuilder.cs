using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Reflection;

namespace PTGame.Builder
{
	public class PTBuilder 
	{
		/// <summary>
		/// 编译QA测试版本（提交fir.im版本）
		/// </summary>
		 [MenuItem("PuTaoTool/Build_IOS/Build_XCode_Test", false, 0)]
		private static void BuildForInner()
		{
			PTBuilderCore.BuildForInner ();
		}
		/// <summary>
		/// 编译AppStore版本
		/// </summary>
		[MenuItem("PuTaoTool/Build_IOS/Build_XCode_AppStore", false, 2)]
		private static void BuildForOnline()
		{
			PTBuilderCore.BuildForOnline ();
		}
		/// <summary>
		/// 编译真机调试版本
		/// </summary>
		[MenuItem("PuTaoTool/Build_IOS/Build_XCode_Device", false, 1)]
		private static void BuildForOnDevice()
		{
			PTBuilderCore.BuildForOnDevice ();
		}

		/// <summary>
		/// Jekins 远程打包
		/// </summary>
		public static void CmdBuild(){
			string[] args = System.Environment.GetCommandLineArgs ();
			PTBuilderCore.CmdBuild (args);
		}

	}

}
