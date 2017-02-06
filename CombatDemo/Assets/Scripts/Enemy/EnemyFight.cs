using UnityEngine;
using System.Collections;
public enum EnemyFightState { 
    Idle,
    Atk,
    Die
}
public class EnemyFight : MonoBehaviour {

    public EnemyFightState state = EnemyFightState.Idle;

    private Transform target;//目标
    private Vector3 targetPos;//目标位置
    private Vector3 startPos;//开始的位置
    private Vector3 startRot;//开始的角度

    public float speed=1;//行动速度
    private bool isAtk=false;//是否已经攻击过
    private bool isDie = false;//是否

    public float HP = 100;//血量
    public int attack = 10;//攻击力

    public GameObject actionBarPre;//行动条的预制体
    public GameObject actionBar;//行动条

    private Animation anima;//动画组件
    public string animName_idle;//休闲动画名称
    public string animName_atk;//攻击动画名称
    public string animName_run;//奔跑动画名称
    public string animName_die;//死亡动画名称
    public string animName_damege;//受伤动画名称

	void Start () {
        startPos = transform.position;//获取初始位置
        startRot = transform.localEulerAngles;//获取初始角度
        actionBar = NGUITools.AddChild(GameObject.Find("actionBar"), actionBarPre) as GameObject;//添加行动条到游戏界面上
        actionBar.transform.localPosition = Vector3.zero;//行动条的位置归零
        actionBar.GetComponent<ActionBar>().speed=speed;//给行动条的速度赋值
        anima=transform.GetChild(0).GetComponent<Animation>();//获取动画组件
	}
	
	void Update () {
        if (isDie)
        {
            state = EnemyFightState.Die;
        }
        else {
            if (actionBar.GetComponent<UISlider>().value == 1)//当行动条的位置达到终点，NGUI滑动器的最大值为1，最小值为0，所以value=1,就代表到达最大值
            {
                FightManager.main.state = FightState.Enemy;//把整个游戏的战斗状态为敌人控制
                state = EnemyFightState.Atk;//敌人状态切换为攻击状态
            }
        }

        switch (state)
        {
            case EnemyFightState.Idle:
                break;
            case EnemyFightState.Atk:
                OnAtk();
                break;
            case EnemyFightState.Die:
                StartCoroutine(waitDieEnd());
                break;
            default:
                break;
        }
	}

    void OnAtk()
    {
        if (target == null)//如果还没有攻击过,即没有目标
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag(Tags.Player);//寻找主角
            target = targets[Random.Range(0, targets.Length)].transform;
            targetPos = target.transform.position;//这里为多个主角的时候，敌人会随机选取主角
        }
        else {
            float dis = Vector3.Distance(transform.position, targetPos);//计算与目标之间的距离
            if (dis > 1.5f)//如果与目标之间的距离大于1.5f
            {
                transform.LookAt(targetPos);//朝着目标看去
                transform.Translate(Vector3.forward * 5 * Time.deltaTime);//朝着目标跑去
                anima.CrossFade(animName_run);//播放奔跑动画
            }
            else
            {
                if (targetPos == startPos)//如果目标位置为开始的位置
                {
                    anima.CrossFade(animName_idle);//播放空闲动画
                    transform.localEulerAngles = startRot;//将角度设置为初始角度
                    transform.localPosition = startPos;//将位置设置为初始位置
                    FightManager.main.FinishRound();//结束一个回合
                    actionBar.GetComponent<UISlider>().value = 0;////把行动条归零，重新开始一个回合
                    state = EnemyFightState.Idle;//将敌人的状态切换为空闲状态
                    isAtk = false;//结束一个回合之后把攻击设置为为攻击，为下一个回合做准备
                }
                else
                {
                    StartCoroutine(waitAtkEnd());//开始攻击
                }
            }
        }
    }

    IEnumerator waitAtkEnd()
    {
        anima.CrossFade(animName_atk);
        yield return new WaitForSeconds(anima.GetClip(animName_atk).length);
        if (isAtk == false)
        {
            target.gameObject.SendMessage("OnDamage",attack);
            isAtk = true;
        }
        state = EnemyFightState.Idle;
        targetPos = startPos;
    }

   public void OnDamage(int value)
    {
        StartCoroutine(waitDamageEnd());
        HP -= value;
        if (HP <= 0)
        {
            isDie = true;
            FightManager.main.CostEnemyCount();//敌人队伍中的数量减少一个
        }
    }
    IEnumerator waitDamageEnd()
    {
        anima.CrossFade(animName_damege);
        yield return new WaitForSeconds(anima.GetClip(animName_damege).length);
        anima.CrossFade(animName_idle);
    }

    IEnumerator waitDieEnd()
    {
        anima.CrossFade(animName_die);
        yield return new WaitForSeconds(anima.GetClip(animName_die).length);
        Destroy(this.gameObject);
        Destroy(actionBar);
    }
}
