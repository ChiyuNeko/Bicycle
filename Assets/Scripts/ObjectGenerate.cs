using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerate : MonoBehaviour
{
    public List<GameObject> ObjectsPrefabs = new List<GameObject>();
    public List<GameObject> AllObjects = new List<GameObject>();
    public Vector2 GenerateZone;
    public Vector2 Distence;
    public float RandomOffset;
    public float GrowUpSpeed;
    int index = 0;

    void Start()
    {
        GenerateObjects();
    }

    void Update()
    {
        GrowUp();
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
                gameObject.transform.localScale = Vector3.zero;
                AllObjects.Add(gameObject);
                GeneratePoint.x += Distence.x;
            }
            GeneratePoint.z += Distence.y;
            GeneratePoint.x = gameObject.transform.position.x;
        }
    }
    public void GrowUp()
    {
        

        if(AllObjects[index].transform.localScale.x <= 1)
        {
            AllObjects[index].transform.localScale += Vector3.one * GrowUpSpeed / 100f;
        }
        else
        {
            index = Random.Range(0, AllObjects.Count);
        }

    }
}