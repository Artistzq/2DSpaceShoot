/** 
 *负责人:
 *版本:
 *提交日期:
 *功能描述:  挂载在子弹实体上
 *修改记录: 
*/  

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour {
	//1 - 设计变量
	/// <summary>
	/// 施加伤害
	/// </summary>
	public int damage = 1;

	//如果
	public bool isEnemyShot = false;
	// Use this for initialization
	void Start () {
		//2限制时间来避免漏洞
		// 20sec后子弹就消失
		Destroy(gameObject, 20); //20sec 
	}
}
