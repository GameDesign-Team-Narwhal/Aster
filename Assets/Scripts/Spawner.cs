using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	
	public GameObject [] prefabs;
	float delay = 2.0f;
	public bool active = true;
	public bool WorldSpawner = true;
	public Vector2 delayRange = new Vector2 (1, 2);
	
	// Use this for initialization
	void Start () {
		ResetDelay ();
		StartCoroutine(EnemyGenerator() );
	}
	
    public void SpawnOne()
    {
		Vector3 location = new Vector3();
		location.x = transform.position.x;
		location.y = transform.position.y;
		location.z = transform.position.z;
		if (WorldSpawner) {
			GameObjectUtil.Instantiate (prefabs [Random.Range (0, prefabs.Length)], GameController.instance.RandomLocationInLevel ()); 
		} else {
			GameObjectUtil.Instantiate (prefabs [Random.Range (0, prefabs.Length)],  location); 
		}
    }

    IEnumerator EnemyGenerator(){
		
		yield return new WaitForSeconds (delay);
		
		if (active) {

            SpawnOne();
			ResetDelay();
			
		}
		
		StartCoroutine(EnemyGenerator() );
		
	}
	
	void ResetDelay(){
		delay = Random.Range (delayRange.x, delayRange.y);
	}
	
	
	
}