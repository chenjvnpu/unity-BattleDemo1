using UnityEngine;
using System.Collections;

public class Loading : MonoBehaviour {

	public UIProgressBar processBar;

	AsyncOperation async;
	private uint _nowprocess;

	void Start () {
		_nowprocess = 0;
		StartCoroutine(loadScene());
	}
	

	void Update () {

		if(async == null)
		{
			return;
		}
		
		uint toProcess;

		if(async.progress < 0.9f)
		{
			toProcess = (uint)(async.progress * 100);
		}
		else
		{
			toProcess = 100;
		}
		
		if(_nowprocess < toProcess)
		{
			_nowprocess++;
		}
		
		processBar.value = _nowprocess/100f;
		
//		if (_nowprocess == 100)//async.isDone应该是在场景被激活时才为true
//		{
//			async.allowSceneActivation = true;
//		}
	}


//注意这里返回值一定是 IEnumerator
IEnumerator loadScene()
{
	//异步读取场景。
	//Globe.loadName 就是A场景中需要读取的C场景名称。
		async = Application.LoadLevelAsync(Globe.LevelIndex);
		//async.allowSceneActivation = false;
		//读取完毕后返回， 系统会自动进入C场景
		yield return null;

		
	}


}
