using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace multi_tower_attack_3d
{
	public class HardObjectScript : MonoBehaviour
	{

		public float rotMax;
		// Use this for initialization
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

			transform.localEulerAngles = new Vector3(0, -Mathf.PingPong(Time.time * 50, rotMax), 0);
		}
	}
}
