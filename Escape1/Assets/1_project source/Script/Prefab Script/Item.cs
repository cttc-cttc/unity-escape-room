using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    Texture2D mouse;
    Image border;
    Image ZoomIcon;
    bool isClicked;
    CameraControl cameraControl;
    Inventory inventory;
    string itemType;

    void Awake()
    {
        mouse = Resources.Load<Texture2D>("pointer");
        border = gameObject.transform.GetChild(0).GetComponent<Image>();
        ZoomIcon = gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        isClicked = false;
        cameraControl = GameObject.Find("Main Camera").GetComponent<CameraControl>();
        inventory = GameObject.Find("inventory").GetComponent<Inventory>();
        itemType = "";
    }
    
    public void SetBorderColor()
    {
        border.color = new Color(1, 150/255f, 0);
    }

    public void SetZoomIconOn()
    {
        if (isClicked)
        {
            Cursor.SetCursor(mouse, new Vector2(11, 0), CursorMode.Auto);
            Color color;
            color = ZoomIcon.color;
            color.a = 1;
            ZoomIcon.color = color;
        }
    }

    public void SetZoomIconOff()
    {
        if (isClicked)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            Color color;
            color = ZoomIcon.color;
            color.a = 0;
            ZoomIcon.color = color;
        }
    }

    public bool GetIsClicked()
    {
        return isClicked;
    }

    // 클릭한 아이콘만 활성화 후 아이템 정보를 넘김
    public void SetItemType(string type)
    {
        int childCnt = gameObject.transform.parent.childCount;
        for (int i = 0; i < childCnt; i++)
        {
            gameObject.transform.parent.GetChild(i).GetChild(0).GetComponent<Image>().color = Color.white;
            gameObject.transform.parent.GetChild(i).GetComponent<Item>().ClickCancel();
        }
        SetBorderColor();
        isClicked = true;
        itemType = type;
        cameraControl.SetItemType(type);
    }

    // 아이템 뷰 관련 함수
    public void ItemViewOn()
    {
        if (isClicked)
            inventory.ItemViewOn(itemType);
    }

    public void ClickCancel()
    {
        isClicked = false;
    }

    
}
