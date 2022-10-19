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
        // 게임 시작 시, UI 이동 화살표 활성화 여부 결정 (좌, 우만 활성화)
        enableArrow(enableLeft, leftArrowTag);
        enableArrow(enableRight, rightArrowTag);
        enableArrow(enableDown, downArrowTag);
    }

    // 장면에 따른 이동 화살표 활성화
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

    // 장면에 따른 이동 화살표 활성화
    public void SetEnableArrow(bool left, bool right, bool down)
    {
        enableLeft = left;
        enableRight = right;
        enableDown = down;
        enableArrow(enableLeft, leftArrowTag);
        enableArrow(enableRight, rightArrowTag);
        enableArrow(enableDown, downArrowTag);
    }

    // 마우스 커서 변경
    public void SetMouseCursor(bool status)
    {
        if (status) 
            Cursor.SetCursor(mouse, new Vector2(11, 0), CursorMode.Auto);
        else
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    // 이동 화살표의 페이드 인 애니메이션을 위한 코루틴
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

    // 장면 전환 시 발생하는 이벤트 설정
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
