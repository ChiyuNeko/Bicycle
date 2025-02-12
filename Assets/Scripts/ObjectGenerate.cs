using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIM;
using Unity.VisualScripting;

public class ObjectGenerate : MonoBehaviour
{
    public PlayerSensor playerSensor;
    public UIManager uim = new UIManager();
    public VelocetyData velocetyData_L;
    public VelocetyData velocetyData_R;
    public HandsParameters handsParameters;
    public List<GameObject> ObjectsPrefabs = new List<GameObject>();
    public List<GameObject> Animals = new List<GameObject>();
    public List<GameObject> AllObjects = new List<GameObject>();
    public float TargetScore;
    public float ShrinkSpeed;
    public GameObject NextStep;
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
        if(playerSensor)
            playerSensor.objectGenerate = this;
    }

    void Update()
    {
        GetVelocity();

        if(uim.distance >= TargetScore)
        {
            Shrink();
        }
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
                GameObject gameObject = Instantiate(ObjectsPrefabs[Random.Range(0, ObjectsPrefabs.Count)], GeneratePoint + RamdomPoint, Quaternion.identity, this.transform);
                gameObject.transform.localScale = Vector3.one * Scale;
                AllObjects.Add(gameObject);
                GeneratePoint.x += Distence.x;

                RaycastHit hit;
                if (Physics.Raycast(gameObject.transform.position, gameObject.transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
                {
                    if(hit.transform.tag == "Ground")
                        gameObject.transform.position = hit.point;
                }

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
        GrowUpSpeed = Mathf.Lerp(GrowUpSpeed, Mathf.Abs((velocetyData_L.Velocety + velocetyData_R.Velocety)/2), 0.1f);
        if (LeftHand)
        {
            return velocetyData_L.Velocety;
        }
        else
        {
            return velocetyData_R.Velocety;
        }
    }

    public void Shrink()
    {
        int UnShrinked = 0;
        foreach(GameObject i in AllObjects)
        {
            if(i.transform.localScale.x >= 0.005)
            {
                i.transform.localScale -= Vector3.one * ShrinkSpeed * Time.deltaTime;
                UnShrinked++;
            }
            else
            {
                playerSensor.InSight.Remove(i);
            }

        }
        if(UnShrinked == 0)
        {
            NextStep?.SetActive(true);
            this.gameObject.SetActive(false);

        }
       
    }
}
