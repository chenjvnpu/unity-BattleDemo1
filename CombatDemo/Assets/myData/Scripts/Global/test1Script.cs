using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test1Script : Singleton<test1Script> {

	// Use this for initialization
	void Start () {
		Debug.Log ("---------test1start");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void init(){
		Debug.Log ("---------test1 init");
	}
}
