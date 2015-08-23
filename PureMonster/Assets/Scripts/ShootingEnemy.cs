using UnityEngine;
using System.Collections;

public class ShootingEnemy : Enemy {

    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    const int timeToRecreate = 90;
    int respown = 0;

	// Use this for initialization
	protected override void Start () {
        base.Start();
	
	}
	
	// Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (respown > 0)
        {
            respown--;
            return;
        }

        var playerPos = Game.Instance.Manager.CenterOfMonster;
        if ((transform.position - Game.Instance.Manager.CenterOfMonster).magnitude < Bullet.distanceMax * 0.9f)
        {
            GameObject bullet = (GameObject)Instantiate(bulletPrefab);
            bullet.transform.position =  transform.position;
            MyMathOnObject.setupRotation(bullet.transform, transform.position, playerPos, 90f);
            bullet.GetComponent<Bullet>().Speed = 0.05f * (playerPos - transform.position).normalized;

            respown = timeToRecreate;
        }
	
	}
}
