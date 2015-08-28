using UnityEngine;
using System.Collections;

public class CameraToMonstr : MonoBehaviour {

    [SerializeField]
    float attr = 0.07f;

    Vector3 startMonsterVec;
    Vector3 cameraStartVec;
    Vector3 lastAdder;

	// Use this for initialization
	void Start () {
        startMonsterVec = Game.Instance.Manager.CenterOfMonster;
        cameraStartVec = transform.position;
	
	}
	
	// Update is called once per frame
	void Update () {

        var adder = Game.Instance.Manager.CenterOfMonster - startMonsterVec;
        var nowAdder = Vector3.Lerp(lastAdder, adder, attr);

        transform.position = cameraStartVec + nowAdder;

        lastAdder = nowAdder;
	
	}
}
