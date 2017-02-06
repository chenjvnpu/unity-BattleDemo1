using UnityEngine;
using System.Collections;

public class FightMenu : MonoBehaviour {



    public void OnAtkButton() {
        transform.gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<PlayerFight>().OnAttack();
    }

    public void OnSkillButton()
    {
        transform.gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<PlayerFight>().OnSkill();
    }

    public void OnDrugButton()
    {
       // transform.gameObject.SetActive(false);
    }

}
