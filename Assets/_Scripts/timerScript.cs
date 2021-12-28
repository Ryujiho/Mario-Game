using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class timerScript : MonoBehaviour
{
    public Text text_time;
    public bool isStarted;
    public float time;
    private player player;

    private void Start()
    {
        text_time.text = "Game Start (Press Enter)";
        isStarted = false;
        time = 70f;
    }

    private void Update()
    {
        player = GameObject.Find("Player").GetComponent<player>();
        if (Input.GetKeyDown(KeyCode.Return))
        {
           // SceneManager.LoadScene("SampleScene");
            isStarted = true;
            player.transform.position = new Vector3(-35.7f, 0,-21);
            player.isFinish = false;
            player.kill = false;
            player.isDead = false;

            camera camera_s = GameObject.Find("Main Camera").GetComponent<camera>(); 
          // camera_s.came.position = transform.position + new Vector3(0f, 15f, -35f);
           //camera_s.
        }
        if (isStarted)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
                Young_he young_he = GameObject.Find("young_he").GetComponent<Young_he>();
                if (player.isFinish)
                {
                    Debug.Log("Win-text");
                    text_time.text = "Win!" + " Score : " + Mathf.Ceil(time).ToString();

                    young_he.source.Stop();
                    isStarted = false;
                }
                else if (time < 0 || player.isDead)
                {
                    text_time.text = "Game Over. Press Enter to restart.";
                    time = 70.0f;
                    young_he.source.Stop();

                    isStarted = false;
                }
                else
                {
                    text_time.text = "Timer: " + Mathf.Ceil(time).ToString();
                }

            }
        }
    }

}
