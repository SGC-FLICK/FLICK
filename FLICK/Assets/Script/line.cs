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
        //noteTiming = note.willPlayTiming;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionStay2D(Collision2D other) { 
        if(other.gameObject.layer == LayerMask.NameToLayer("Note")){ // 노트와 라인이 충돌 중엔 프레임 단위로 반복

            noteTiming = other.collider.gameObject.GetComponent<Note>().willPlayTiming;
            //
            
            if(toggle.isOn) //라인이 클릭되어있는 상태에서 충돌 중인 경우 
             {
                now = HighSpeed.Instance.CurrentTime; //파괴된 시간을 기록해서 now에 저장
                toggle.isOn = false; 
                Destroy(other.gameObject); // 노트 파괴
                
                
                if(noteTiming - 0.35 <= now && now <= noteTiming + 0.35) //노트 타이밍 +- 0.04이면 퍼펙트
                {
                    Debug.Log("perfect!");
                } 
                else if(noteTiming - 0.6 <= now && now <= noteTiming + 0.6)
                {
                    Debug.Log("good!");
                }
                else //bad만 나옴 생각보다 now와 noteTiming의 차이가 큼
                {
                    Debug.Log(noteTiming);
                    Debug.Log(now);
                    Debug.Log("bad!");
                }
                // queue[0]처리하기
             }
        }
    }

    void OnCollisionExit2D(Collision2D other) //노트가 라인을 빠져나가면
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Note"))
        {
            Debug.Log("miss");
        }
    } 
}
