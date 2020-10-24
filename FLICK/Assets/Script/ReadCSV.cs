using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class ReadCSV : MonoBehaviour
{
    public Song currentSong;
    public int bpm;                           //BPM
    public double beat;                       //1박
    public double CountperSec;                //1분 BPM을 채우기 위해 1초에 카운트
    public float musicStartDelay;             //첫 음악 시작까지 딜레이
    public float noteStartDelay;              //첫 노트 생성까지 딜레이

    public Queue<NoteQueue> note = new Queue<NoteQueue>();
    //public Queue note = new Queue();

    void Start()
    {
        Init();
        ReadCSVFile();
    }

    void Init()
    {
        bpm = currentSong.bpm;
        beat = currentSong.beat;
        musicStartDelay = currentSong.musicStartDelay;
        noteStartDelay = currentSong.noteStartDelay;
    }

    void ReadCSVFile()
    {
        var CSVdir = (Application.dataPath + "/CSVFiles/" + currentSong.MusicName + ".csv").Replace("/", "\\");
        //상대경로 지정
        StreamReader strReader = new StreamReader(CSVdir);
        bool endOfFile = false;
        int previousData = -1;
        string currentTitle = "";
        while (!endOfFile)
        {
            string data_String = strReader.ReadLine();
            if (data_String == null)
            {
                endOfFile = true;
                break;
            }
            var data_values = data_String.Split(',');

            if (data_values[2].ToString() == " Tempo")
            {
                string tempoStr;
                float tempo;
                tempoStr = data_values[3].ToString();
                tempo = (float)Convert.ToDouble(tempoStr);
                Debug.Log("TEMPO : " + tempo);
                bpm = Mathf.RoundToInt(60000000 / tempo);
                //Debug.Log("BPM : " + bpm);
            }

            if (data_values[2].ToString() == " Title_t")
            {
                currentTitle = data_values[3].ToString();
            }

            if (data_values[2] == " Note_on_c" && data_values[5] != " 0")
            {
                int data_values_toInt;
                data_values_toInt = Convert.ToInt32(data_values[1]);

                if (previousData == -1) //밤새우면서 짠 코드라 나중에 수정...필요..
                {
                    previousData = data_values_toInt;
                    continue;
                }

                if (data_values_toInt - previousData >= beat * 2) //현재 음표가 2분음표 보다 길면
                {
                     note.Enqueue(CreateNoteData(previousData, data_values_toInt, Note.NoteType.LONG));
                }
                else
                {
                     note.Enqueue(CreateNoteData(previousData, 0, Note.NoteType.NORMAL));
                }

                previousData = data_values_toInt;
            }


            //if (currentTitle == " \"track_1\"" && data_values[2] == " Note_on_c")
            //{
            //    int data_values_toInt;
            //    data_values_toInt = Convert.ToInt32(data_values[1]);
            //    note[0].Enqueue(CreateNoteData(data_values_toInt, 1));
            //    Debug.Log("노트1 생성");
            //}
        }
        CountperSec = bpm * beat / 60;
    }

    NoteQueue CreateNoteData(int startTime, int endTime, Note.NoteType noteType)
    {
        NoteQueue notedata;
        notedata = new NoteQueue(startTime, endTime, noteType);
        return notedata;
    }

    public struct NoteQueue
    {
        public int startTime;
        public int endTime;
        public Note.NoteType noteType;

        public NoteQueue(int _startTime, int _endTime, Note.NoteType _noteType)
        {
            startTime = _startTime;
            endTime = _endTime;
            noteType = _noteType;
        }
    }

}
