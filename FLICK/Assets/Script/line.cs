using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class line : MonoBehaviour
{
    [SerializeField] public Toggle toggle;
    private Note note;
    private double now = 0;
    private double noteTiming;
    void Start()
    {
        note = GetComponent<Note>();
        noteTiming = note.willPlayTiming;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionStay2D(Collision2D other) { 
        if(other.gameObject.layer == LayerMask.NameToLayer("note")){ // 노트와 라인이 충돌 중에
            if(toggle.isOn) //라인이 클릭되면
             {
                now = HighSpeed.Instance.CurrentTime;
                toggle.isOn = false; 
                Destroy(other.gameObject); //노트 파괴
                //파괴된 시간을 기록해서 now에 저장
                
                if(noteTiming - 0.04  <= now && now <= noteTiming + 0.04) //노트 타이밍 +- 0.04이면 퍼펙트
                {
                    Debug.Log("perfect!");
                } 
                else if(noteTiming - 0.12 <= now && now <= noteTiming + 0.12)
                {
                    Debug.Log("good!");
                }
                else
                {
                    Debug.Log("bad!");
                }
                // queue[0]처리하기
             }
        }
    }

    void OnCollisionExit2D(Collision2D other) //노트가 라인을 빠져나가면
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("note"))
        {
            Debug.Log("miss");
        }
    } 
}
