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

    // 선형보간 Lerp(a, b, t) : t는 0부터 1까지의 값이고 t=0일 때 a를 반환, t=1일 때 b를 반환함
    // t가 0.5면 a, b의 중간값을 반환하며 반환값에는 일정한 작은 간격이 존재함

    // 화면이 투명도 0인 상태에서 1이 되며 덮기 때문에 fade in
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

    // 화면이 투명도 1인 상태에서 0이 되며 게임 화면이 보이기 때문에 fade out
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
