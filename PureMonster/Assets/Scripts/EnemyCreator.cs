using UnityEngine;
using System.Collections;

public class EnemyCreator : MonoBehaviour {

    [SerializeField]
    GameObject enemy;

    [SerializeField]
    GameObject shootingEnemy;

    int count;
    
	// Use this for initialization
	void Start () {
	
	}
	
    GameObject createEnemy(GameObject prefab)
    {
        GameObject newEnemy = Instantiate(prefab);
        Vector3 pos = newEnemy.transform.position;

        newEnemy.transform.parent = this.transform;

        pos.x = UnityEngine.Random.Range(-40f, 40f);
        pos.y = UnityEngine.Random.Range(-40f, 40f);

        newEnemy.transform.position = pos;

        return newEnemy;
    }

	// Update is called once per frame
	void Update () {
        count++;
        if (count > 2 * 60)
        {
            count = 0;

            if (UnityEngine.Random.value < (Time.timeSinceLevelLoad / 300f))
            {
                createEnemy(shootingEnemy);
                return;
            }
            createEnemy(enemy);
        }
	
	}
}
