using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject Player;
    public HandsParameters handsParameters;
    public float velocity;
    public float speed;
    public float smooth;
    public bool LeftHand;

    public RaycastHit hit;
    float dis;
    public float hight;

    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Physics.Raycast(Player.transform.position, Player.transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
            dis = hit.distance;
        }
        if(dis < hight)
        {
            Player.transform.Translate(0, 0.01f, 0);
        }
        else if(dis > hight)
        {
            Player.transform.Translate(0, -0.01f, 0);
        }

        if (LeftHand)
        {
            velocity = Mathf.Lerp(velocity, Mathf.Abs(handsParameters.LeftControllerVelocity.y), 1/smooth);
        }
        else
        {
            velocity = Mathf.Lerp(velocity, Mathf.Abs(handsParameters.RightControllerVelocity.y), 1/smooth);
        }
        Player.transform.Translate(0, 0, velocity*speed, Space.Self);
    }
}
