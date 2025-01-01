using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerate : MonoBehaviour
{
    public HandsParameters handsParameters;
    public List<GameObject> ObjectsPrefabs = new List<GameObject>();
    public List<GameObject> Animals = new List<GameObject>();
    public List<GameObject> AllObjects = new List<GameObject>();
    public Vector2 GenerateZone;
    public Vector2 Distence;
    public float Scale;
    public float RandomOffset;
    public float GrowUpSpeedOffset = 1;
    [SerializeField]
    float GrowUpSpeed;
    public bool LeftHand;
    int index = 0;

    void Start()
    {
        GenerateObjects();
    }

    void Update()
    {
        GetVelocity();
        //GrowUp();
    }

    public void GenerateObjects()
    {
        Vector3 GeneratePoint =  gameObject.transform.position;
        Vector3 RamdomPoint;
        for (int i = 0; i < GenerateZone.y; i++)
        {
            for(int j = 0; j < GenerateZone.x; j++)
            {
                RamdomPoint = new Vector3( Random.Range(-RandomOffset, RandomOffset), 0, Random.Range(-RandomOffset, RandomOffset));
                GameObject gameObject = Instantiate(ObjectsPrefabs[Random.Range(0, ObjectsPrefabs.Count)], GeneratePoint + RamdomPoint, Quaternion.identity);
                gameObject.transform.localScale = Vector3.one * Scale;
                AllObjects.Add(gameObject);
                GeneratePoint.x += Distence.x;
            }
            GeneratePoint.z += Distence.y;
            GeneratePoint.x = gameObject.transform.position.x;
        }
    }
    public void GrowUp(GameObject target)
    {
        
        if(target)
        {
            if(target.transform.localScale.x <= 1)
            {
                target.transform.localScale += Vector3.one * GrowUpSpeedOffset * GrowUpSpeed / 10f;
            }
            // else
            // {
            //     index = Random.Range(0, AllObjects.Count);
            // }
        }

    }

    public float GetVelocity()
    {
        if (LeftHand)
        {
            GrowUpSpeed = Mathf.Lerp(GrowUpSpeed, Mathf.Abs(handsParameters.LeftControllerVelocity.y), 1/GrowUpSpeed);
            return handsParameters.RightControllerVelocity.y;
        }
        else
        {
            GrowUpSpeed = Mathf.Lerp(GrowUpSpeed, Mathf.Abs(handsParameters.RightControllerVelocity.y), 1/GrowUpSpeed);
            return handsParameters.RightControllerVelocity.y;
        }
    }
}
