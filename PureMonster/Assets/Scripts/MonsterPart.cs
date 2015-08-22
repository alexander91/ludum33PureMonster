using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class MonsterPart : MonoBehaviour
{

    const float length = 2.5f;

    public delegate void MonsterPartDestroyed(MonsterPart monsterPart);
    public event MonsterPartDestroyed onMonsterPartDestroyed;

    [SerializeField]
    GameObject myCopy;
    CopyingStrategy copyStrategy;

    List<MonsterPart> neighbors = new List<MonsterPart>();
    public void addNeighbor(MonsterPart neighbor)
    {
        neighbors.Add(neighbor);
        neighbor.onMonsterPartDestroyed += (destroedPart) => { neighbors.Remove(destroedPart); };

        neighbor.neighbors.Add(neighbor);
        onMonsterPartDestroyed += (destroedPart) => { neighbor.neighbors.Remove(destroedPart); };
    }

    Vector3 toAddPos;
    public Vector3 TargetPosAdder
    {
        get { return toAddPos; }
        set { toAddPos = value; }
    }
    Vector3 pos;
    Vector3 oldPos;

    const float oldAffection = 0.6f;
    const float maxDelta = 0.4f;


    public void Iterate()
    {
        var nowSavedPos = pos;
        var targetPos = pos + toAddPos;
        pos += pos - oldPos;
        pos = oldAffection * pos + (1f - oldAffection) * targetPos;
        oldPos = nowSavedPos;

        if ((targetPos - pos).magnitude > maxDelta)
        {
            pos = targetPos + maxDelta * (pos - targetPos).normalized;
        }

        transform.position = pos;
    }



    // Use this for initialization
    void Start()
    {
        this.oldPos = transform.position;
        this.pos = transform.position;
        this.toAddPos = Vector3.zero;

        copyStrategy = new CopyingStrategy(this);

        Game.Instance.Manager.AddPart(this);

    }


    void getAdderFromNeighbors()
    {
        foreach (var neigh in neighbors)
        {
            Vector3 d = neigh.pos - pos;
            float lNow = d.magnitude;
            toAddPos += 0.5f * (lNow - length) * d.normalized;
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
        copyStrategy.IterateForCreation();
    }

    void DestroySelf()
    {
        if (onMonsterPartDestroyed != null)
            onMonsterPartDestroyed(this);
        Game.Instance.Manager.RemovePart(this);
        DestroyObject(gameObject);
    }
}
