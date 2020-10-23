using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pointer : MonoBehaviour
{
    
    public bool isMouseDown = false;
    public bool isDragged = false;
    [SerializeField] public Toggle toggle;
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetMouseButtonDown(0))
        {
            isMouseDown = true;
            isDragged = false;
        }
        if (isDragged)
        {
            isMouseDown = false;
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (isMouseDown)
            {
                MouseClicked();
                isMouseDown = false;
            }
            isDragged = false;
        }
    }

    
    public void MouseClicked()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(
                Camera.main.ScreenPointToRay(Input.mousePosition).origin,
                Camera.main.ScreenPointToRay(Input.mousePosition).direction,
                Mathf.Infinity,
                LayerMask.GetMask("line"));
        // target 클릭 시 hit2D에 담김

         if (hit2D.collider != null) //라인이 클릭되면
         { 
             Debug.Log("line click");

             toggle.isOn = true; // 라인에 노트가 들어왔는 지 검사하는 단계

             // Destroy(hit2D.collider.gameObject); //노트파괴
         }
    }
}
