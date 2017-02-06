using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

	public GameObject MouseClikEffectPre;
	public Vector3 targetPosition;	//主角要移动的目标
	public bool isMoving=false;//是否正在移动
	private float speed=4;	//移动速度
	private CharacterController controller;//角色碰撞器
	private PlayerAnim playerAnim;
   


	void Awake()
	{
		controller=this.GetComponent<CharacterController>();
		playerAnim=this.GetComponent<PlayerAnim>();
       
		targetPosition=transform.position;//一开始的移动目标为本身，这样才不会进行移动
	}
    void Start()
    {

    }
	void Update()
	{
        OnPlayerMove();
		if (Input.GetMouseButtonDown (0)) {//按下鼠标左键
            if (UICamera.hoveredObject != null) return;
			Ray ray=Camera.main.ScreenPointToRay (Input.mousePosition);//发出一条射线
			RaycastHit hitInfo;
            if((Physics.Raycast(ray, out hitInfo)) && hitInfo.collider.tag == Tags.Ground)//如果射线检测到鼠标点击了地面
			{

                ShowEffect(hitInfo.point);
				isMoving=true;//主角进行移动
			}
		}
		else{
			isMoving = false;
		}
		if(Input.GetMouseButtonUp (0)){//当鼠标左键抬起时，主角不再移动
			isMoving = false;
		}

		if(isMoving)//如果可以移动，那么
		{
			Ray ray=Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			bool isCollider = Physics.Raycast (ray,out hit);
			if(isCollider && hit.collider.tag==Tags.Ground)
			{
				LookAtTargetPos(hit.point);//主角朝着目标位置位移
			}
		}



	}
	
    //主角的移动功能函数的接口
	void OnPlayerMove()
	{
        
            float distance = Vector3.Distance(this.transform.position, targetPosition);//计算主角与目标点的位置
            //print (distance);
            if (distance > 0.3f)//如果与目标位置不重合，则一直为移动状态
            {
                playerAnim.state = PlayerState.Run;
                controller.SimpleMove(transform.forward * speed);//主角移动实现
            }
            else
            {
                playerAnim.state = PlayerState.Idle;//如果与目标位置重合了，则主角切换至休闲状态
                isMoving = false;
            }
       
	}
   
    //主角的移动函数
    public void SimpleMove(Vector3 target)
    {
        transform.LookAt(target);
        controller.SimpleMove(transform.forward * speed);
    }

	//主角朝着目标看去
	void LookAtTargetPos(Vector3 hitPoint)
	{

		targetPosition=new Vector3(hitPoint.x,transform.position.y,hitPoint.z);

		transform.LookAt (targetPosition);
	}


	//实例化出特效
	void ShowEffect(Vector3 point)
	{
		point = new Vector3 (point.x,point.y+0.2f,point.z);
		Instantiate (MouseClikEffectPre,point,Quaternion.identity);
	}
}
