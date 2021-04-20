using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteCoin : MonoBehaviour
{

    public Transform[] BallArray;
    public Rigidbody Ball;
    public SkiBallScore ScoreBoard;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "InteractionObject")
        {
            if (collision.gameObject.name == "Coin" || collision.gameObject.name == "Coin(Clone)")
            {
                Destroy(collision.gameObject);

                for (int BallLoop = 0; BallLoop < BallArray.Length; BallLoop++)
                {
                    Rigidbody Ballz_ = Instantiate(Ball, BallArray[BallLoop].position, BallArray[BallLoop].rotation) as Rigidbody;
                }

                ScoreBoard.ResetScore();

            }
        }
    }
}

    //private void Update()
    //{
    //    if (Score <= 0) { Score = 0; }//Clamps value to stop it going less than 0

    //    if (Score >= 1)//Only allows fuction to happen when it has a value above 0
    //    {
    //        ///do stuff
    //    }
    //}
