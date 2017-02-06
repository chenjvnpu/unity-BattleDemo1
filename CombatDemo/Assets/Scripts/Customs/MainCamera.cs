using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {

    private Vector3 offsetPos;
    private Transform player;
	void Start () {
        player = GameObject.FindGameObjectWithTag(Tags.Player).transform;
        offsetPos = transform.position - player.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = offsetPos + player.position;
	}
}
