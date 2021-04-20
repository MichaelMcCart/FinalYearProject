using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPouchParent : MonoBehaviour {

    public GameObject rig;

	// Update is called once per frame
	void Update ()
    {
        transform.position = rig.transform.position - new Vector3(0, 0.5f, 0);
	}
}
