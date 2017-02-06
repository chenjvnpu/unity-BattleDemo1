using UnityEngine;
using System.Collections;
/// <summary>
/// 枚举主角的战斗状态
/// </summary>
public enum PlayerFightState { 
    Idle,
    Atk,
    Skill,
    Damage,
    Die
}
/// <summary>
/// 控制主角战斗的类
/// </summary>
/// 

//思路
public class PlayerFight : MonoBehaviour {

    private PlayerFightState state = PlayerFightState.Idle;

    private Vector3 targetPos;//目标位置的坐标
    private Transform target;//目标

    public GameObject fightMenu;//战斗菜单
    private bool isDie = false;//是否已经死亡
    private bool isShowed = false;//是否已经显示战斗菜单
    private bool isAtked = false;//是否已经攻击

    private Vector3 startPos;//初始信息
    private Vector3 startRot;//初始角度
    public UISlider actionBarPre;//行动条的预制体
    private GameObject actionBar;//行动条
    public GameObject skillEffect;//技能特效

    public float speed = 1f;//行动速度
    public float HP = 100;//血量
    public int attack = 20;

    private CharacterController controller;//角色控制器
    private Animation anima;//动画组件
    public string animName_Idle;//休闲状态的动画名称
    public string animName_Run;//奔跑状态的动画名称
    public string animName_atk;//攻击状态的动画名称
    public string animName_skill;//技能状态的动画名称
    public string animName_damage;//受伤状态的动画名称
    public string animName_die;//死亡状态的动画名称
    public float atkTimer;//攻击动画的长度


	void Start () {
        
        fightMenu.SetActive(false);//一开始隐藏掉主角战斗菜单
        startPos = transform.position;//储存好开始位置，因为下面在攻击好敌人之后要回到原位置
        startRot = transform.localEulerAngles;//储存好开始的角度

        targetPos = startPos;//一开始的目标位置是本身所在的位置
        controller=transform.GetComponent<CharacterController>();//获取角色控制器组件
        anima=transform.GetChild(0).GetComponent<Animation>();//获取动画组件
        actionBar = NGUITools.AddChild(GameObject.Find("actionBar"), actionBarPre.gameObject) as GameObject;//添加行动条到游戏界面上
        actionBar.transform.localPosition = Vector3.zero;//行动条的位置归零
        actionBar.GetComponent<ActionBar>().speed = speed;//给行动条的速度赋值
	}
	
	void Update () {
        if (isDie)
        {
           
        }
        else {
            if (actionBar.GetComponent<UISlider>().value == 1)//当行动条的位置达到终点，NGUI滑动器的最大值为1，最小值为0，所以value=1,就代表到达最大值
            {
                FightManager.main.state = FightState.Player;//整个游戏的战斗状态为玩家控制
                if (isShowed == false)//如果战斗菜单没有显示
                {
                    isShowed = true;//显示战斗菜单
                    fightMenu.SetActive(true);
                }
            }
            if (FightManager.main.state != FightState.Player)//当整个游戏战斗状态不是玩家控制，则要隐藏战斗菜单
            {
                fightMenu.SetActive(false);
            }
        }
        
        switch (state)
        {
            case PlayerFightState.Idle:
                break;
            case PlayerFightState.Atk:
                PlayerAtk();
                break;
            case PlayerFightState.Skill:
                PlayerSkill();
                break;
            case PlayerFightState.Damage:
                break;
            case PlayerFightState.Die:
                break;
            default:
                break;
        }
	}

