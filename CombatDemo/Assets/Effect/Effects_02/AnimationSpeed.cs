using UnityEngine;
using System.Collections;

public class AnimationSpeed : MonoBehaviour {
	
	public AnimationClip anim;
	public float speedAnim;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Animation>()[anim.name].speed = speedAnim;
	}
}
