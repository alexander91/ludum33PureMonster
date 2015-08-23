using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        float speed = 0.5f;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            foreach (var part in Game.Instance.Manager.AllParts)
            {
                part.AddAccel(speed * Vector3.up);
            }
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            foreach (var part in Game.Instance.Manager.AllParts)
            {
                part.AddAccel( - speed * Vector3.up);
            }
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            foreach (var part in Game.Instance.Manager.AllParts)
            {
                part.AddAccel(speed * Vector3.right);
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            foreach (var part in Game.Instance.Manager.AllParts)
            {
                part.AddAccel(speed * Vector3.left);
            }
        }
	
	}
}
