using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public CameraControl cameraControl;
    public Texture2D mouse;
    public GameObject gridSetting;
    GameObject itemView;
    GameObject itemZoomArea;
    GameObject infoTextArea;
    GameObject page1;
    GameObject page2;
    Image itemZoom;
    string currentItem;
    Sprite blueKey;
    Sprite bottle;
    Sprite handkerchief;
    Sprite handkerchiefActive;
    Sprite handkerchiefActiveInventory;
    bool isHandkerchiefCombined;
    Sprite battery;
    Sprite cabinetKey;
    Sprite whaleKey;
    Sprite whaleKeyOn;
    Sprite whaleKeyOnInventory;
    bool isWhaleKeyCombined;
    Sprite crumpledPaper;
    Sprite hintPaper;
    Sprite hintPaperInventory;
    bool isCppViewed;
    Sprite blackKey;

    public SfxManager sfx;

    private void Awake()
    {
        currentItem = "";
        itemView = transform.GetChild(1).gameObject;
        itemZoomArea = itemView.transform.GetChild(0).gameObject;
        infoTextArea = itemView.transform.GetChild(2).gameObject;
        page1 = infoTextArea.transform.GetChild(1).gameObject;
        page2 = infoTextArea.transform.GetChild(2).gameObject;
        itemZoom = itemZoomArea.GetComponent<Image>();
        itemZoom.preserveAspect = true;
        blueKey = Resources.Load<Sprite>("view_blue_key");
        bottle = Resources.Load<Sprite>("view_bottle");
        handkerchief = Resources.Load<Sprite>("view_handkerchief");
        handkerchiefActive = Resources.Load<Sprite>("view_handkerchief_active");
        handkerchiefActiveInventory = Resources.Load<Sprite>("handkerchief_active");
        isHandkerchiefCombined = false;
        battery = Resources.Load<Sprite>("view_battery");
        cabinetKey = Resources.Load<Sprite>("view_cabinet_key");
        whaleKey = Resources.Load<Sprite>("view_whale_key");
        whaleKeyOn = Resources.Load<Sprite>("view_whale_key_on");
        whaleKeyOnInventory = Resources.Load<Sprite>("whale_key_on");
        isWhaleKeyCombined = false;
        crumpledPaper = Resources.Load<Sprite>("view_crumpled_paper");
        hintPaper = Resources.Load<Sprite>("view_hint_paper");
        hintPaperInventory = Resources.Load<Sprite>("hint_paper");
        isCppViewed = false;
        blackKey = Resources.Load<Sprite>("view_black_key");
    }

    public void AcquireItem(string tag)
    {
        switch (tag)
        {
            case "blue_key":
                AddInventory(tag);
                break;
            case "bottle":
                AddInventory(tag);
                break;
            case "handkerchief":
                AddInventory(tag);
                break;
            case "battery":
                AddInventory(tag);
                break;
            case "cabinet_key":
                AddInventory(tag);
                break;
            case "whale_key":
                AddInventory(tag);
                break;
            case "crumpled_paper":
                AddInventory(tag);
                break;
            case "black_key":
                AddInventory(tag);
                break;
            default: break;
        }
    }

    void AddInventory(string tag)
    {
        GameObject prefab = Resources.Load<GameObject>("pf_" + tag);
        Instantiate(prefab).transform.SetParent(gridSetting.transform, false); // 동적으로 자식 객체 생성
    }

    public void ItemViewOn(string itemType)
    {
        itemView.SetActive(true);
        if (itemType.Equals("info1"))
        {
            itemZoomArea.SetActive(false);
            infoTextArea.SetActive(true);
            page1.SetActive(true);
            page2.SetActive(false);
        }
        else if (itemType.Equals("info2"))
        {
            itemZoomArea.SetActive(false);
            infoTextArea.SetActive(true);
            page1.SetActive(false);
            page2.SetActive(true);
        }
        else
        {
            itemZoomArea.SetActive(true);
            infoTextArea.SetActive(false);
            switch (itemType)
            {
                case "blue_key":
                    itemZoom.sprite = blueKey;
                    break;
                case "bottle":
                    itemZoom.sprite = bottle;
                    break;
                case "handkerchief":
                    if (!isHandkerchiefCombined)
                        itemZoom.sprite = handkerchief;
                    else
                        itemZoom.sprite = handkerchiefActive;
                    break;
                case "battery":
                    itemZoom.sprite = battery;
                    break;
                case "cabinet_key":
                    itemZoom.sprite = cabinetKey;
                    break;
                case "whale_key":
                    if (!isWhaleKeyCombined)
                        itemZoom.sprite = whaleKey;
                    else
                        itemZoom.sprite = whaleKeyOn;
                    break;
                case "crumpled_paper":
                    if (!isCppViewed)
                        itemZoom.sprite = crumpledPaper;
                    else
                        itemZoom.sprite = hintPaper;
                    break;
                case "black_key":
                    itemZoom.sprite = blackKey;
                    break;
                default: break;
            }
            currentItem = itemType;
        }
    }

    public void CombineItem()
    {
        string itemType = cameraControl.GetItemType();

        // 구겨진 종이를 펼쳤을 때 힌트 종이로 변환
        if (!isCppViewed && currentItem.Equals("crumpled_paper"))
        {
            itemZoom.sprite = hintPaper;
            gridSetting.transform.Find("pf_crumpled_paper(Clone)").GetComponent<Image>().sprite = hintPaperInventory;
            isCppViewed = true;
            sfx.PlaySfx("crumplingPaper");
        }

        // 손수건에 와인을 적셨을 때 젖은 손수건으로 변환
        if (currentItem.Equals("handkerchief") && itemType.Equals("bottle"))
        {
            itemZoom.sprite = handkerchiefActive;
            gridSetting.transform.Find("pf_handkerchief(Clone)").GetComponent<Image>().sprite = handkerchiefActiveInventory;
            isHandkerchiefCombined = true;
            cameraControl.UseItem();
            cameraControl.SetItemType("");
        }

        // 고래 열쇠에 배터리를 넣었을 때 열쇠의 스위치 온
        if (currentItem.Equals("whale_key") && itemType.Equals("battery"))
        {
            itemZoom.sprite = whaleKeyOn;
            gridSetting.transform.Find("pf_whale_key(Clone)").GetComponent<Image>().sprite = whaleKeyOnInventory;
            isWhaleKeyCombined = true;
            cameraControl.UseItem();
            cameraControl.SetItemType("");
        }
    }

    public void SetCursorOn()
    {
        Cursor.SetCursor(mouse, new Vector2(11, 0), CursorMode.Auto);
    }

    public void SetCursorOff()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void ItemViewOff()
    {
        itemView.SetActive(false);
        SetCursorOff();
    }

    public bool GetIsHandkerchiefCombined()
    {
        return isHandkerchiefCombined;
    }
    
    public bool GetIsWhaleKeyCombined()
    {
        return isWhaleKeyCombined;
    }
}
