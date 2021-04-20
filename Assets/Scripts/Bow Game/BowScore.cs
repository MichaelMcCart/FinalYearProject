using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BowScore : MonoBehaviour {

    [HideInInspector]
    public int StartBowScore = 0;
    public int PlayerBowScore;
    public Text BowScoreText;

    // Use this for initialization
    void Start()
    {
        PlayerBowScore = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        BowScoreText.text = "" + PlayerBowScore;
    }

    public void TakeAwayOne()
    {
        PlayerBowScore = PlayerBowScore - 1;
    }

    public void PlusTwo()
    {
        PlayerBowScore = PlayerBowScore + 2;
    }

    public void ResetScore()
    {
        PlayerBowScore = StartBowScore;
    }
}
