using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinalScoreScript : MonoBehaviour
{
    public TextMeshProUGUI TimeScoreText;
    public TextMeshProUGUI AccScoreText;
    public TextMeshProUGUI HealthScoreText;
    public TextMeshProUGUI TotalScoreText;
    public float totalDuration;
    float currentTime;
    float splitTime;
    int maxScore;
    int timeScore;
    int accScore;
    int healthScore;
    bool activated;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
        maxScore = 10000;
        splitTime = totalDuration / 4;

        timeScore = Mathf.FloorToInt(
            (SceneManagerScript.GM.remainingTime / 
            SceneManagerScript.GM.levelStartTime) * 
            maxScore
        );
        accScore = Mathf.FloorToInt(
            ((float)SceneManagerScript.GM.hittedShots / 
            (float)(SceneManagerScript.GM.hittedShots + SceneManagerScript.GM.missedShots)) * 
            maxScore
        );
        healthScore = Mathf.FloorToInt(
            ((float)SceneManagerScript.GM.playerStatus.hp /
            100) *
            maxScore
        );

        activated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            // Update scores over time with animation
            if (currentTime < splitTime)
            {
                TimeScoreText.enabled = true;
                TimeScoreText.text = Random.Range(0, maxScore).ToString();
            }
            else if (currentTime < splitTime * 2)
            {
                TimeScoreText.text = timeScore.ToString();

                AccScoreText.enabled = true;
                AccScoreText.text = Random.Range(0, maxScore).ToString();            
            }
            else if (currentTime < splitTime * 3)
            {
                AccScoreText.text = accScore.ToString();

                HealthScoreText.enabled = true;
                HealthScoreText.text = Random.Range(0, maxScore).ToString();      
            }
            else if (currentTime < splitTime * 4f)
            {
                HealthScoreText.text = healthScore.ToString();

                TotalScoreText.enabled = true;
                TotalScoreText.text = Random.Range(0, maxScore).ToString();      
            }
            else
            {
                TotalScoreText.text = (timeScore + accScore + healthScore).ToString();
                if (Input.GetKeyDown(KeyCode.Space)) 
                {
                    SceneManagerScript.GM.RestartLevel();
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentTime += splitTime;
            }
            else
            {
                currentTime += Time.deltaTime;
            }
        }
    }

    public void Activate()
    {
        activated = true;
    }
}
