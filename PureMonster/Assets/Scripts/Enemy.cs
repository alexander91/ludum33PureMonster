using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    Renderer renderer;

    class WandererControl
    {
        public float speed = 0.01f;
        public const float turnSpeed = 0.1f;
        public const float maxGoingTime = 5f;

        Vector3 nextPosition;
        float startGoingTime;

        public void TurnToPosition(Transform objectToTurn)
        {
            objectToTurn.rotation *= (Quaternion.Lerp(
                Quaternion.FromToRotation(objectToTurn.rotation * Vector3.right, nextPosition - objectToTurn.position), 
                Quaternion.identity, turnSpeed));
        }

        public void Iterate()
        {
            if ((Time.time - startGoingTime) > maxGoingTime)
            {
                SetNewTarget();
                startGoingTime = Time.time;
            }
        }

        void SetNewTarget()
        {
            nextPosition = RandomUtils.getRandomVector(45f);
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
        transform.position += myWalker.speed * Vector3.right;
        myWalker.TurnToPosition(transform);
        myWalker.Iterate();
	
	}

    public void DestroySelf()
    {
        killed = true;
        DestroyObject(gameObject);
    }
}
