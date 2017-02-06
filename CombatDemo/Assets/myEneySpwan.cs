using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myEneySpwan : Singleton<myEneySpwan> {
	public GameObject enemyPrefab;
	public List<Transform> spwanPoints = new List<Transform> ();
	public List<GameObject> enemyList=new List<GameObject>();
	int round=0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SpwanEnemy(){
		StartCoroutine (GeneralEnemy ());

	}

	IEnumerator GeneralEnemy(){
		if (round < 3) {
			round++;
			for (int i = 0; i < spwanPoints.Count; i++) {
				GameObject item = Instantiate (enemyPrefab,spwanPoints[i].position,Quaternion.identity);
				enemyList.Add (item);
				yield return null;
			}
		
		}

	}

}
