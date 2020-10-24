using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class line : MonoBehaviour
{
    private double now = 0;

    //private int state = 3;
    private bool isLongNote = false;

    public bool isOn = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        Note note = other.gameObject.GetComponent<Note>();
        note.isStateChecked = false;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Note note = other.gameObject.GetComponent<Note>();
        if (note.noteType == Note.NoteType.NORMAL)
        {
            if (note.isStateChecked == false)
            {
                ComboText.Instance.GetMiss();
                Judge(note, Note.ScoreType.MISS);
            }
        }

        if (note.noteType == Note.NoteType.LONG)
        {
            if (note.isStateChecked == false)
            {
                ComboText.Instance.GetMiss();
                isLongNote = false;    // LongNote 탈출 시 처리
                Judge(note, Note.ScoreType.MISS);
            }
        }
    }

    void ScoreProcess(Note note)
    {
        ComboEffect.Instance.GetJudgement((int)note.scoreType); // 현재 state에 맞는 점수 처리
        if(note.scoreType != Note.ScoreType.BAD) 
        {
            ComboText.Instance.GetNoteExactly();
            //if(!isLongNote && !isOn) note.scoreType = Note.ScoreType.BAD; 
            // LongNote인 경우엔 직전 state 유지 단, 누르고 있는 경우이다.
        }
        else
            ComboText.Instance.GetMiss();
    }

    void OnTriggerStay2D(Collider2D other) 
    {
        Note note = other.gameObject.GetComponent<Note>();

        if (isOn) //라인을 클릭했는데 노트와 충돌 중인 경우 진입 
        {
            var noteTiming = note.willPlayTiming;
            now = HighSpeed.Instance.CurrentTime; // 클릭한 시간

            if (note.isStateChecked == false) // state 판정
            {
                note.isStateChecked = true;
                if (noteTiming - 0.05 <= now && now <= noteTiming + 0.05) // perfect
                {
                    Debug.Log("노트 타이밍 : " + noteTiming + "노래 : " + now);
                    Judge(note, Note.ScoreType.PERFECT);
                }
                else if (noteTiming - 0.1 <= now && now <= noteTiming + 0.1) // good
                {
                    Judge(note, Note.ScoreType.BAD);
                }
                else if (noteTiming - 0.2 <= now && now <= noteTiming + 0.2)    // bad
                {
                    Judge(note, Note.ScoreType.BAD);
                }
            }
        }
    }
    
    void Judge(Note note, Note.ScoreType scoreType)
    {
        if (note == null)
        {
            Debug.LogError("Note is null");
        }

        note.scoreType = scoreType;
        ScoreProcess(note);

        EventManager.instance.Raise(new NoteDecisionEvent(note));

        note.Eliminate();
    }
}