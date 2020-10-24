using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PointerMgr : MonoBehaviour
{
   
    public bool isMouseDown = false;
    public bool isDragged = false;
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MouseStateCheck();
    }
    public void MouseStateCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMouseDown = true;
            isDragged = false;
        }
        if (isDragged) isMouseDown = false;

        if (Input.GetMouseButtonUp(0)) // 마우스를 뗀 경우
        {
            if (isMouseDown)
            {
                MouseClicked();
                isMouseDown = false;
            }
            isDragged = false;
        }
        else // 마우스를 누르고 있는 경우
        {
            if(isMouseDown)
            {
                MousePushing();
            }
        }
    }

    public void MousePushing()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(
                Camera.main.ScreenPointToRay(Input.mousePosition).origin,
                Camera.main.ScreenPointToRay(Input.mousePosition).direction,
                Mathf.Infinity,
                LayerMask.GetMask("Line"));

        if (hit2D.collider != null) //라인이 클릭되면
         { 
             hit2D.collider.gameObject.GetComponent<Toggle>().isOn = true; // 라인에 노트가 들어왔는 지 검사하는 단계로 진입
         }
    }
    
    public void MouseClicked()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(
                Camera.main.ScreenPointToRay(Input.mousePosition).origin,
                Camera.main.ScreenPointToRay(Input.mousePosition).direction,
                Mathf.Infinity,
                LayerMask.GetMask("Line"));
        // target 클릭 시 hit2D에 담김
         if (hit2D.collider != null) 
         { 
             hit2D.collider.gameObject.GetComponent<Toggle>().isOn = false;
         }
    }
}
