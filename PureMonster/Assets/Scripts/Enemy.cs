using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    [SerializeField]
    bool active = true;

    new Renderer renderer;

    [SerializeField]
    float speed = 0.01f;

    [SerializeField]
    int reward = 2;

    public int Reward
    {
        get { return reward; }
    }



    class WandererControl
    {
        public WandererControl()
        {
            SetNewTarget();
        }

        
        public const float maxGoingTime = 3f;

        Vector3 nextPosition;
        Vector3 lastPos;
        float startGoingTime;

        public void GoToPosition(Enemy enemy)
        {
            lastPos = enemy.transform.position;
            enemy.transform.position += enemy.speed * (nextPosition - lastPos).normalized;
        }

        public void Iterate()
        {
            var playerCenter = Game.Instance.Manager.CenterOfMonster;
            if ((playerCenter - lastPos).sqrMagnitude < 225f)
            {
                startGoingTime = Time.time;
                nextPosition = lastPos + (lastPos - playerCenter);
            }
            if ((Time.time - startGoingTime) > maxGoingTime)
            {
                SetNewTarget();
                startGoingTime = Time.time;
            }
        }

        void SetNewTarget()
        {
            nextPosition = RandomUtils.getRandomVector(185f);
            nextPosition.z = 0f;
        }
    }

    WandererControl myWalker;

    bool killed = false;

    public bool Killed
    {
        get { return killed; }
        set { killed = value; }
    }


    [SerializeField]
    int maxHealth = 1;
    int health;

    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            renderer.material.color = Color.Lerp(Color.blue, Color.white, 1.0f - (((float)health) / maxHealth));
            if (health == 0)
            {
                DestroySelf();
            }

        }
    }
    
	// Use this for initialization
	protected virtual void Start () {
        myWalker = new WandererControl();

        renderer = GetComponent<Renderer>();
        Health = maxHealth;
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        if (!active) return;

        myWalker.GoToPosition(this);
        myWalker.Iterate();
	
	}

    public void DestroySelf()
    {
        killed = true;        
        DestroyObject(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.tag == "Wall")
        {
            DestroySelf();
        }
    }
}
