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

/// <summary>
/// 敌人的通用行为
/// </summary>

public class EnemyScript : MonoBehaviour 
{
	private WeaponScript[] weapons;

	private void Awake() 
	{
		//只重新激活一次武器
		weapons = GetComponentsInChildren<WeaponScript>(); //实例化
	}
		
	// Update is called once per frame
	void Update () 
	{
		//此处保证了敌人不会被自己的子弹消灭；
		foreach (WeaponScript weapon in weapons)
		{
		//自动开火
		//如果实例化成功，且weapon.CanAttack不变（默认为1）；
		//那么，执行函数weapon.Attack(true)
		//因为传递给Attack函数的变量为真，
		//则，Weapon里的shot类（脚本）的变量isEnemyShot变为true
		//则，挂载在敌人的脚本HealthScript里的OnTriggerEnter2D函数里的if语句不执行
		//则，敌人Hp不减少，敌人不消灭
			if (weapon != null && weapon.CanAttack)
			{
				weapon.Attack(true);
			}
		}	
	}
}
