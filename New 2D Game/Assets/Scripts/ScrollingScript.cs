/** 
 *负责人:
 *版本:
 *提交日期:
 *功能描述:使图层移动，达到视差滚动效果  
 *修改记录: 
*/  

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 视差滚动脚本，挂载在一个图层上
/// </summary>

public class ScrollingScript : MonoBehaviour 
{
	// 滚动速度
	public	Vector2 speed = new Vector2(2, 2);

	//移动方向
	public Vector2 direction = new Vector2(-1,0);

	//移动应该施加于摄像机上
	//默认不加在摄像机上，所以设置为否(false)
	public bool isLinkedToCamera = false;

	void Start () 
	{
		//移动
		Vector3 movement = new Vector3(
			speed.x * direction.x,
			speed.y * direction.y,
			0
		);

		movement *= Time.deltaTime;
		transform.Translate(movement);
		
		//移动摄像机
		if (isLinkedToCamera)
			Camera.main.transform.Translate(movement);
	}
}
