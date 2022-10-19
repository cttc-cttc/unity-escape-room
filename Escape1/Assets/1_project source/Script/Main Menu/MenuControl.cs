using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public Texture2D mouse;
    Image mainMenu;
    GameObject menuObj;
    GameObject infoMenu;
    Image menuScreen;
    Image gameOverScreen;

    void Awake()
    {
        mainMenu = gameObject.GetComponent<Image>();
        menuObj = gameObject.transform.GetChild(0).gameObject;
        infoMenu = gameObject.transform.GetChild(0).GetChild(4).gameObject;
        menuScreen = gameObject.transform.GetChild(0).GetChild(5).gameObject.GetComponent<Image>();
        gameOverScreen = gameObject.transform.GetChild(1).GetChild(3).gameObject.GetComponent<Image>();
    }

    void Start()
    {
        StartCoroutine("StartMenuScreenFadeOut");
    }

    IEnumerator StartMenuScreenFadeOut()
    {
        yield return new WaitForSecondsRealtime(1f);
        Color color = menuScreen.color;
        float start = 1;
        float end = 0;
        float time = 0;
        float animTime = 2;
        while (color.a > 0)
        {
            time += Time.deltaTime / animTime;
            color.a = Mathf.Lerp(start, end, time);
            menuScreen.color = color;
            yield return null;
        }
        menuScreen.gameObject.SetActive(false);
    }

    public void GameStart()
    {
        menuScreen.gameObject.SetActive(true);
        StartCoroutine("StartMenuScreenFadeIn");
    }

    // �������� Lerp(a, b, t) : t�� 0���� 1������ ���̰� t=0�� �� a�� ��ȯ, t=1�� �� b�� ��ȯ��
    // t�� 0.5�� a, b�� �߰����� ��ȯ�ϸ� ��ȯ������ ������ ���� ������ ������

    // ȭ���� ���� 0�� ���¿��� 1�� �Ǹ� ���� ������ fade in
    IEnumerator StartMenuScreenFadeIn()
    {
        Color color = menuScreen.color;
        float start = 0;
        float end = 1;
        float time = 0;
        while (color.a < 1)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(start, end, time);
            menuScreen.color = color;
            yield return null;
        }
        menuObj.SetActive(false);
        StartCoroutine("StartMainMenuFadeOut");
    }

    // ȭ���� ���� 1�� ���¿��� 0�� �Ǹ� ���� ȭ���� ���̱� ������ fade out
    IEnumerator StartMainMenuFadeOut()
    {
        Color color = mainMenu.color;
        float start = 1;
        float end = 0;
        float time = 0;
        while (color.a > 0)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(start, end, time);
            mainMenu.color = color;
            yield return null;
        }
        gameObject.SetActive(false);
    }

    public void OpenInfo()
    {
        infoMenu.SetActive(true);
    }

    public void CloseInfo()
    {
        infoMenu.SetActive(false);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void GoMainMenu()
    {
        gameOverScreen.gameObject.SetActive(true);
        StartCoroutine("FadeInGameOverScreen");
    }

    IEnumerator FadeInGameOverScreen()
    {
        Color color = gameOverScreen.color;
        float start = 0;
        float end = 1;
        float time = 0;
        while (color.a < 1)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(start, end, time);
            gameOverScreen.color = color;
            yield return null;
        }
        SceneManager.LoadScene(0);
    }

    public void SetCursorOn()
    {
        Cursor.SetCursor(mouse, new Vector2(11, 0), CursorMode.Auto);
    }

    public void SetCursorOff()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
