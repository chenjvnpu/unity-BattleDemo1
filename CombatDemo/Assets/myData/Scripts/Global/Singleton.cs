using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T:Singleton<T> {

	protected static T instance;

	public static T Instance{
		get{ 
			if (instance == null) {
				object obj = new object ();
				lock(obj){
					instance = FindObjectOfType<T> ();
					if(instance==null){
						instance = new GameObject (typeof(T).Name,typeof(T)).GetComponent<T>();
						//instance = new GameObject (typeof(T).Name).AddComponent<T>();

					}
				}
				
			} 

			return instance;
		}
	}

	void Start () {
		Debug.Log ("---------singleton start");
	}
}
