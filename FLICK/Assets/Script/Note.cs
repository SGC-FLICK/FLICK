using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public float speed = 3.0f;      //내려오는 속도
    public int trackNum;            //몇 번 트랙에 놓여있는지
    public bool isStateChecked;     //판정 받았는지
    public int priority;            //부모 객체로부터 몇 번째 노트인가

    public double spawnedTiming;    //스폰된 타이밍
    public double willPlayTiming;   //연주될 타이밍

    float needArriveTime;           //라인까지 걸리는 시간

    public enum NoteType
    {
        NORMAL,
        LONG,
        BOUNCE
    }

    public enum ScoreType
    {
        PERFECT,
        GOOD,
        BAD,
        MISS
    }

    public NoteType noteType;
    public ScoreType scoreType;

    void Start()
    {
        InvokeRepeating("Timer", 0, 0.03f);
    }
    
    void Update()
    {
        //var position = transform.position;
        //position.y = ((float)willPlayTiming - HighSpeed.Instance.CurrentTime) * HighSpeed.Instance.MoveSpeed;
        //transform.position = position;
        var yMove = HighSpeed.Instance.MoveSpeed * Time.deltaTime;
        transform.Translate(new Vector3(0.0f, -yMove, 0.0f));
    }

    void Timer() //생성부터 라인까지 걸리는 시간
    {
        needArriveTime += 0.03f;
        if (gameObject.transform.position.y < -3.0f) //3.0f는 Line 오브젝트가 있는 y좌표
        {
            Debug.Log("생성부터 소멸까지 " + needArriveTime + "초 걸림");
            CancelInvoke("Timer");
            //Eliminate();
        }
    }

    public void CheckScore()
    {

    }

    public void Eliminate()
    {
        Destroy(gameObject);
    }
}