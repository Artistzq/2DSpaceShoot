/** 
 *负责人:
 *版本:
 *提交日期:
 *功能描述:  挂载在敌人和玩家上
 *修改记录: 
*/  

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour {
	///<summary>
	/// 总共生命值
	///</summary>	
	public int hp = 1;

	/// <summary>
	/// 敌人还是玩家?
	/// 敌人则布尔变量为ture，碰到玩家的子弹，销毁；碰到敌人自己子弹，没事；
	/// 玩家则布尔变量为false，碰到玩家子弹没事，
	/// </summary>
	public bool isEnemy = true;
	
	//造成伤害并检查物体是否应被摧毁
	//这个销毁的是敌人
	public void Damage(int damageCount)
	{
		hp -= damageCount;
		
		if (hp <= 0)
		{
			//死！
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D otherCollider)//当子弹碰到实体
	{
		//这是个射击吗？
		ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();
		//我的理解：脚本实例化了，unity的功能，脚本名就是一个大类，也就是类实例化

		if (shot != null)
		{
			//避免误伤友军
			//if括号里的条件：前者默认false；
			//如果Health挂载在玩家上，false ！= false 返回 flase，不执行销毁；
			//如果Health挂载在敌人上，false ！=true 返回 true，执行销毁
			if (shot.isEnemyShot != isEnemy)
			{
				Damage(shot.damage);//这个销毁的是该脚本挂载的对象，敌人/玩家
				Destroy(shot.gameObject);//这个销毁的是子弹
				//记得要标记gameobject，不然你就会移除脚本
				//我的理解：如果写成Destroy(shot),那脚本就被移除了，而不是消除掉物体
			}
		}
	}
}
