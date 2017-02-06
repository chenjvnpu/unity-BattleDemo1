using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myEnemyMove : MonoBehaviour {
	Transform playerLeader;
	public float speed = 5;
	public float offsetDis=2f;
	public Vector3 offsetPos = new Vector3 (0,0,0.5f);
	// Use this for initialization
	void Start () {
		playerLeader = GameObject.FindGameObjectWithTag ("playerLeader").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (playerLeader != null) {
			float dis = Vector3.Distance (transform.position, playerLeader.position);
//			Vector3 destination = playerLeader.position + (playerLeader.position - transform.position).normalized * offsetDis;
			Vector3  destination=playerLeader.position+offsetPos;
			if(Vector3.Distance (transform.position, destination)>1.5f){
				transform.LookAt (playerLeader);
				transform.Translate (transform.forward*Time.deltaTime*speed,Space.World);

			}
		
		}

		
	}
}
