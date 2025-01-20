using System.Collections;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UIM;

public class GameController : SaveSystem
{
    public UIM.UIManager uim = new UIM.UIManager(); 
    public HandsParameters handsParameters;
    public GameObject InformationUI;
    public Collider PlayerSensorCollider;
    public static float startTime;
    public static float playTime = 60;
    public static int amount = 0;
    public static SaveData data = new SaveData();
    public GameData perGame = new GameData();
    [SerializeField] TMP_Text scoreUI;
    [SerializeField] TMP_InputField UID_InputField;
    [SerializeField] TMP_InputField UID_Creat_InputField;
    [SerializeField] GameObject keyBoard;
    [SerializeField] Collider con1;
    [SerializeField] Collider con2;
    [SerializeField] Collider con3;
    [SerializeField] Collider con4;
    [SerializeField] Collider con5;
    [SerializeField] GameObject Login;
    [SerializeField] GameObject Adjust;
    [SerializeField] GameObject Create;
    [SerializeField] GameObject Level;
    [SerializeField] GameObject Gaming;
    [SerializeField] GameObject End;
    [SerializeField] Image Choose;
    [SerializeField] GameObject adjustCon;
    [SerializeField] GameObject enterError;
    [SerializeField] GameObject createError;
    [SerializeField] GameObject scoreBreakUI;
    [SerializeField] GameObject distanceBreakUI;
    [SerializeField] GameObject Net;
    [SerializeField] GameObject music;
    [SerializeField] GameObject HMD;
    [SerializeField] GameObject LCon;
    [SerializeField] GameObject RCon;
    int difficult;
    bool Adjusting = false;
    //bool colliderUp = false;
    //bool colliderDown = false;
    bool upAdjustDone = false;
    bool downAdjustDone = false;
    Vector3 upAdjustPos;
    Vector3 downAdjustPos;
    Vector3 checkPos;
    float adjustTime;
    bool scoreBreak = false;
    bool distanceBreak = false;
    [SerializeField]Image upCheckImage;
    [SerializeField]Image downCheckImage;
    float upCheck = 0;
    float downCheck = 0;
    string hitSide;
    Vector3 Left;

    //���b���l�W

    public class SaveData
    {
        //��J�n�x�s���ܼ�
        //�O�o�x�s�e�n��s�o�̪��ƾ�
        public float score;
        public float moveDis;
       
    }
    [System.Serializable]
    public class GameData
    {
        public string ID;
        public float score;
        public float moveDis;
        public List<string> position = new List<string>();
    }
    [System.Serializable]
    public class Q3Pos
    {
        public Vector3 HMD;
        public Vector3 RController;
        public Vector3 LController;
    }
    // IEnumerator Q3PosRecord()
    // {
    //     if (ButterflyBorn.setPos)
    //     {
    //         data.moveDis += Vector3.Distance(LCon.transform.position, Left);
    //         Left = LCon.transform.position;

    //         Q3Pos record = new Q3Pos();
    //         record.HMD = HMD.transform.position;
    //         record.RController = RCon.transform.position;
    //         record.LController = LCon.transform.position;
    //         string recording = string.Format("HMD:{0},RCon:{1},LCon:{2}", record.HMD.ToString(), record.RController.ToString(), record.LController.ToString());
    //         perGame.position.Add(recording);
    //         yield return new WaitForSeconds(0.05f);
    //         StartCoroutine(Q3PosRecord());
    //     }
    // }

    public class MoveData
    {
        Vector3 pos;
        public float time;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        music.GetComponent<AudioSource>().Play();
       // Left = LCon.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        // if (Adjusting)
        // {
        //     CheckAdjust();
        // }
        // else
        // {
        //     adjustTime = Time.time;
        // }
        // if (Gaming.activeSelf)
        // {
        //     if(Time.time - startTime > playTime)
        //     {

