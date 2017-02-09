using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myPlayerMove : MonoBehaviour {
	public float speed = 8f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("世超嘚儿的呵");
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		if(Mathf.Abs(h)>0.1f || Mathf.Abs(v)>0.1f){
			transform.LookAt (transform.position+new Vector3(h,0,v));
			transform.Translate (transform.forward*Time.deltaTime*speed,Space.World);
		}
		
	}
}
