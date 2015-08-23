using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class MonsterPart : MonoBehaviour
{
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy.Killed) return;
            enemy.Health--;
            if (enemy.Killed) copyStrategy.IterateForCreation();
        }
        if (other.gameObject.transform.tag == "Wall")
        {
            DestroySelf();
        }

        if (other.gameObject.transform.tag == "Bullet")
        {
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            bullet.DestroySelf();
            DestroySelf();
        }
    }

    const float length = 0.8f;
    public const float AverageSpeed = 0.1f;

    public delegate void MonsterPartDestroyed(MonsterPart monsterPart);
    public event MonsterPartDestroyed onMonsterPartDestroyed;

    [SerializeField]
    GameObject myCopy;
    CopyingStrategy copyStrategy;

    List<MonsterPart> neighbors = new List<MonsterPart>();
    public void addNeighbor(MonsterPart neighbor)
    {
        neighbors.Add(neighbor);
        neighbor.onMonsterPartDestroyed += (destroнedPart) => { neighbors.Remove(destroнedPart); };

        neighbor.neighbors.Add(this);
        onMonsterPartDestroyed += (destroнedPart) => { neighbor.neighbors.Remove(destroнedPart); };
    }

    Vector3 accel;
    public void AddAccel(Vector3 targetAccel)
    {
        accel += 0.01f * targetAccel;
    }
    Vector3 pos;
    Vector3 speed;

    public Vector3 Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public void Iterate()
    {
        var nowSavedPos = pos;
        //var targetPos = pos + 0.05f*toAddPos;
        pos += speed;
        speed += accel;
        accel = Vector3.zero;
        speed *= 0.9f;

        pos.z *= 0.6f;
        transform.position = pos;
    }

    void Awake()
    {
        copyStrategy = new CopyingStrategy(this);

    }

    // Use this for initialization
    void Start()
    {
        this.speed = Vector3.zero;
        this.pos = transform.position;
        this.accel = Vector3.zero;


        Game.Instance.Manager.AddPart(this);

    }


    void getAdderFromNeighbors()
    {
        foreach (var neigh in neighbors)
        {
            Vector3 d = neigh.pos - pos;
            float lNow = d.magnitude;
            AddAccel( (lNow - length) * d.normalized * AverageSpeed / (AverageSpeed + speed.magnitude) );
        }
    }

    void FixedUpdate()
    {
        getAdderFromNeighbors();
    }

    // Update is called once per frame
    void Update()
    {
        Iterate();
    }

    void DestroySelf()
    {
        if (onMonsterPartDestroyed != null)
            onMonsterPartDestroyed(this);
        Game.Instance.Manager.RemovePart(this);
        DestroyObject(gameObject);
    }
}
