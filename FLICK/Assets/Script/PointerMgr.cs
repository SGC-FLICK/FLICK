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
        if (Input.GetMouseButton(0)) //마우스를 누르는 동안
        {
            MousePushing();
        }
        if (Input.GetMouseButtonUp(0)) //마우스를 때면
        {
            MouseUp();
        }
    }

    public void MousePushing()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 30, Color.blue, 3.5f);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector3.zero);
        if (hit)
        {
            if (hit.transform.gameObject.tag == "Tile")
            {
                hit.transform.GetComponent<line>().isOn = true;
            }
        }
    }
    
    public void MouseUp()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.blue, 3.5f);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector3.zero);
        if (hit)
        {
            if (hit.transform.gameObject.tag == "Tile")
            {
                hit.transform.GetComponent<line>().isOn = false;
            }
        }
    }
}
