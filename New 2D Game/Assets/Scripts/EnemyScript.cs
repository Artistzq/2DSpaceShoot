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
	private bool hasSpawn;
	private MoveScript moveScript;
	private WeaponScript[] weapons;
	private Collider2D colliderComponent;
	private SpriteRenderer rendererComponent;	

	private void Awake() 
	{
		//只重新激活一次武器
		weapons = GetComponentsInChildren<WeaponScript>(); //实例化

		//当不需要重生的时候，让脚本失效
		moveScript = GetComponent<MoveScript>();

		colliderComponent = GetComponent<Collider2D>();

		rendererComponent = GetComponent<SpriteRenderer>();
	}
		
	//1.让所有失效
	private void Start() 
	{
		hasSpawn = false;//还未生成

		//使所有失效
		//----collider
		colliderComponent.enabled = false;
		//----Moving
		moveScript.enabled = false;
		//----shooting
		foreach (WeaponScript weapon in weapons)
		{
			weapon.enabled = false;
		}	
	}

	void Update () 
	{
		//2.检查敌人是否生成
		if (hasSpawn == false)
		{
			if (rendererComponent.IsVisibleFrom(Camera.main))
				Spawn();
		}
		else
		{
			//自动开火
			foreach (WeaponScript weapon in weapons)
			{
				if (weapon != null && weapon.enabled && weapon.CanAttack)
				{
					weapon.Attack (true);
				}
			}

			//超出屏幕?销毁物体
			if (rendererComponent.IsVisibleFrom(Camera.main) == false)
			{
				Destroy(gameObject);
			}
		}





		/* 
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
		*/
	}

	//3.激活自己
	private void Spawn()
	{
		hasSpawn = true;

		//激活所有
		//--Collider
		colliderComponent.enabled = true;
		//--Moving
		moveScript.enabled = true;
		//--shooting
		foreach (WeaponScript weapon in weapons)
		{
			weapon.enabled = true;
		}
	}
}
