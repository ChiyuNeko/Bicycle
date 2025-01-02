using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class PlayerSensor : MonoBehaviour
{
    public ObjectGenerate objectGenerate;
    public GameObject Target;
    public List<GameObject> InSight = new List<GameObject>();
    public List<float> Distance = new List<float>();
    public AudioSource SuccessAudio;
    public AudioSource Appear;

    float temp = 0;
    GameObject animals;
    
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
        if(Target)
        {
            if(Target.transform.localScale.x >= 1)
            {
                AnimalGenerate();
                Appear.Play();
                Target.tag = "Untagged";
                InSight.Remove(Target);
                Target = null;
            }
        }
        AnimalGrowUp();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "GeneratedObject")
            SuccessAudio.Play();
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

    public void AnimalGenerate()
    {
        int a = Random.Range(0, objectGenerate.Animals.Count);
        animals = Instantiate(objectGenerate.Animals[a], Target.transform.position + new Vector3(3, 1, 0), Quaternion.Euler(new Vector3(0, 180, 0)));
        animals.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        animals.transform.LookAt(transform.position);
    }
    public void AnimalGrowUp()
    {
        if(animals)
        {
            if(animals.transform.localScale.x < 1)
            {
                animals.transform.localScale += Vector3.one * 0.1f;
            }
        }
    }
}
