using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;
using Oculus.Interaction.Samples;

namespace UIM
{

    public class UIManager : MonoBehaviour
    {
        public ObjectGenerate objectGenerate;
        public GameController gameController;
        public VelocetyData velocetyData_L;
        public VelocetyData velocetyData_R;
        public float CountdownTime = 0;
        public bool StartCount{get; set;}
        public Text score;
        public Text score_finish;
        public Text speed;
        public Text time;
        public UnityEvent On_Finish;
        public float velocity {get; set;}
        public float distance;
        float StartTime;
        bool triggered = false;
        void Start()
        {
            StartCount = false;
            StartTime = 0;
            Initialized();
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                StartTime = Time.time;
                Initialized();
            }
            if(Input.GetKeyDown(KeyCode.S))
            {
                On_Finish?.Invoke();
                score_finish.text = score.text;
            }
            velocity = Mathf.Abs(velocetyData_L.Velocety + velocetyData_R.Velocety) / 2;
            distance += velocity / 100;
            speed.text = "速度：" + velocity.ToString("F2") + "m/s";
            score.text = "騎行分數：" + distance.ToString("F2");
            if(StartCount)
            {
                CountDown();
                if((CountdownTime - Time.time + StartTime) <= 0 && !triggered)
                {
                    score_finish.text = score.text;
                    On_Finish?.Invoke();
                    triggered = true;
                }
            }

        }

        public int CountDown()
        {
            float a = (CountdownTime - Time.time + StartTime) /60;
            int b;
            float c;
            b = (int)a;
            c =  60 - ((Time.time - StartTime) % 60);
            if(c > 10)
                time.text = $"{b} : {(int)c}";
            else if (c < 10)
                time.text = $"{b} : 0{(int)c}";
            if(c == 60)
                time.text = $"{b} : 00";
            return b;
        }
        public void Initialized()
        {
            StartTime = Time.time;
            distance = 0;

        }
        public void TryAgain()
        {
            string currentSceneName = SceneManager.GetActiveScene().name;

            SceneManager.LoadScene(currentSceneName);
        }
    }
}
