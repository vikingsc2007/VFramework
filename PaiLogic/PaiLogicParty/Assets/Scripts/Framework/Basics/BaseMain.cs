using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace Framework.Basics{
	public abstract class BaseMain:MonoBehaviour
	{
//		public void Awake(){
//			Debug.Log(typeof(MonoBehaviour).Name);
//		}
		public static GameObject Root;

		void Awake(){
			DontDestroyOnLoad (Root);
			Root = gameObject;



			InitManager ();
		}



		private void InitManager(){
			InitCoreManager ();
			InitGameManager ();
		}

		private void InitCoreManager(){
			PTGame.AssetBundles.PTResourceManager.Instance.Initialize ();
			Framework.Managers.UIManager.Instance.Init ();
			BindGameUIBinder ();
		}

		public abstract void InitGameManager ();


		public abstract void BindGameUIBinder ();

	}
}