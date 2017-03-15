using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Framework.Basics{
	public class BaseManager<T> where T : new()   
	{
		public static T Instance  
		{  
			get { return SingletonCreator.instance; }  
		}  
		class SingletonCreator  
		{  
			internal static readonly T instance = new T();  
		}  


		public virtual void Init (){
			
		}




	}
}