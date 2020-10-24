using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboEffect : MonoBehaviour
{
    public static ComboEffect Instance;
    
    private Text fxText;
    private string[] judgement = new[] {"Perfect", "Good", "Bad", "Miss"};
    
    // Start is called before the first frame update
    void Start()
    {
        fxText = GetComponent<Text>();
        Instance = this;
    }

    public void GetJudgement(int judge)
    {
        fxText.text = judgement[judge];
    }
}
