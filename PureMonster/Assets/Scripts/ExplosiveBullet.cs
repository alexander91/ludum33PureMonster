using UnityEngine;
using System.Collections;

public class ExplosiveBullet : Bullet {

    [SerializeField]
    GameObject explotion;

    public override void DestroySelf()
    {
        Instantiate(explotion, transform.position, transform.rotation);
        base.DestroySelf();
    }
}