        //         Gaming.SetActive(false);
        //         End.SetActive(true);
        //         EndGame();
        //     }
        // }
        // upCheckImage.fillAmount = upCheck;
        // downCheckImage.fillAmount = downCheck;
        // Debug.Log(GameController.amount);
        
    }
    void SetUI()
    {
        scoreUI.SetText(data.score.ToString());
    }
    private void OnCollisionEnter(Collision collision)
    {
        //foreach (ContactPoint contact in collision.contacts)
        //{
        //    // �N�@�ɪk�u�ഫ�즹���󪺥��a�y��
        //    Vector3 localNormal = transform.InverseTransformDirection(contact.normal);

        //    // �p��P�U�b�V�������
        //    float forwardDot = Vector3.Dot(localNormal, Vector3.forward);
        //    float backDot = Vector3.Dot(localNormal, Vector3.back);
        //    float rightDot = Vector3.Dot(localNormal, Vector3.right);
        //    float leftDot = Vector3.Dot(localNormal, Vector3.left);
        //    float upDot = Vector3.Dot(localNormal, Vector3.up);
        //    float downDot = Vector3.Dot(localNormal, Vector3.down);

        //    // �b�o�̿�ܭ���dot�ȳ̤j
        //    // �ϥ�Mathf.Abs�ӧP�_���Ӥ�V�̩���
        //    float maxDot = Mathf.Max(Mathf.Abs(forwardDot), Mathf.Abs(backDot), Mathf.Abs(rightDot), Mathf.Abs(leftDot), Mathf.Abs(upDot), Mathf.Abs(downDot));

        //    hitSide = "Unknown";
        //    if (Mathf.Abs(forwardDot) == maxDot) hitSide = (forwardDot > 0) ? "Front" : "Back";
        //    else if (Mathf.Abs(rightDot) == maxDot) hitSide = (rightDot > 0) ? "Right" : "Left";
        //    else if (Mathf.Abs(upDot) == maxDot) hitSide = (upDot > 0) ? "Top" : "Bottom";
        //}
        //Debug.Log("Hit Side: " + hitSide);

        // if (collision != null)
        // {
        //     if(collision.gameObject.tag == "Butterfly")
        //     {
        //         if(collision.gameObject.GetComponent<ButterflyController>().canHit == true)
        //         {
        //             CatchButterfly(collision);
        //             music.GetComponent<AudioSource>().CatchPlay();
        //             amount -= 1;
        //         }   
        //     }
        // }
    }


    void CatchButterfly(Collision collision)
    {
        data.score++;
        Destroy(collision.gameObject);
    }

    //void CheckEndGame()
    //{
    //    if(Time.time - startTime > playTime)
    //    {
    //        EndGame();
    //    }
    //}

