using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class test2Script : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static bool checkVersion(){
		int level=	SceneManager.GetActiveScene ().buildIndex;
		Debug.Log (level);
		if (level == 0) {
			return true;
		} else
			SceneManager.LoadScene (0);
		return false;
		
	}
}
