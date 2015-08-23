using UnityEngine;
using System.Collections;

public class EnemyCreator : MonoBehaviour {

    [SerializeField]
    GameObject enemy;

    int count;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        count++;
        if (count > 3 * 60)
        {
            count = 0;
            GameObject newEnemy = Instantiate(enemy);
            Vector3 pos = newEnemy.transform.position;

            newEnemy.transform.parent = this.transform;
            
            pos.x = UnityEngine.Random.Range(-40f, 40f);
            pos.y = UnityEngine.Random.Range(-40f, 40f);

            newEnemy.transform.position = pos;

        }
	
	}
}
