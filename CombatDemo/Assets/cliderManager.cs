using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cliderManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision col){
		Debug.Log (col.gameObject.name);
	}

	void OnTriggerEnter(Collider collider)
	{
		Debug.Log ("OnTriggerEnter:"+collider.gameObject.name);
		if(collider.gameObject.name=="player"){
			myEneySpwan.Instance.SpwanEnemy ();
		}

	}
}
