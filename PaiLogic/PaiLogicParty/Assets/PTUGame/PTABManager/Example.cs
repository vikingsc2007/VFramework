﻿using UnityEngine;
using System.Collections;
using PTGame.Core;

namespace PTGame.AssetBundles
{
	public class Example : MonoBehaviour
	{

		// Use this for initialization
		void Start ()
		{
			PTDebug.EnableLog = true;
//			LoadBySync ();

			LoadByAsync ();

//			PTResourceManager.Instance.Initialize ();
//
//			Texture t = PTResourceManager.Instance.LoadAsset<Texture>(PTAssetBundle.Uisprite.BundleName,PTAssetBundle.Uisprite.UISPRITE_DRAWLINELEDBLUECLOSE);
		
//			Instantiate (t);
		}

		/// <summary>
		/// 同步方法加载
		/// </summary>
		private void LoadBySync ()
		{
			PTDebug.Log ("bbbbbbbbb");
			PTResourceManager.Instance.Initialize ();
//			PTResourceManager.Instance.LoadAssetBundle ("effects");
//			GameObject cubePref = PTResourceManager.Instance.LoadAsset<GameObject> ("effects", "Cube");
//			GameObject.Instantiate (cubePref);

			PTResourceManager.Instance.LoadAssetBundle ("Model");

			GameObject t = PTResourceManager.Instance.LoadAsset<GameObject> ("model", "cube");
			Instantiate (t);
//			TestCube testCube = t.GetComponent<TestCube> ();
//			Debug.Log ("bbbbbbbbbbb");
//			TestObject testObj = testCube.t;
//
////			Debug.Log (testCube.t+":kkkkkkkkkkk"+testCube.gameObject.name);
////			GameObject gb = Instantiate (testCube.gameObject);
//
//			Debug.Log("bbbbb>>>>>>"+testObj.bb);
//			Instantiate (testObj.bb);
		}

		private void LoadByAsync ()
		{
			PTResourceManager.Instance.InitializeAsync (InitializeFinished);

		}

		private void InitializeFinished (bool result)
		{
//			PTResourceManager.Instance.LoadAssetAsync<GameObject> (PTAssetBundle.Model.BundleName, PTAssetBundle.Model.CUBE, (bool success, GameObject t) => {
//				if (success) {
//					Instantiate (t);
//					//同步加载 前提是该assetbundle 已明确加载成功了
//					GameObject gameObj = PTResourceManager.Instance.LoadAsset<GameObject> (PTAssetBundle.Model.BundleName,  PTAssetBundle.Model.CUBE);
//					Instantiate (gameObj).transform.position-=Vector3.left*2;
//					Debug.Log ("同步加载：" + gameObj);
//								
//				}
//			});
		}
	
		// Update is called once per frame
		void Update ()
		{
			
		}
	}
}
