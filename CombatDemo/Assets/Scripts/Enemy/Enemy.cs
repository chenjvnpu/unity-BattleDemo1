using UnityEngine;
using System.Collections;
/// <summary>
/// 敌人的巡逻
/// </summary>
/// 
public enum EnemyState { 
    Idle,
    Walk,
    Run,
    Atk,
    Die
}
public class Enemy : MonoBehaviour {

    public float distance_atk=5f;//追击的范围
    public float walkSpeed = 2f;//巡逻的速度
    public float runSpeed = 4f;//追击的速度
    private float changStateRate = 1;//巡逻状态的改变速率
    private float timer = 0;
    private Transform target;//目标
    public EnemyState state = EnemyState.Idle;
    private Animation anima;//动画组件
    //动画名字
    public string animName_idle;
    public string animName_walk;
    public string animName_run;
	void Start () {
	    anima=transform.GetChild(0).GetComponent<Animation>();
	}
	
	void Update () {
        print(state);
        if (target == null)//如果目标为空，则进行巡逻状态
        {
            timer += Time.deltaTime;
            if (timer > changStateRate)
            {
                timer= 0;
                RandState();//随机一个巡逻状态
            }
            Collider[] cols = Physics.OverlapSphere(transform.position, distance_atk);//在追击的范围内检测主角
            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i].tag == Tags.Player)
                    target = cols[i].transform;
            }
        }
        else {
            float dis = Vector3.Distance(transform.position,target.position);//计算与目标的距离
            if (dis > 1.2f)
            {
                LookTargetRun();//朝着目标奔跑
            }
            else
            {
                state = EnemyState.Idle;//进入战斗模式
                Application.LoadLevel("04_loading");
                Globe.LevelIndex = 2;
            }
           if(dis>distance_atk)//如果大于追击的距离，则丢失目标
            {
                target = null;
                state = EnemyState.Idle;
            }
        }

        //状态切换机
        switch (state)
        {
            case EnemyState.Idle:
                anima.CrossFade(animName_idle);
                break;
            case EnemyState.Walk:
                anima.CrossFade(animName_walk);
                transform.Translate(Vector3.forward*Time.deltaTime*walkSpeed);
                break;
            case EnemyState.Run:
                anima.CrossFade(animName_run);
                transform.Translate(Vector3.forward * Time.deltaTime * runSpeed);
                break;
            default:
                break;
        }
	}

    //随机一个状态
    void RandState()
    {
        if (Random.Range(0, 2) == 1)
        {
            if (state != EnemyState.Idle)
            {
                transform.localEulerAngles = new Vector3(0, Random.Range(0, 360f), 0);
            }
            state = EnemyState.Walk;
        }
        else {
            state = EnemyState.Idle;
        }
    }
    //朝着目标奔跑
    void LookTargetRun()
    {
        transform.LookAt(target);
        state = EnemyState.Run;
    }
}
