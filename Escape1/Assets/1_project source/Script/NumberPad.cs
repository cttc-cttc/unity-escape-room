using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberPad : MonoBehaviour
{
    public Material lamp_green;
    public Material num_0;
    public Material num_1;
    public Material num_2;
    public Material num_3;
    public Material num_4;
    public Material num_5;
    public Material num_6;
    public Material num_7;
    public Material num_8;
    public Material num_9;
    List<int> answer;
    List<Material> btnArray;
    MeshRenderer btn1;
    MeshRenderer btn2;
    MeshRenderer btn3;
    MeshRenderer btn4;
    int btn1Count = 0;
    int btn2Count = 0;
    int btn3Count = 0;
    int btn4Count = 0;
    public SfxManager sfx;
    MeshRenderer lamp;
    bool isClear;

    void Awake()
    {
        answer = new List<int>();
        btnArray = new List<Material>();
        btn1 = transform.GetChild(0).GetComponent<MeshRenderer>();
        btn2 = transform.GetChild(1).GetComponent<MeshRenderer>();
        btn3 = transform.GetChild(2).GetComponent<MeshRenderer>();
        btn4 = transform.GetChild(3).GetComponent<MeshRenderer>();
        lamp = transform.GetChild(4).GetComponent<MeshRenderer>();
        btnArray.Add(num_0);
        btnArray.Add(num_1);
        btnArray.Add(num_2);
        btnArray.Add(num_3);
        btnArray.Add(num_4);
        btnArray.Add(num_5);
        btnArray.Add(num_6);
        btnArray.Add(num_7);
        btnArray.Add(num_8);
        btnArray.Add(num_9);
        answer.Add(3);
        answer.Add(5);
        answer.Add(2);
        answer.Add(9);
        // test¿ë
        //answer.Add(1);
        //answer.Add(0);
        //answer.Add(0);
        //answer.Add(0);
    }

    public void RotateNumber(string btn)
    {
        switch (btn)
        {
            case "numberBtn1":
                ManageCount(btn1Count, 1);
                btn1.material = btnArray[btn1Count];
                break;
            case "numberBtn2":
                ManageCount(btn2Count, 2);
                btn2.material = btnArray[btn2Count];
                break;
            case "numberBtn3":
                ManageCount(btn3Count, 3);
                btn3.material = btnArray[btn3Count];
                break;
            case "numberBtn4":
                ManageCount(btn4Count, 4);
                btn4.material = btnArray[btn4Count];
                break;
            default :break;
        }

        sfx.PlaySfx("lightSwitch");
        CheckAnswer();
    }

    void ManageCount(int count, int btn)
    {
        count++;
        if (count % 10 == 0)
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
            default: break;
        }
    }

    void CheckAnswer()
    {
        for (int i = 0; i < answer.Count; i++)
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
                default: break;
            }

            if (i == answer.Count - 1)
            {
                sfx.PlaySfx("itemUsed");
                btn1.gameObject.GetComponent<Collider>().enabled = false;
                btn2.gameObject.GetComponent<Collider>().enabled = false;
                btn3.gameObject.GetComponent<Collider>().enabled = false;
                btn4.gameObject.GetComponent<Collider>().enabled = false;
                lamp.material = lamp_green;
                isClear = true;
            }
        }
    }

    public bool GetIsClear()
    {
        return isClear;
    }
}
