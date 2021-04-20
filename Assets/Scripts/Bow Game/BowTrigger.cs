using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowTrigger : MonoBehaviour {

    public BowScore ScoreBoard;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "InteractionObject")
        {
            if (collision.gameObject.name == "RealArrow(Clone)")
            {
                Destroy(collision.gameObject);
                if (this.gameObject.tag == "TakeAwayOne")
                {
                    ScoreBoard.TakeAwayOne();
                    Debug.Log("TakeAwayOne");
                }
                else if (this.gameObject.tag == "PlusTwo")
                {
                    ScoreBoard.PlusTwo();
                    Debug.Log("PlusTwo");
                }
            }
        }
    }
}
