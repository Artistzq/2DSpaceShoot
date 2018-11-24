﻿/** 
 *负责人:
 *版本:
 *提交日期:
 *功能描述:  
 *修改记录: 
*/  

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
	[SerializeField]
    [Tooltip("飞船前进的速度")]
	public Vector2 speed = new Vector2(50, 50);

	[SerializeField]
    [Tooltip("储存移动和组件")]
	private Vector2 movement;
	private Rigidbody2D rigidbodyComponent;

	// Update is called once per frame
	void Update () 
	{
		//重定义轴数值
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");

		//每个方向上的移动，速度和方向相乘
		movement = new Vector2(
			speed.x * inputX,
			speed.y * inputY
		);
	}

	void FixedUpdate() 
	{	
		//获取组件并储存引用
		if(rigidbodyComponent == null) 
			rigidbodyComponent = GetComponent<Rigidbody2D>();
		//移动游戏物体
		rigidbodyComponent.velocity = movement;
	}
}