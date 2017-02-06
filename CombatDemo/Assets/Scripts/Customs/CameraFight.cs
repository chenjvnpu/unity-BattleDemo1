using UnityEngine;
using System.Collections;

public class CameraFight : MonoBehaviour {

    private Vector3 offsetPos;
    private Transform player;
	void Start () {
        player=GameObject.FindGameObjectWithTag(Tags.Player).transform;
        offsetPos = transform.position - player.position;
	}
	
	
	void LateUpdate () {
        transform.position = player.position + offsetPos;
	}
}
