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

		//射击
		bool shoot = Input.GetButtonDown("Fire1");
		shoot |= Input.GetButtonDown("Fire2");
		//注意：对于Mac用户，ctrl+箭头不是个好主意

		if (shoot)
		{
			//调用WeaponScript里的Attack函数，传递的参数为bool值false
			WeaponScript weapon = GetComponent<WeaponScript>();
			if (weapon != null)
			{
				//用false是因为，玩家（player）不是敌人(enemy)
				weapon.Attack(false);
			}
		}
	}

	void FixedUpdate() 
	{	
		//获取组件并储存引用
		if(rigidbodyComponent == null) 
			rigidbodyComponent = GetComponent<Rigidbody2D>();
		//移动游戏物体
		rigidbodyComponent.velocity = movement;
	}

	/// <summary>
	/// 此函数实现玩家与敌人碰撞
	/// </summary>
	/// <param name="collision"></param>
	private void OnCollisionEnter2D(Collision2D collision) //括号里其他碰撞体
	{
		bool damagePlayer = false;

		//和敌人碰撞
		//collision.gameObject:The GameObject whose collider you are colliding with. (Read Only).
		//你正在与之碰撞体碰撞的物体，即为collision.gameObject；
		EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
		if (enemy != null)
		{
			//杀死敌人
			//获得enemy（敌人对象）的HealthScrit组件
			HealthScript enemyHealth = enemy.GetComponent<HealthScript>();
			if (enemyHealth != null)
				enemyHealth.Damage(enemyHealth.hp);	
				//给敌人造成和hp一样大小的伤害
				//即消灭敌人
			
			damagePlayer = true;
		}

		//销毁玩家
		if (damagePlayer) //只有敌人销毁成功，damagePlayer才会变成true
		                  //如果不销毁敌人，该变量为false，不执行此if语句
		{
			HealthScript playerHealth = this.GetComponent<HealthScript>();
			if (playerHealth != null)
				playerHealth.Damage(playerHealth.hp);
		}
	}
}
