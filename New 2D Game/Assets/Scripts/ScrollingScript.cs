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
using System.Linq;

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

	//1.背景无限,默认不无限
	public bool isLooping = false;

	//2.渲染器子类的一个列表
	//私有变量储存图层子类
	private List<SpriteRenderer> backgroundPart;

	//3.获取所有子类
	private void Start() 
	{
		//只让背景无限
		if (isLooping)
		{
			//获取所有含有渲染器的这一层的子类
			backgroundPart = new List<SpriteRenderer>();

			for(int i=0; i < transform.childCount; i++)
			{
				Transform child = transform.GetChild(i);
				SpriteRenderer r = child.GetComponent<SpriteRenderer>();

				//只添加可见的子类
				if (r != null)
					backgroundPart.Add(r);
			}	

			//按位置排序
			//笔记: 从左到右获取子类
			//我们需要添加一点情形
			//去搞定所有可能的滚动方向
			//按x坐标排序，x小，即位置在左边的，排到队列的前面
			backgroundPart = backgroundPart.OrderBy(
				t => t.transform.position.x
			).ToList();
		}
	}

	void Update () 
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
		//如果与摄像机相链接，if执行，则摄像机也移动
		//前景与摄像机相链接
		if (isLinkedToCamera)
			Camera.main.transform.Translate(movement);

		//4.循环
		if (isLooping)
		{
			//获取第一个物体储存在List中
			//这个列表从左到右
			SpriteRenderer firstChild = backgroundPart.FirstOrDefault(); 
			//傳回序列的第一個項目；如果找不到任何項目，則傳回預設值。
			
			if (firstChild != null)
			{
				//检查这个子物体是否已经（部分地）在摄像机前面，
				//我们首先测试位置，因为这个IsVisibleFrom方法调用有点繁重
				if (firstChild.transform.position.x < Camera.main.transform.position.x)
				{	
					//如果这个子类已经在摄像机的左边，
					//我们测试它是否!!!完全!!!地在外面且需要循环
					if (firstChild.IsVisibleFrom(Camera.main) == false)
					{
						//获取最后一个子类的位置
						SpriteRenderer lastChild = backgroundPart.LastOrDefault();

						Vector3 lastPosition = lastChild.transform.position;
						Vector3 lastsize = (lastChild.bounds.max - lastChild.bounds.min);

						//将被循环的那个子类的位置设置到最后一个子类的后面去。
						//注意：当前只在水平滚动有效

						firstChild.transform.position = new 
						Vector3 (lastPosition.x + lastsize.x, firstChild.transform.position.y,
						firstChild.transform.position.z);
						
						//将这个被循环的子类设置到背景渲染器列表的最后一个
						backgroundPart.Remove(firstChild);
						backgroundPart.Add(firstChild);				
					}
				}
			}
		}
	}
}
