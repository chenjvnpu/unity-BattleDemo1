using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {


    public void OnStartButtonClick()
    {
        Application.LoadLevel("04_loading");//先加载loading场景
        Globe.LevelIndex = 1;//再设置要加载的场景，1为要加载的场景的场景索引
    }

    public void OnLoadButtonClick()
    { 
        
    }

    public void OnExitButtonClick()
    {
        Application.Quit();
    }
}
