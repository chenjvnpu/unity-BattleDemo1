using UnityEngine;
using System.Collections;

public class FllowPlayer : MonoBehaviour {

	private Vector3 offsetPosition;//相机与主角的偏移量
	private Transform player;

	private float distance=0;//相机的视野距离
	private float scrollSpeed=4;//改变相机视野的速度
	private float rotateSpeed=4;//相机旋转的速度
	private bool isRotate=false;//是否可以旋转
	void Awake()
	{

		player=GameObject.FindGameObjectWithTag(Tags.Player).transform;
		offsetPosition=new Vector3(0,2,-3);
		transform.position=offsetPosition;
		transform.localEulerAngles=new Vector3(15,0,0);

	}

	void LateUpdate()
	{
		transform.position=offsetPosition+player.position;//相机的跟随主角的位移
		if(UICamera.hoveredObject==null){
			RotateView();//旋转功能
			ScrollView();//缩放相机功能
		}
	}
	
    //相机视野的缩放
	void ScrollView()
	{
		distance=offsetPosition.magnitude;
		distance-=Input.GetAxis ("Mouse ScrollWheel")*scrollSpeed;//相机的远近由鼠标的中键控制
		distance = Mathf.Clamp (distance,3,18);
		offsetPosition = offsetPosition.normalized * distance;
	}
	
    //相机角度的旋转
	void RotateView()
	{
		if (Input.GetMouseButtonDown (1)) //如果按下鼠标右键
			isRotate = true;//可以旋转
		
		if(Input.GetMouseButtonUp(1))//如果抬起鼠标右键
			isRotate = false;//不可以旋转
		
        //如果可以旋转
		if (isRotate) {
			Vector3 organizPosition=transform.position;
			Quaternion organizRotation=transform.rotation;
			//根据水平和垂直方向的增量来控制相机旋转的速度
			transform.RotateAround(player.transform.position,Vector3.up,Input.GetAxis("Mouse X")*rotateSpeed);
			transform.RotateAround(player.transform.position,transform.right,-Input.GetAxis("Mouse Y")*rotateSpeed);
			
			float x=transform.eulerAngles.x;
			if(x<20||x>80)
			{
				transform.position=organizPosition;
				transform.rotation=organizRotation;
			}
			x=Mathf.Clamp(x,20,80);//相机旋转的角度控制，在垂直方向上不能无限旋转
			transform.eulerAngles=new Vector3(x,transform.eulerAngles.y,transform.eulerAngles.z);
			offsetPosition = transform.position - player.transform.position;
		}
		
	}
}
