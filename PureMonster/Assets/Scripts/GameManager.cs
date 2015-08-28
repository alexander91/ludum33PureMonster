using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    int count;
    int partsAtAll;

    List<MonsterPart> allParts = new List<MonsterPart>();

    [SerializeField]
    GameObject restartButton;

    [SerializeField]
    GameObject statistic;

    void ShowGameOver()
    {
        restartButton.SetActive(true);
        statistic.GetComponent<UnityEngine.UI.Text>().text = " Lasted for " + Time.timeSinceLevelLoad.ToString() +
            " seconds! Created " + partsAtAll.ToString() + " parts in total!";
        statistic.SetActive(true);
    }

    public void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public IEnumerable<MonsterPart> AllParts {
        get
        {
            return allParts;
        }
    }

    public void AddPart(MonsterPart part)
    {
        partsAtAll++;
        allParts.Add(part);
    }

    public void RemovePart(MonsterPart part)
    {
        allParts.Remove(part);
        if (allParts.Count == 0) ShowGameOver();
    }

    public int NumberOfParts
    {
        get
        {
            return allParts.Count;
        }
    }

    public Vector3 CenterOfMonster
    {
        get
        {
            Vector3 center = Vector3.zero;
            foreach (var part in allParts)
            {
                center += part.transform.position;
            }
            if (NumberOfParts == 0)
            {
                return Vector3.zero;
            }
            else
            {
                return center * (1.0f / NumberOfParts);
            }
            
        }
    }

	// Use this for initialization
	void Start () {
        Game.Instance.Manager = this;
	}

    public void IterateRandomForces()
    {
        count++;
        if (count > 2 * 60) {
            foreach (var part in allParts)
            {
                part.AddAccel(RandomUtils.getRandomVector(10.0f));
            }
            count = 0;
        }
    }

    public void AddRandomForces()
    {
        foreach (var part in allParts)
        {
            part.AddAccel(RandomUtils.getRandomVector(20.0f));
        }
        count = 0;

    }
	
	// Update is called once per frame
	void Update () {


        float allPartV = 0f;
        foreach (var part in allParts)
        {
            allPartV += part.Speed.magnitude;
        }

        if (allPartV == 0) return;
        //if (allPartV < MonsterPart.AverageSpeed * 4) return;

        float normCoef = MonsterPart.AverageSpeed * allParts.Count / allPartV;

        foreach (var part in allParts)
        {
            part.Speed *= normCoef;
        }

        IterateRandomForces();

	}



}
