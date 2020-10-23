using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public float speed = 3.0f;      //내려오는 속도
    public int trackNum;            //몇 번 트랙에 놓여있는지
    public bool isInPerfectZone;    //퍼펙트 판정구역 판별
    public bool isInMissZone;       //퍼펙트 판정구역 판별
    public int priority;                   //부모 객체로부터 몇 번째 노트인가


    void Start()
    {
        StartCoroutine(Move());
    }

    void Update()
    {
    }

    public void Eliminate()
    {
        Destroy(gameObject);
    }

    IEnumerator Move()
    {
        while (true)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            yield return null;
            if (gameObject.transform.position.y < -20f)
            {
                Eliminate();
                yield return null;
            }
        }
    }
}