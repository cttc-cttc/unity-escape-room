using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenHandle : MonoBehaviour
{
    public Texture2D mouse;
    public CameraControl cameraControl;
    GameObject[] leftArrowTag;
    GameObject[] rightArrowTag;
    GameObject[] downArrowTag;
    bool enableLeft;
    bool enableRight;
    bool enableDown;

    public Image leftA;
    public Image rightA;
    public Image downA;

    void Awake()
    {
        leftArrowTag = GameObject.FindGameObjectsWithTag("leftArrow");
        rightArrowTag = GameObject.FindGameObjectsWithTag("rightArrow");
        downArrowTag = GameObject.FindGameObjectsWithTag("downArrow");
        enableLeft = true;
        enableRight = true;
        enableDown = false;
    }

    void Start()
    {
        // ���� ���� ��, UI �̵� ȭ��ǥ Ȱ��ȭ ���� ���� (��, �츸 Ȱ��ȭ)
        enableArrow(enableLeft, leftArrowTag);
        enableArrow(enableRight, rightArrowTag);
        enableArrow(enableDown, downArrowTag);
    }

    // ��鿡 ���� �̵� ȭ��ǥ Ȱ��ȭ
    void enableArrow(bool isEnable, GameObject[] arrowObjects)
    {
        if (isEnable)
        {
            foreach (GameObject arrObj in arrowObjects)
            {
                arrObj.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject arrObj in arrowObjects)
            {
                arrObj.SetActive(false);
            }
        }
    }

    // ��鿡 ���� �̵� ȭ��ǥ Ȱ��ȭ
    public void SetEnableArrow(bool left, bool right, bool down)
    {
        enableLeft = left;
        enableRight = right;
        enableDown = down;
        enableArrow(enableLeft, leftArrowTag);
        enableArrow(enableRight, rightArrowTag);
        enableArrow(enableDown, downArrowTag);
    }

    // ���콺 Ŀ�� ����
    public void SetMouseCursor(bool status)
    {
        if (status) 
            Cursor.SetCursor(mouse, new Vector2(11, 0), CursorMode.Auto);
        else
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    // �̵� ȭ��ǥ�� ���̵� �� �ִϸ��̼��� ���� �ڷ�ƾ
    public void FadeInArrow(string img)
    {
        switch (img)
        {
            case "left":
                StartCoroutine(PlayFadeIn(leftA));
                break;
            case "right":
                StartCoroutine(PlayFadeIn(rightA));
                break;
            case "down":
                StartCoroutine(PlayFadeIn(downA));
                break;
            default: break;
        }
    }

    IEnumerator PlayFadeIn(Image img)
    {
        Color color = img.color;
        color.a = 0;
        img.color = color;

        float start = 0;
        float end = 1;
        float time = 0;
        float animTime = 0.8f;
        while (color.a < 0.5f)
        {
            time += Time.deltaTime / animTime;
            color.a = Mathf.Lerp(start, end, time);
            img.color = color;
            yield return null;
        }
    }

    // ��� ��ȯ �� �߻��ϴ� �̺�Ʈ ����
    public void SetArrow(string arrow)
    {
        switch (arrow)
        {
            case "left":
                cameraControl.TurnLeft();
                break;
            case "right":
                cameraControl.TurnRight();
                break;
            case "down":
                cameraControl.ZoomOut();
                break;
        }
    }

    public void SetDownArrowStatus(bool status)
    {
        downA.enabled = status;
    }
}
