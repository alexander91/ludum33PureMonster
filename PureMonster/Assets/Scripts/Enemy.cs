using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    [SerializeField]
    bool active = true;

    Renderer renderer;

    class WandererControl
    {
        public WandererControl()
        {

        }

        public float speed = 0.03f;
        public const float maxGoingTime = 3f;

        Vector3 nextPosition;
        float startGoingTime;

        public void GoToPosition(Transform transform)
        {
            transform.position += speed * (nextPosition - transform.position).normalized;
        }

        public void Iterate()
        {
            //if ()
            if ((Time.time - startGoingTime) > maxGoingTime)
            {
                SetNewTarget();
                startGoingTime = Time.time;
            }
        }

        void SetNewTarget()
        {
            nextPosition = RandomUtils.getRandomVector(185f);
            Debug.Log(nextPosition);
            nextPosition.z = 0f;
        }
    }

    WandererControl myWalker = new WandererControl();

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
	void Start () {
        renderer = GetComponent<Renderer>();
        Health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
        if (!active) return;

        myWalker.GoToPosition(transform);
        myWalker.Iterate();
	
	}

    public void DestroySelf()
    {
        killed = true;
        DestroyObject(gameObject);
    }
}
