using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGenerator : MonoBehaviour
{
    public ReadCSV csv;
    public GameObject notePrefab;
    public GameObject spawnedPrefab;
    public GameObject[] spawnPoint;
    public double count; //디버깅 편의상, 나중엔 private로 바꾸기
    public float musicStartDelay; //음악 시작까지 딜레이
    public float noteStartDelay;  //노트 생성까지 딜레이
    float tick = 0.03f;

    void Awake()
    {
        Init();
    }

    void Init()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        for (int i = 0; i < 6; i++)
            spawnPoint[i] = GameObject.Find("SP"+i);
        //주의 : 인스펙터에서 "SP+(숫자)" 이름을 바꾸지 마시오
    }

    void Start()
    {
        Invoke("PlayMusic", musicStartDelay);
        InvokeRepeating("Timer", noteStartDelay, tick);
        //2초 뒤에, 0.03초 간격으로 timer 메서드 호출
        //노래 시작 타이밍을 조절해서 싱크 맞출 수 있음
    }
    //Invoke를 써야 정확하다!
    void FixedUpdate()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Instantiate(notePrefab, spawnPoint.transform);
        //}
    }

    void PlayMusic()
    {
        GetComponent<AudioSource>().Play();
    }

    void Timer()
    {
        count += csv.CountperSec * tick;

        if (count > (int)csv.note.Peek())
        {
            GenerateNote();
        }
    }

    void GenerateNote()
    {
        int rand;
        rand = Random.Range(0,6);
        spawnedPrefab = Instantiate(notePrefab, spawnPoint[rand].transform);
        //spawnedPrefab.GetComponent<Note>().trackNum = rand;
        //spawnedPrefab.GetComponent<Note>().priority = spawnPoint[rand].transform.childCount;

        //Debug.Log(csv.note.Peek());
        csv.note.Dequeue();
        //csv.note[track].Dequeue();
    }
}
