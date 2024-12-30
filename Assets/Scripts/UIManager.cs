using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public ObjectGenerate objectGenerate;
    public Text score;
    public Text speed;
    public Text time;
    float velocity;
    float distance;
    float StartTime;
    void Start()
    {
        StartTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            StartTime = Time.time;
        }
        velocity = Mathf.Abs(objectGenerate.GetVelocity());
        distance += velocity / 100;
        speed.text = "速度：" + velocity.ToString("F2");
        score.text = "騎行距離：" + distance.ToString("F2");
        CountDown();
    }

    public int CountDown()
    {
        float a = (1200 - Time.time + StartTime) /60;
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
}
