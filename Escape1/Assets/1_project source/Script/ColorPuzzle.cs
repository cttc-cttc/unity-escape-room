using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPuzzle : MonoBehaviour
{
    public Material btn_red;
    public Material btn_orange;
    public Material btn_yellow;
    public Material btn_green;
    public Material btn_blue;
    public Material btn_purple;
    public Material btn_black;
    List<int> answer;
    List<Material> btnArray;
    MeshRenderer btn1;
    MeshRenderer btn2;
    MeshRenderer btn3;
    MeshRenderer btn4;
    MeshRenderer btn5;
    MeshRenderer btn6;
    int btn1Count = 0;
    int btn2Count = 0;
    int btn3Count = 0;
    int btn4Count = 0;
    int btn5Count = 0;
    int btn6Count = 0;
    public SfxManager sfx;
    MeshRenderer lamp;
    bool isClear;

    void Awake()
    {
        answer = new List<int>();
        answer.Add(5);
        answer.Add(2);
        answer.Add(1);
        answer.Add(6);
        answer.Add(3);
        answer.Add(4);
        // test¿ë
        //answer.Add(1);
        //answer.Add(0);
        //answer.Add(0);
        //answer.Add(0);
        //answer.Add(0);
        //answer.Add(0);
        btnArray = new List<Material>();
        btnArray.Add(btn_red);
        btnArray.Add(btn_orange);
        btnArray.Add(btn_yellow);
        btnArray.Add(btn_green);
        btnArray.Add(btn_blue);
        btnArray.Add(btn_purple);
        btnArray.Add(btn_black);
        btn1 = transform.GetChild(0).GetComponent<MeshRenderer>();
        btn2 = transform.GetChild(1).GetComponent<MeshRenderer>();
        btn3 = transform.GetChild(2).GetComponent<MeshRenderer>();
        btn4 = transform.GetChild(3).GetComponent<MeshRenderer>();
        btn5 = transform.GetChild(4).GetComponent<MeshRenderer>();
        btn6 = transform.GetChild(5).GetComponent<MeshRenderer>();
        lamp = transform.GetChild(6).GetComponent<MeshRenderer>();
        isClear = false;
    }

    public void RotateColor(string btn)
    {
        switch (btn)
        {
            case "colorBtn1":
                btn1.material = btnArray[btn1Count];
                ManageCount(btn1Count, 1);
                break;
            case "colorBtn2":
                btn2.material = btnArray[btn2Count];
                ManageCount(btn2Count, 2);
                break;
            case "colorBtn3":
                btn3.material = btnArray[btn3Count];
                ManageCount(btn3Count, 3);
                break;
            case "colorBtn4":
                btn4.material = btnArray[btn4Count];
                ManageCount(btn4Count, 4);
                break;
            case "colorBtn5":
                btn5.material = btnArray[btn5Count];
                ManageCount(btn5Count, 5);
                break;
            case "colorBtn6":
                btn6.material = btnArray[btn6Count];
                ManageCount(btn6Count, 6);
                break;
            default: break;
        }

        sfx.PlaySfx("lightSwitch");
        CheckAnswer();
    }

    void ManageCount(int count, int btn)
    {
        count++;
        if (count % 7 == 0)
        {
            count = 0;
        }

        switch (btn)
        {
            case 1:
                btn1Count = count;
                break;
            case 2:
                btn2Count = count;
                break;
            case 3:
                btn3Count = count;
                break;
            case 4:
                btn4Count = count;
                break;
            case 5:
                btn5Count = count;
                break;
            case 6:
                btn6Count = count;
                break;
            default: break;
        }
    }

    void CheckAnswer()
    {
        for (int i=0; i<answer.Count; i++)
        {
            switch (i)
            {
                case 0:
                    if (btn1Count != answer[i])
                    {
                        return;
                    }
                    break;
                case 1:
                    if (btn2Count != answer[i])
                    {
                        return;
                    }
                    break;
                case 2:
                    if (btn3Count != answer[i])
                    {
                        return;
                    }
                    break;
                case 3:
                    if (btn4Count != answer[i])
                    {
                        return;
                    }
                    break;
                case 4:
                    if (btn5Count != answer[i])
                    {
                        return;
                    }
                    break;
                case 5:
                    if (btn6Count != answer[i])
                    {
                        return;
                    }
                    break;
                default: break;
            }

            if (i == answer.Count-1)
            {
                sfx.PlaySfx("itemUsed");
                btn1.gameObject.GetComponent<Collider>().enabled = false;
                btn2.gameObject.GetComponent<Collider>().enabled = false;
                btn3.gameObject.GetComponent<Collider>().enabled = false;
                btn4.gameObject.GetComponent<Collider>().enabled = false;
                btn5.gameObject.GetComponent<Collider>().enabled = false;
                btn6.gameObject.GetComponent<Collider>().enabled = false;
                lamp.material = btnArray[3];
                isClear = true;
            }
        }
    }

    public bool GetIsClear()
    {
        return isClear;
    }

}
