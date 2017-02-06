using UnityEngine;
using System.Collections;

public enum PlayerState{//枚举玩家的动画状态
	Idle,
	Run,
    Die
}

public class PlayerAnim : MonoBehaviour {

	private PlayerMove move;
	public PlayerState state;

    private bool isDie = false;
	void Awake()
	{
		move=this.GetComponent<PlayerMove>();
	}

	void Update()
	{
        if (isDie)
        {
            PlayAnimation("Dead1");
        }else

            if (state == PlayerState.Idle)
            {
                PlayAnimation("Idle");
            }
            if (state == PlayerState.Run)
            {
                PlayAnimation("Run");
            }
	}


	void PlayAnimation(string animName)
	{
		transform.GetChild(0).GetComponent<Animation>().CrossFade(animName);
	}
    IEnumerator Dead()
    {
        PlayAnimation("Dead");
        yield return new WaitForSeconds(1.3f);
        isDie = true;
        

    }
}
