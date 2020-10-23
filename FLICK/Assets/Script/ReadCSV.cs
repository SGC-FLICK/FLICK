using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class ReadCSV : MonoBehaviour
{
    public int bpm;     //BPM
    public double beat; //1박
    //Faded는 96, Titanium은 188
    public double CountperSec; //1분 BPM을 채우기 위해 1초에 카운트
    string CSVdir;
    bool flag = false; //Title_t를 마주쳤는지

    //public Queue <NoteQueue>[] note = new Queue<NoteQueue>[6];
    public Queue note = new Queue();

    void Start()
    {
        //for (int i = 0; i < 6; i++)
        //{
        //    note[i] = new Queue<NoteQueue>();
        //}
        ReadCSVFile();
    }

    void ReadCSVFile()
    {
        CSVdir = (Application.dataPath + "/CSVFiles/Faded.csv").Replace("/", "\\");
        //상대경로 지정
        StreamReader strReader = new StreamReader(CSVdir);
        bool endOfFile = false;
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
            if (data_values[2] == " Note_on_c")
            {
                int data_values_toInt;
                data_values_toInt = Convert.ToInt32(data_values[1]);
                note.Enqueue(data_values_toInt);
            }
            //if (currentTitle == " \"track_1\"" && data_values[2] == " Note_on_c")
            //{
            //    int data_values_toInt;
            //    data_values_toInt = Convert.ToInt32(data_values[1]);
            //    note[0].Enqueue(CreateNoteData(data_values_toInt, 1));
            //    Debug.Log("노트1 생성");
            //}

            //if (currentTitle == " \"track_2\"" && data_values[2] == " Note_on_c")
            //{
            //    int data_values_toInt;
            //    data_values_toInt = Convert.ToInt32(data_values[1]);
            //    note[1].Enqueue(CreateNoteData(data_values_toInt, 2));
            //    Debug.Log("노트2 생성");
            //}

            //if (currentTitle == " \"track_3\"" && data_values[2] == " Note_on_c")
            //{
            //    int data_values_toInt;
            //    data_values_toInt = Convert.ToInt32(data_values[1]);
            //    note[2].Enqueue(CreateNoteData(data_values_toInt, 3));
            //    Debug.Log("노트3 생성");
            //}
        }
        CountperSec = bpm * beat / 60;
    }

    NoteQueue CreateNoteData(int spawnTime, int track)
    {
        NoteQueue notedata;
        notedata = new NoteQueue(spawnTime, track);
        return notedata;
    }

    public struct NoteQueue
    {
        public int spawnTime;
        //몇번째 count 시간에 등장하는지
        public int trackData;
        //몇 번째 트랙인지
        public NoteQueue(int _spawnTime, int _track)
        {
            spawnTime = _spawnTime;
            trackData = _track;
        }
    }
}
