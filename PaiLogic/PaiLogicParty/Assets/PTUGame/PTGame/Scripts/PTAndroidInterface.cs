using UnityEngine;
using System.Collections;
using PTGame.Core;



	public class PTAndroidInterface
	{

		private static PTAndroidInterface androidInterface;

		public static PTAndroidInterface Instance {
			get { 
				if (androidInterface == null) {
					androidInterface = new PTAndroidInterface ();
				}
				return androidInterface;
			}
		}

		AndroidJavaObject jo;

		AndroidJavaClass ptcustom;


		public PTAndroidInterface ()
		{
		
			AndroidJavaClass jc = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			jo = jc.GetStatic<AndroidJavaObject> ("currentActivity");

			ptcustom = new AndroidJavaClass ("com.putao.ptgame.PTCustom"); 


		}

		public void ShowNewAppVersionInAppStore (string url, string content, string confirm, string cancle)
		{
			ptcustom.CallStatic ("ShowNewAppVersionInAppStore", new object[]{ jo, url, content, confirm, cancle });
	
		}

		public void ShowNewAppVersionInAppStoreForceUpdate (string url, string content, string confirm)
		{
			ptcustom.CallStatic ("ShowNewAppVersionInAppStoreForceUpdate", new object[]{ jo, url, content, confirm });

		}

	/// <summary>
	/// Executes the keyboard adjust.
	/// 为了解决unity 在android 上弹出键盘后，webcamtexture卡顿的问题
	/// </summary>
	   public void ExecuteKeyboardAdjust(){
		  
		   ptcustom.CallStatic("StartKeyboardAdjust", jo);
	   }

		public int GetAppVersionCode ()
		{
			return ptcustom.CallStatic<int> ("GetAppVersionCode", jo);
		}

		public AndroidJavaObject GetAndroidJavaObj ()
		{
			return jo;
		}
		public AndroidJavaObject GetPTCustomJavaObj(){
			return ptcustom;
		}

		//SetPaiBotAngleForGame
		public void CallMethod (string methodName, params object[] args)
		{
			jo.Call (methodName, args);
		}

		public string GetPTDeviceType ()
		{
		
			return jo.CallStatic<string> ("GetPTDeviceType");
		}

	}
	public class PTAndroidDeviceType{
		public const string UNKNOW="unknown";
		public const string PAIPAD="Paipad";
		public const string PAIBOT = "Paibot";
	}

	