    //普通攻击的控制
    void PlayerAtk()
    {
        if (targetPos == startPos && isAtked==false)//当主角的位置为初始位置，且还没有攻击过，isAtked=false 表示还没有攻击过
        {
            if (Input.GetMouseButtonDown(0))//如果按下了鼠标左键，则发出射线检测敌人，获取要攻击的目标
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo))
                {
                    if (hitInfo.collider.tag == Tags.Enemy)
                    {
                        
                        target = hitInfo.collider.gameObject.transform;
                        targetPos = target.position;//获取目标位置
                    }
                }  
            }
        }
        else//当主角的位置不是初始位置，且已经攻击过
        {
            float dis = Vector3.Distance(targetPos,transform.position);
            if (dis > 1.5f)//判断主角和敌人的位置
            {
                if (targetPos != null)//如果目标位置不为零（如果这句不下去好像也可以）
                {
                    transform.LookAt(targetPos);//朝着目标位置看去
                    controller.SimpleMove(transform.forward * 5f);//朝着目标位置跑去
                    anima.CrossFade(animName_Run);//播放奔跑动画
                }
            }
            else {//如果主角和敌人的位置小于1.2f,则表示要 攻击 或者 停止回合
                if (targetPos == startPos)//如果目标为一开始的位置，则停止回合
                {
                    anima.CrossFade(animName_Idle);//播放空闲状态
                    transform.localEulerAngles = startRot;//把自身的角度设置为初始角度
                    transform.localPosition = startPos;//把自身的位置设置为初始位置
                    OnRsetState();//结束一个回合
                }
                else {//如果目标位置不是开始位置，则开始攻击
                    StartCoroutine(WaitAtkEnd());//开始攻击（这里用到了协程，攻击完毕之后会自动退出攻击状态）
                }
            }
           
        }
    }

    //技能
    void PlayerSkill()
    {
        if (target == null && isAtked == false)
        {
            if (Input.GetMouseButtonDown(0))//如果按下了鼠标左键，则发出射线检测敌人，获取要攻击的目标
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo))
                {
                    if (hitInfo.collider.tag == Tags.Enemy)
                    {
                        target = hitInfo.collider.gameObject.transform;
                    }
                }
            }
        }
        else {
            StartCoroutine(WaitSkillEnd());
        }
    }

    //对外接口——普通攻击（战斗菜单的按钮事件，点击 攻击 ，会调用此方法）
    public void OnAttack()
    {
        state = PlayerFightState.Atk;
        isAtked = false;
    }
    //对外接口——技能攻击
    public void OnSkill()
    {
        state = PlayerFightState.Skill;
        isAtked = false;
    }
    //等待播放攻击动画完毕
    IEnumerator WaitAtkEnd()
    {
        anima.CrossFade(animName_atk);
        yield return new WaitForSeconds(atkTimer);
        if (isAtked == false)
        {
            target.gameObject.SendMessage("OnDamage",attack);
            isAtked = true;
        }
        targetPos = startPos;
    }
    //等待播放技能动画完毕
    IEnumerator WaitSkillEnd()
    {
        anima.CrossFade(animName_skill);
        yield return new WaitForSeconds(anima.GetClip(animName_skill).length);
        if (isAtked == false)
        {
            Instantiate(skillEffect, target.position, Quaternion.identity);
            target.gameObject.SendMessage("OnDamage",attack*2);
            isAtked = true;
        }
        target = null;
        OnRsetState();//结束回合
    }

    //结束回合
    void OnRsetState()
    {
        anima.CrossFade(animName_Idle);
        FightManager.main.FinishRound();//结束一个回合
        actionBar.GetComponent<UISlider>().value = 0;//把行动条归零，重新开始一个回合
        state = PlayerFightState.Idle;//结束回合之后一定要把主角战斗状态设置为空闲状态
        isShowed = false;
    }

    //受到伤害
    public void OnDamage(int value = 10)
    {
        if (isDie) return;
        StartCoroutine(waitDamageEnd());
        HP -= value;
        if (HP <= 0)
        {
            isDie = true;
        }
    }
    //等待受伤动画播放完毕
    IEnumerator waitDamageEnd()
    {
        anima.CrossFade(animName_damage);
        yield return new WaitForSeconds(anima.GetClip( animName_damage).length);
        anima.CrossFade(animName_Idle);
    }
}
