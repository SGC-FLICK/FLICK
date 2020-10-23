using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGenerator : MonoBehaviour
{
    public ReadCSV csv;
    public GameObject notePrefab;
    public GameObject spawnedPrefab;
    public GameObject[] spawnPoint;

    public float musicStartDelay;             //첫 음악 시작까지 딜레이
    public float noteStartDelay;              //첫 노트 생성까지 딜레이
    public float musicPlayedTime = 0.00f;     //노래 플레이 후 경과 시간
    public float arriveSpentTime = 2.0f;      //노트가 지점까지 가는데에 걸리는 시간
    float tick = 0.03f;
    double count;                             //디버깅 편의상, 나중엔 private로 바꾸기

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
        musicPlayedTime += tick;
        if (count > (int)csv.note.Peek())
        {
            GenerateNote(musicPlayedTime);
        }
    }

    void GenerateNote(float musicPlayedTime)
    {
        int rand;
        rand = Random.Range(0,6);
        spawnedPrefab = Instantiate(notePrefab, spawnPoint[rand].transform);
        spawnedPrefab.GetComponent<Note>().spawnedTiming = musicPlayedTime;
        spawnedPrefab.GetComponent<Note>().willPlayTiming = musicPlayedTime + arriveSpentTime;
        //spawnedTiming, willPlayTiming은 note 프리팹에 저장되는 변수
        //spawnedTiming은 음악 몇초 째에 생성되었는지, willPlayTiming은 몇초 째에 연주되어야 하는지를 의미
        //arriveSpentTime은 생성 후 라인까지 걸리는 시간, 
        //그러므로 willPlayTiming = spawnedTiming + arriveSpentTime, 


        //spawnedPrefab.GetComponent<Note>().priority = spawnPoint[rand].transform.childCount;

        //Debug.Log(csv.note.Peek());
        csv.note.Dequeue();
    }
}
