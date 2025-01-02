using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMove : MonoBehaviour
{
    public GameObject headObject;
    public float minHeight = 1.2f;
    public float distance = 1f;

    [Header("Following Speed Setting")]
    public float followSpeed = 1.0f;

    private Vector3 usedPos = Vector3.zero;
    private Quaternion usedRot;

    // Start is called before the first frame update
    void Start()
    {
        usedPos = this.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(headObject != null)
        {
            usedPos = Vector3.ClampMagnitude(headObject.transform.forward * 1000, distance) + headObject.transform.position;
            usedRot = headObject.transform.rotation;

        }
        else
        {
            usedPos.y = minHeight;
        }

        this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, usedPos, Time.deltaTime * followSpeed);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, usedRot, Time.deltaTime * followSpeed);
    }
}
