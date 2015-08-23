using UnityEngine;
using System.Collections;

public class CameraToMonstr : MonoBehaviour {

    Vector3 startMonsterVec;
    Vector3 cameraStartVec;

	// Use this for initialization
	void Start () {
        startMonsterVec = Game.Instance.Manager.CenterOfMonster;
        cameraStartVec = transform.position;
	
	}
	
	// Update is called once per frame
	void Update () {

        var adder = Game.Instance.Manager.CenterOfMonster - startMonsterVec;

        transform.position = cameraStartVec + adder;
	
	}
}
