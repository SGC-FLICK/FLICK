using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Song", menuName = "Song")]
public class Song : ScriptableObject
{
    public string MusicName; 
    //주의 : CSV 파일도 이름이 같아야 함
    public AudioClip clip;
    public int bpm;                           //BPM
    public double beat;                       //1박
    public float musicStartDelay;             //첫 음악 시작까지 딜레이
    public float noteStartDelay;              //첫 노트 생성까지 딜레이
}