    // void EndGame()
    // {
    //     savePerGameData();
    //     Net.GetComponent<MeshRenderer>().enabled = false;
    //     if (data.score > CheckID.bestScore)
    //     {
    //         scoreBreak = true;
    //     }
    //     if (data.moveDis > CheckID.bestDis)
    //     {
    //         distanceBreak = true;
    //     }
    //     BreakRecord();
    //     GameObject[] Butterfly = GameObject.FindGameObjectsWithTag("Butterfly");
    //     ButterflyBorn.setPos = false;
    //     for(int i = 0;i < Butterfly.Length; i++)
    //     {
    //         Destroy(Butterfly[i]);
    //     }
    //     amount = 0;
    // }
    void savePerGameData()
    {
        perGame.ID = CheckID.playerID;
        perGame.score = data.score;
        perGame.moveDis = data.moveDis;
        GameData savedata = perGame;
        string fileName = string.Format("{0}{1}_{2}{3}{4}_PosData", DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        Save(perGame, fileName);
        perGame.position.Clear();
    }
    //�p�G���}�̰�����
    // void BreakRecord()
    // {
    //     End.GetComponent<SetEnd_UI>().SetUI();
    //     if (scoreBreak)
    //     {
    //         scoreBreakUI.SetActive(true);
    //         CheckID.bestScore = data.score;
    //     }
    //     else
    //     {
    //         scoreBreakUI.SetActive(false);
    //         data.score = CheckID.bestScore;
    //     }
    //     if (distanceBreak)
    //     {
    //         distanceBreakUI.SetActive(true);
    //         CheckID.bestDis = data.moveDis;
    //     }
    //     else
    //     {
    //         distanceBreakUI.SetActive(false);
    //         data.moveDis = CheckID.bestDis;
    //     }
    //     string fileName = string.Format("{0}_GameData", CheckID.playerID);
    //     Save(data, fileName);
    // }

    public void SetKeyboard()
    {
        con1.enabled = false;
        con2.enabled = false;
        con3.enabled = false;
        con4.enabled = false;
        con5.enabled = false;
        keyBoard.SetActive(true);
    }
    public void KeyboardCon()
    {
        music.GetComponent<AudioSource>().Play();
        con1.enabled = true;
        con2.enabled = true;
        con3.enabled = true;
        con4.enabled = true;
        con5.enabled = true;
        keyBoard.SetActive(false);
    }
    public void NumPadEnter(string num)
    {
        music.GetComponent<AudioSource>().Play();
        CheckID.playerID = CheckID.playerID + num;
        UID_InputField.text = CheckID.playerID;
        UID_Creat_InputField.text = CheckID.playerID;
    }
    
    public void ButBack()
    {
        music.GetComponent<AudioSource>().Play();
        if(CheckID.playerID.Length > 0)
            CheckID.playerID = CheckID.playerID.Substring(0, CheckID.playerID.Length -1);
        UID_InputField.text = CheckID.playerID;
        UID_Creat_InputField.text = CheckID.playerID;
    }
    public void LoginCon()
    {
        music.GetComponent<AudioSource>().Play();
        if (CheckID.checkID())
        {
            Login.SetActive(false);
            uim.Initialized();
            PlayerSensorCollider.enabled = true;
            InformationUI.SetActive(true);
            handsParameters.enabled = true;
            //Adjust.SetActive(true);
            //Adjusting = true;
        }
        else
        {
            StartCoroutine(LoginError());
        }
    }
    public void GoToCreateCon()
    {
        music.GetComponent<AudioSource>().Play();
        Login.SetActive(false);
        Create.SetActive(true);
    }
    public void CreateCon()
    {
        music.GetComponent<AudioSource>().Play();
        if (CheckID.checkID())
        {
            StartCoroutine(CreatingError());
        }
        else
        {
            string fileName = string.Format("{0}_GameData", CheckID.playerID);
            Save(data, fileName);
            //Create.SetActive(false);
            Login.SetActive(false);
            uim.Initialized();
            PlayerSensorCollider.enabled = true;
            InformationUI.SetActive(true);
            handsParameters.enabled = true;
            //Adjust.SetActive(true);
            //Adjusting = true;
        }
    }
    public void AdjustCon()
    {
        music.GetComponent<AudioSource>().Play();
        adjustCon.SetActive(false);
        upCheck = 0;
        upCheckImage.fillAmount = upCheck;
        downCheck = 0;
        downCheckImage.fillAmount = downCheck;
        upAdjustDone = false;
        downAdjustDone = false;
        Adjust.SetActive(false);
        Adjusting = false;
        Level.SetActive(true);
    }
    public void Level1()
    {
        music.GetComponent<AudioSource>().Play();
        difficult = 1;
        Choose.transform.localPosition = new Vector3(-61f, Choose.transform.localPosition.y, Choose.transform.localPosition.z);
    }
    public void Level2()
    {
        music.GetComponent<AudioSource>().Play();
        difficult = 2;
        Choose.transform.localPosition = new Vector3(0.27f, Choose.transform.localPosition.y, Choose.transform.localPosition.z);
    }
    public void Level3()
    {
        music.GetComponent<AudioSource>().Play();
        difficult = 3;
        Choose.transform.localPosition = new Vector3(60.6f, Choose.transform.localPosition.y, Choose.transform.localPosition.z);
    }
    // public void EnterGame()
    // {
    //     Left = LCon.transform.position;
    //     music.GetComponent<AudioSource>().Play();
    //     ButterflyBorn.setPos = true;
    //     ButterflyBorn.bornDirection = Camera.main.transform.forward;
    //     ButterflyBorn.bornDirection = Vector3.ClampMagnitude(ButterflyBorn.bornDirection * 1000, ButterflyBorn.bornDistance);
    //     Level.SetActive(false);
    //     Gaming.SetActive(true);
    //     StartCoroutine(CheckBornDirection());
    //     StartCoroutine(Q3PosRecord());
    //     startTime = Time.time;
    //     Net.GetComponent<MeshRenderer>().enabled = true;
    // }
    // IEnumerator CheckBornDirection()
    // {
    //     yield return new WaitForSeconds(0.1f);
    //     if (Gaming.activeSelf)
    //     {
    //         ButterflyBorn.bornDirection = Camera.main.transform.forward;
    //         ButterflyBorn.bornDirection = Vector3.ClampMagnitude(ButterflyBorn.bornDirection * 1000, ButterflyBorn.bornDistance);
    //         StartCoroutine(CheckBornDirection());
    //     }
    // }
    public void CheckAdjust()
    {
        //if (colliderUp)
        //{
        //    if (!upAdjustDone)
        //    {
                
        //        if (Vector3.Distance(checkPos, this.transform.position) > 0.05f)
        //        {
        //            adjustTime = Time.time;
        //            upCheck = 0;
        //            upCheckImage.fillAmount = upCheck;
        //            checkPos = this.transform.position;
        //        }
        //        else if (Time.time - adjustTime > 2f)
        //        {
        //            upAdjustPos = this.transform.position;
        //            upCheck = 1;
        //            upCheckImage.fillAmount = upCheck;
        //            upAdjustDone = true;
        //        }
        //        else
        //        {
        //            upCheck = (Time.time - adjustTime)*0.5f;
        //            upCheckImage.fillAmount = upCheck;
        //        }
        //    }
        //}
        //else if (colliderDown)
        //{
        //    if (!downAdjustDone)
        //    {
        //        if (Vector3.Distance(checkPos, this.transform.position) > 0.05f)
        //        {
        //            adjustTime = Time.time;
        //            downCheck = 0;
        //            downCheckImage.fillAmount = downCheck;
        //            checkPos = this.transform.position;
        //        }
        //        else if (Time.time - adjustTime > 2f)
        //        {
        //            downAdjustPos = this.transform.position;
        //            downCheck = 1;
        //            downCheckImage.fillAmount = downCheck;
        //            downAdjustDone = true;
        //        }
        //        else
        //        {
        //            downCheck = (Time.time - adjustTime)*0.5f;
        //            downCheckImage.fillAmount = downCheck;
        //        }
        //    }
        //}

        // if(upAdjustDone && downAdjustDone)
        // {
        //     ButterflyBorn.bornDistance = (Vector3.Distance(Camera.main.transform.position, upAdjustPos) + Vector3.Distance(Camera.main.transform.position, downAdjustPos)) / 2f;
        //     adjustCon.SetActive(true);
        // }
    }

    // public void UpAdjust()
    // {
    //     upAdjustPos = this.transform.position;
    //     upAdjustDone = true;
    //     music.GetComponent<AudioSource>().AdjustPlay();
    //     //�ե�����UI
    //     upCheck = 1;
    //     upCheckImage.fillAmount = upCheck;
    // }
    // public void DownAdjust()
    // {
    //     downAdjustPos = this.transform.position;
    //     downAdjustDone = true;
    //     music.GetComponent<AudioSource>().AdjustPlay();
    //     //�ե�����UI
    //     downCheck = 1;
    //     downCheckImage.fillAmount = downCheck;
    // }
    void OnCollisionStay(Collision collider)
    {
        //if (collider.gameObject.tag == "upAdjust")
        //{
        //    colliderUp = true;
        //    colliderDown = false;
        //}
        //else if(collider.gameObject.tag == "downAdjust")
        //{
        //    colliderDown = true;
        //    colliderUp = false;
        //}
        //else
        //{
        //    colliderUp = false;
        //    colliderDown = false;
        //}
    }
    IEnumerator LoginError()
    {
        enterError.SetActive(true);
        yield return new WaitForSeconds(2f);
        enterError.SetActive(false);
    }
    IEnumerator CreatingError()
    {
        createError.SetActive(true);
        yield return new WaitForSeconds(2f);
        createError.SetActive(false);
    }
    public void playAgainCon()
    {
        music.GetComponent<AudioSource>().Play();
        data.score = 0;
        data.moveDis = 0;
        scoreBreak = false;
        distanceBreak = false;
        Adjusting = true;
        End.SetActive(false);
        Adjust.SetActive(true);
    }
}
