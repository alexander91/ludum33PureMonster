using UnityEngine;
using System.Collections;

public class EnemyCreator : MonoBehaviour {

    [SerializeField]
    GameObject enemy;

    [SerializeField]
    GameObject shootingEnemy;

    [SerializeField]
    GameObject shootingEnemyExplosive;

    [SerializeField]
    GameObject shootingEnemyExplosiveFat;

    [SerializeField]
    GameObject shootingEnemyFast;


    [SerializeField]
    GameObject tower;

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
        if (count > 2 * 20)
        {
            count = 0;

            if (UnityEngine.Random.value < Mathf.Min((Time.timeSinceLevelLoad / 200f), 0.04f))
            {
                createEnemy(tower);
                return;
            }

            if (UnityEngine.Random.value < Mathf.Min((Time.timeSinceLevelLoad / 160f), 0.07f))
            {
                createEnemy(shootingEnemyExplosive);
                return;
            }

            if (UnityEngine.Random.value < Mathf.Min((Time.timeSinceLevelLoad - 30f / 240f), 0.02f))
            {
                createEnemy(shootingEnemyExplosiveFat);
                return;
            }

            if (UnityEngine.Random.value < Mathf.Min((Time.timeSinceLevelLoad - 20f / 150f), 0.05f))
            {
                createEnemy(shootingEnemyFast);
                return;
            }

            if (UnityEngine.Random.value < Mathf.Min((Time.timeSinceLevelLoad / 160f), 0.24f))
            {
                createEnemy(shootingEnemy);
                return;
            }
            createEnemy(enemy);
        }
	
	}
}
