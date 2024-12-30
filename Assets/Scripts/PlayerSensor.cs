using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSensor : MonoBehaviour
{
    public ObjectGenerate objectGenerate;
    public GameObject Target;
    public List<GameObject> InSight = new List<GameObject>();
    public List<float> Distance = new List<float>();

    float temp = 0;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        GetDistance();
        foreach(GameObject i in InSight)
        {
            Vector3.Distance(gameObject.transform.position, i.transform.position);
        }
        if(Target.transform.localScale.x >= 1)
        {
            Target.tag = "Untagged";
            InSight.Remove(Target);
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        float TargeDistance = Vector3.Distance(gameObject.transform.position, other.transform.position);
        if(other.tag == "GeneratedObject")
        {
            if(!InSight.Contains(other.gameObject))
                InSight.Add(other.gameObject);
            if(Distance.Count != 0)               
            {
                if(Distance[Distance.Count - 1] == TargeDistance)
                {
                    Target = other.gameObject;
                }
                objectGenerate.GrowUp(Target);
            }
            
        }    
        
    }
    private void OnTriggerExit(Collider other)
    {
        //float farest = Vector3.Distance(gameObject.transform.position, InSight[InSight.Count - 1].transform.position);
        if(other.tag == "GeneratedObject")
        {
            if(InSight.Contains(other.gameObject))
                InSight.Remove(other.gameObject);
        }    
    }

    public void GetDistance()
    {
        if(Distance.Count > InSight.Count)
        {
            Distance.RemoveAt(0);
        }
        else if(Distance.Count < InSight.Count)
        {
            Distance.Add(temp);
        }
        else
        {
            for(int i = 0; i < InSight.Count; i++)
            {
                Distance[i] = Vector3.Distance(gameObject.transform.position, InSight[i].transform.position);
                Distance.Sort();
            }
        }

    }
}
