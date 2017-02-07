using UnityEngine;
using System.Collections;

public class DestroyForTime : MonoBehaviour {

	public float time;

	void Start () {
		Destroy(this.gameObject,time);
	}

}
