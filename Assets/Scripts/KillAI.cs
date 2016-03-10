using UnityEngine;
using System.Collections;

public class KillAI : MonoBehaviour {
		
		void OnTriggerEnter2D(Collider2D other)
		{
				GameObject.Destroy(gameObject);
		}
	}
