using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {
	
	public string followTagName;
	private GameObject target;
	
	// Use this for initialization
	void Start () {
		
		target = GameObject.FindGameObjectWithTag(followTagName);
	
	}
	
	// Update is called once per frame
	void Update () {
		
		this.transform.position = new Vector3(target.transform.position.x,transform.position.y,target.transform.position.z);
	
	}
}
