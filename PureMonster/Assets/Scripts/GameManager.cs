using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    List<MonsterPart> allParts = new List<MonsterPart>();

    public void AddPart(MonsterPart part)
    {
        allParts.Add(part);
    }

    public void RemovePart(MonsterPart part)
    {
        allParts.Remove(part);
    }

	// Use this for initialization
	void Start () {
        Game.Instance.Manager = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
