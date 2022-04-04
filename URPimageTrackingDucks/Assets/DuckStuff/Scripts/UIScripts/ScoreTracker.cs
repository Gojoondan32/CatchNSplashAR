using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreTracker : MonoBehaviour
{
    public static ScoreTracker instance;
    private void Awake()
    {
        instance = this;
        endscreen.SetActive(false);
    }


    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private TextMeshProUGUI finalScore;
    [SerializeField] private TextMeshProUGUI timeLefttxt;
    [SerializeField] private float timeLimit = 60f;
    [SerializeField] private GameObject duckGame;
    [SerializeField] private GameObject endscreen;

    private int score = 0;
    public int Score { get { return score; } set { score = value; } }

    private void Update()
    {
        if(GameStates.instance.TrueGameStates == GameStates._GameStates.Game)
        {
            timeLimit -= Time.deltaTime;

            if (timeLimit <= 0f)
            {
                //Game has finished
                Time.timeScale = 0f;
                GameObject[] ducks = GameObject.FindGameObjectsWithTag("Duck");
                foreach (GameObject duck in ducks)
                {
                    Destroy(duck);
                }
                endscreen.SetActive(true);
            }
            timeLefttxt.text = "Time Left: " + timeLimit.ToString("F0");
            scoreTxt.text = "Score: " + score.ToString();
            finalScore.text = "Final Score: " + score.ToString();
        }

    }
    private void RevealEndScore()
    {
        
        LeanTween.scale(endscreen, new Vector3(1, 1, 1), 1f);
    }
    public void UpdateScore(int scoreValue)
    {
        scoreTxt.text += scoreValue;
        finalScore.text += scoreValue;
    }
}
