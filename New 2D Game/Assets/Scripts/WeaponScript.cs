/** 
 *负责人:
 *版本:
 *提交日期:
 *功能描述:  在该脚本控制的游戏对象前，实例化一个子弹实体。
			挂载在player上;或者敌人上
 *修改记录: 
*/  

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour 
{
/// <summary>
/// 发射子弹 	
/// </summary>
 	
	//----------------------- 
	// 1.设计变量
	//-----------------------

	//子弹预制的射击
	public Transform shotPrefab; // Transform用于储存并操控物体的位置、旋转和缩放。
	
	//两次射击之间的冷却时间（sec）
	public float shootingRate = 0.25f;
	
	//------------------------------
	// 2.冷却
	//------------------------------

	private float shootCooldown;  //私有变量，射击冷却

	void Start () 
	{
		shootCooldown = 0f;	
	}
	
	void Update () 
	//如果冷却大于0，则减去一个delta time，原因未知
	//目的:使冷却（shottCooldown）始终小于等于0；
	{
		if(shootCooldown > 0)
			shootCooldown -= Time.deltaTime;	
	}

	//--------------------------
	// 3. 从其他脚本射击
	//--------------------------

	// 可能的话，创建一个新的子弹
	//攻击
	public void Attack(bool isEnemy)
	{
		if(CanAttack)
		{
			shootCooldown = shootingRate; 

			//创建一个新的投射物
			// Instantiate 克隆原始物体并返回克隆物体，就是召唤一个实体子弹
			var shotTranform = Instantiate(shotPrefab) as Transform;

			//分配位置，我的理解：就在该脚本挂载的player的位置
			shotTranform.position = transform.position;

			//这是敌人的性质（功能）
			//调用ShotScript
			//并将此处isEnemy的值赋给其中的布尔变量isEnemyShot（默认为否）
			//此isEnemy为Attack函数的形参，调用Attack时传递
			//如isEnemy为false，则shot.isEnemyShot为false，一旦碰撞触发，-
			//-即销毁子弹，消灭敌人。
			ShotScript shot = shotTranform.gameObject.GetComponent<ShotScript>();
			if (shot != null)
			{
				shot.isEnemyShot = isEnemy;
			}

			//确保武器总是朝它射击
			//使子弹方向向右
			MoveScript move = shotTranform.gameObject.GetComponent<MoveScript>();
			if (move != null)
			{
				//2D空间的方向（移动方向）（move.direction）
				//是这个图像的右边（Weaponscript脚本（this）的对象的右边）
				move.direction = this.transform.right; 			
			}
		}
	}

	/// <summary>
	/// 这个武器准备好创建一个新投射物了吗？
	/// 如果shootCooldown<=0成立，函数返回true给变量 CanAttack
	/// 就能执行WeaponScript.Attack()函数
	/// </summary>
	public bool CanAttack
	{
		//get用法暂时不明
		get
		{
			return shootCooldown <= 0f;
		}
	}
}
