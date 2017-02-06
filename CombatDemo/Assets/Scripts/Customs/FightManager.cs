using UnityEngine;
using System.Collections;

public enum FightState { 
    Player,
    Enemy,
    Computer,
    Quit
}

public class FightManager : MonoBehaviour {

    public static FightManager main;
    public FightState state = FightState.Computer;
    public ArrayList enemys=new ArrayList();

    public ArrayList  players=new ArrayList();
    private bool isQuit = false;

    private GameObject winText;
    private GameObject loseText;
    
	void Start () {
        main = this;
        winText = GameObject.Find("winText");
        loseText = GameObject.Find("loseText");
        winText.SetActive(false);
        loseText.SetActive(false);
        GameObject[] pls=GameObject.FindGameObjectsWithTag(Tags.Player);
        GameObject[] es=GameObject.FindGameObjectsWithTag(Tags.Enemy);
        for (int i = 0; i < pls.Length; i++)
        { 
           players.Add(pls[i]);
        }
        for (int i = 0; i < es.Length; i++)
        {
            enemys.Add(es[i]);
        }
	}
    public void FinishRound()
    {
        if (isQuit)
        {
            state = FightState.Quit;
        }
        else {
            state = FightState.Computer;
        }
    }

    public void CostEnemyCount()
    {
        enemys.RemoveAt(0);
        if (enemys.Count <= 0)
        {
            winText.SetActive(true);
            Invoke("QuitFight",3f);
            isQuit = true;
        }
    }

    public void CostPlayerCount()
    {
        players.RemoveAt(0);
        if (players.Count <= 0)
        {
            loseText.SetActive(true);
            isQuit = true;
        }
    }

    void QuitFight()
    {
        Application.LoadLevel("04_loading");
        Globe.LevelIndex = 1;
    }
}
