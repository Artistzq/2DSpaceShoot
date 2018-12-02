/** 
 *负责人:
 *版本:
 *提交日期:
 *功能描述:  
 *修改记录: 
*/  

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour {
	//设计变量
	/// <summary>
	/// 物体速度
	/// </summary>
	public Vector2 speed = new Vector2(10, 10);
	/// <summary>
	/// 移动方向
	/// </summary>
	public Vector2 direction = new Vector2(-1, 0);

	private Vector2 movement;
	private Rigidbody2D rigidbodyComponent;

	// Update is called once per frame
	void Update () 
	{
		//移动
		movement = new Vector2(
			speed.x * direction.x,
			speed.y * direction.y
		);
	}

	private void FixedUpdate() 
	{
	//	if(rigidbodyComponent==null)
			rigidbodyComponent = GetComponent<Rigidbody2D>();
		rigidbodyComponent.velocity = movement;	
	}
}
