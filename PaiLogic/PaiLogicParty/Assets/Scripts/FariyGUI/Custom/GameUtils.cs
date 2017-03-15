using UnityEngine;
using System.Collections;
using PTGame.AssetBundles;
using PTAssetBundle;
using System;
using System.IO;

namespace Putao.PTBook
{
	
	public class GameUtils {

//        /// <summary>
//        /// 截取摄像机的当前帧数据
//        /// </summary>
//		/// <param name="rect">截取的区域</param>
//		/// <param name="cameras">截取的摄像机</param>
//		public static Texture2D CaptureCamera(Rect rect, params Camera[] cameras) {  
//			
//			// 创建一个RenderTexture对象 Depth The type of the depth buffer. None, 16 bit or 24 bit.
//			// maoling: RenderTexture的尺寸一定要和屏幕一样，否则会因为剪裁而丢失camera下绘制的信息
//			RenderTexture rt = new RenderTexture((int)Screen.width, (int)Screen.height, 24);  
//			// 临时设置相关相机的targetTexture为rt, 并手动渲染相关相机  
//            for (int i = 0; i < cameras.Length; i++)
//            {
//                if (cameras[i] != null)
//                {
//                    cameras[i].targetTexture = rt;
//                    cameras[i].Render();
//                }
//            }
//
//            // 激活这个rt, 并从中中读取像素。  
//			RenderTexture.active = rt;  
//			Texture2D screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24,false);  
//			screenShot.ReadPixels(rect, 0, 0);// 注：这个时候，它是从RenderTexture.active中读取像素  
//			screenShot.Apply();
//
//            // 重置相关参数，以使用camera继续在屏幕上显示  
//            for (int i = 0; i < cameras.Length; i++)
//            {
//                if (cameras[i] != null)
//                    cameras[i].targetTexture = null;
//            }
//
//            RenderTexture.active = null; // JC: added to avoid errors  
//			GameObject.Destroy(rt);
//
//			return screenShot;  
//		}
//
//        /* *
//		 * 添加控件到控件
//		 * transform 		要挂的对象
//		 * parent			挂在到的父节点
//		 * */
//        public static Transform Reset(Transform transform, Transform parent) {
//			transform.SetParent(parent);
//			transform.localPosition = Vector3.zero;
//			transform.localRotation = Quaternion.identity;
//			transform.localScale = Vector3.one;
//			return transform;
//		}
//
//		public static AssetBundle GetLoadedAssetBundle(string assetBundleName)
//		{
//			string error;
//			LoadedAssetBundle ab = AssetBundleManager.GetLoadedAssetBundle(assetBundleName.ToLower(), out error);
//			if (string.IsNullOrEmpty(error) && ab != null)
//				return ab.m_AssetBundle;
//			return null;
//		}
//
//		public static AssetBundle LoadAssetBundle (string assetBundleName) {
//			//PTResourceManager.Instance.LoadAssetBundle (assetBundleName.ToLower());
//			//需要返回assetbundle
//			return AssetBundleManager.LoadAssetBundleSync (assetBundleName.ToLower(),false);
//		}
//
//		public static void LoadAssetBundleAsync(string assetBundleName, Action callback) 
//		{
//			PTResourceManager.Instance.LoadAssetBundleAsync(assetBundleName.ToLower(), (ok, ab)=>
//			{
//				if(callback != null) callback();
//			});
//		}
//
//		public static void LoadAssetBundleAsync(string assetBundleName, Action<bool,AssetBundle> action = null) 
//		{
//			PTResourceManager.Instance.LoadAssetBundleAsync(assetBundleName.ToLower(), action);
//		}
//
//		public static T LoadAsset<T> (string assetBundleName, string  assetName)
//			where T : UnityEngine.Object {
//			return PTResourceManager.Instance.LoadAsset<T> (assetBundleName, assetName);
//		}
//
//		public static void LoadAssetAsync<T> (string assetBundleName, string  assetName,
//			Action<bool, T> action) where T : UnityEngine.Object {
//			PTResourceManager.Instance.LoadAssetAsync<T> (assetBundleName, assetName, action);
//		}
//
//		public static void UnLoadAssetBundle (string assetBundleName, bool force = true) {
//			PTResourceManager.UnloadAssetBundle (assetBundleName.ToLower(), force);
//		}
//
//		public static void UnLoadAll() {
//			PTResourceManager.ForceUnloadAllAssetBundle ();
//		}
//
//		public static void TakeShot(out Texture2D texture) {
//			string date = System.DateTime.Now.ToString("yyyyMMddHHmmss");
//
//			int screenWidth = Screen.width;
//			int screenHeight = Screen.height;
//
//			Rect screenRect = new Rect(0, 0, screenWidth, screenHeight);
//
//			texture = new Texture2D(screenWidth, screenHeight, TextureFormat.RGB24, false);
//			texture.ReadPixels(screenRect, 0, 0); 
//			texture.Apply(); 
//		}
//
//		public static void SaveTextureToFile(Texture2D texture, string filepath)
//		{
//			byte[] bytes = texture.EncodeToPNG(); 
//			File.WriteAllBytes(filepath, bytes); 
//		}
//
//		public static Texture2D LoadTextureFromFile(string filePath)
//		{
//			Texture2D tex = null;
//			byte[] fileData;
//			if (File.Exists(filePath))     
//			{
//				fileData = File.ReadAllBytes(filePath);
//				tex = new Texture2D(2, 2);
//				tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
//			}
//			return tex;
//		}
//
//	    /// <summary>
//	    /// 裁剪一张图的一块区域
//	    /// </summary>
//	    public static Texture2D ClipTexture(Texture2D sourceTex, Rect clipRect)
//	    {
//	        int x = Mathf.FloorToInt(clipRect.x);
//	        int y = Mathf.FloorToInt(clipRect.y);
//	        int width = Mathf.FloorToInt(clipRect.width);
//	        int height = Mathf.FloorToInt(clipRect.height);
//
//	        Color[] pix = sourceTex.GetPixels(x, y, width, height);
//	        Texture2D destTex = new Texture2D(width, height);
//	        destTex.SetPixels(pix);
//	        destTex.Apply();
//
//	        return destTex;
//	    }
	}
}
