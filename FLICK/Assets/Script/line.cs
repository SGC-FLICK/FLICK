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

    private int state;
    private bool isNotCheckState = false;
    private bool isCollision = false;
    void Start()
    {
        note = GetComponent<Note>();
        //noteTiming = note.willPlayTiming;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isCollision) //토글은 켜져있는데 충돌상태가 아닌 경우엔 토글을 꺼야함
        {
            toggle.isOn = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        isCollision = true;
        isNotCheckState = true;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        Destroy(other.gameObject); // 노트 파괴
        ComboEffect.Instance.GetJudgement(state);
        ComboText.Instance.GetNoteExactly();
        isCollision = false;
        isNotCheckState= false;
    }
    void OnTriggerStay2D(Collider2D other) 
    {

        if(toggle.isOn) //라인을 클릭했는데 노트와 충돌 중인 경우 진입 
        {
            noteTiming = other.gameObject.GetComponent<Note>().willPlayTiming;
        
            if(other.gameObject.layer == LayerMask.NameToLayer("Note")) // 노트와 라인이 충돌 중엔 프레임 단위로 반복  
            {  
                now = HighSpeed.Instance.CurrentTime; // 클릭한 시간
                if(noteTiming - 0.35 <= now && now <= noteTiming + 0.35) state = 0; // perfect
                else if(noteTiming - 0.7 <= now && now <= noteTiming + 0.7) state = 1; // good
                else if(noteTiming - 1 <= now && now <= noteTiming + 1) state = 2; // bad
            }

            else if(other.gameObject.layer == LayerMask.NameToLayer("LongNote")) // 롱노트와 라인임 충돌 중엔 프레임 단위로 반복
            {
                now = HighSpeed.Instance.CurrentTime; // 클릭한 시간
                if(isNotCheckState) // state 판정
                {
                    if(noteTiming - 0.35 <= now && now <= noteTiming + 0.35) state = 0; // perfect
                    else if(noteTiming - 0.7 <= now && now <= noteTiming + 0.7) state = 1; // good
                    else if(noteTiming - 1 <= now && now <= noteTiming + 1) state = 2; // bad
                    isNotCheckState = false;
                }
                else
                {
                    ComboEffect.Instance.GetJudgement(state);
                    ComboText.Instance.GetNoteExactly();
                }
            }
    
        }
    
    }
    
}