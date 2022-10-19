using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CameraControl : MonoBehaviour
{
    public ScreenHandle screenHandle;
    RaycastHit hit;

    // 전등 스위치 관련
    public CeilingLight ceiling;
    public OtherLight other;
    Light ceilingLight;
    Light otherLight;

    // 3가지 정보 리스트
    List<Collider> colInfo;
    List<Vector3> posInfo;
    List<Quaternion> quaInfo;
    public Inventory inventory;

    // 바닥 전등
    static Vector3 floorLampPos = new Vector3(5.5f, 4.5f, 3.5f);
    static Vector3 floorLampQua = new Vector3(1, -5, 6);
    static Vector3 passAreaPos = new Vector3(5.8f, 1, 6);
    static Vector3 passAreaQua = new Vector3(0, -10, 4);
    public GameObject password;

    // 벽 버튼
    static Vector3 wallBtnAreaPos = new Vector3(-7, 3.5f, -0.3f);
    static Vector3 wallBtnAreaQua = new Vector3(-7.6f, 0, 0);
    public GameObject wallButton;
    public GameObject wallShelf2;
    bool isPushed;

    // 장식장
    static Vector3 shelfPos = new Vector3(1.65f, 3, 4.7f);
    static Vector3 shelfQua = new Vector3(0, -4, 5);
    bool shelfPot1Moved;
    static Vector3 shelfPot1Dist = new Vector3(0.3f, 0, 0);
    bool shelfPot2Moved;
    static Vector3 shelfPot2Dist = new Vector3(0.3f, 0, 0);

    // 책장 1
    static Vector3 bookcase1Pos = new Vector3(-4, 4.2f, 2);
    static Vector3 zBookcase1Pos = new Vector3(-2.8f, 3.6f, 5);

    // 책장 2
    static Vector3 bookcase2Pos = new Vector3(3.5f, 3.7f, 1.6f);
    static Vector3 bookcase2Qua = new Vector3(6.4f, -3, 0);
    static Vector3 zBookcase21Pos = new Vector3(6, 3.3f, 2.2f);
    static Vector3 zBookcase21Qua = new Vector3(6.5f, -4, 0);
    static Vector3 zBookcase22Pos = new Vector3(6, 2, 0.97f);
    static Vector3 zBookcase22Qua = new Vector3(6, 0, 0);
    bool isCabinetUnlock;
    bool isCabinetOpen;
    public ColorPuzzle colorPuzzle;
    bool isColorPuzzleScene;
    bool isBookcaseDoorOpen;

    // 사무 테이블
    static Vector3 deskPos = new Vector3(4.1f, 2.8f, -2);
    static Vector3 deskQua = new Vector3(9, -7, 2);
    bool isDrawer1Open;
    bool isDrawer1Unlock;
    bool isDrawer2Open;
    bool isNumberPadScene;
    static Vector3 numberPadPos = new Vector3(5, 1.25f, -1.13f);
    static Vector3 numberPadQua = new Vector3(5.4f, 0, 0);
    public NumberPad numberPad;
    static Vector3 bnpPos = new Vector3(5, 3.5f, -3.5f);
    static Vector3 bnpQua = new Vector3(6.3f, -5, 0);

    // 벽 선반
    static Vector3 wallShelfPos = new Vector3(-0.4f, 5.1f, -3);
    static Vector3 wallShelfQua = new Vector3(0, -0.4f, -8);
    static Vector3 wallShelf1Pos = new Vector3(1.4f, 5.2f, -5);
    static Vector3 wallShelf1Qua = new Vector3(0, 0, -7.2f);
    static Vector3 wallShelf2Pos = new Vector3(-1.7f, 5.2f, -5);
    static Vector3 wallShelf2Qua = new Vector3(0, 0, -7.2f);

    // 바닥 식물
    static Vector3 floorPlantPos = new Vector3(-3.5f, 2.5f, -4.5f);
    static Vector3 floorPlantQua = new Vector3(-6, -4, -5.8f);
    static Vector3 hintAreaPos = new Vector3(-5.1f, 2, -6.3f);
    static Vector3 hintAreaQua = new Vector3(-6.1f, -7, 0);

    // 벽 구멍
    public GameObject hiddenDoor;
    static Vector3 wallHoleAreaPos = new Vector3(-6.8f, 3.1f, 2.7f);
    static Vector3 wallHoleAreaQua = new Vector3(-7.7f, -3, 3);

    // 출입문
    static Vector3 doorPos = new Vector3(-3, 3, -2.1f);
    static Vector3 doorQua = new Vector3(-7.8f, -1, 0);
    public DoorObj doorObj;

    // 캔버스 요소
    public Image mainMenu;
    string itemType;
    Image gameOverScreen;

    public SfxManager sfx;

    void Awake()
    {
        ceilingLight = ceiling.GetComponent<Light>();
        otherLight = other.GetComponent<Light>();

        colInfo = new List<Collider>();
        posInfo = new List<Vector3>();
        quaInfo = new List<Quaternion>();
        itemType = "";

        isPushed = false;

        shelfPot1Moved = false;
        shelfPot2Moved = false;

        isDrawer1Open = false;
        isDrawer1Unlock = false;
        isDrawer2Open = false;
        isNumberPadScene = false;

        isCabinetUnlock = false;
        isCabinetOpen = false;
        isColorPuzzleScene = false;
        isBookcaseDoorOpen = false;

        gameOverScreen = mainMenu.gameObject.transform.GetChild(1).GetChild(3).gameObject.GetComponent<Image>();
    }

    // ray를 쏴서 클릭하는 오브젝트에 대한 태그를 반환 
    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // UI 위에서 클릭된 경우가 아니면 (게임 오브젝트 직접 클릭)
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    string tag = hit.collider.tag;
                    //Debug.Log(tag + hit.transform.position);

                    if (tag.Equals("lightSwitch")) // 전등 스위치
                    {
                        sfx.PlaySfx("lightSwitch");
                        if (!ceilingLight.enabled)
                        {
                            ceilingLight.enabled = true;
                            otherLight.enabled = false;
                            ChangeMaterial change = hit.collider.gameObject.GetComponent<ChangeMaterial>();
                            change.SetMaterial(2);
                            password.SetActive(false);
                        }
                        else
                        {
                            ceilingLight.enabled = false;
                            otherLight.enabled = true;
                            ChangeMaterial change = hit.collider.gameObject.GetComponent<ChangeMaterial>();
                            change.SetMaterial(1);
                            password.SetActive(true);
                        }
                    }
                    else if (tag.Equals("floorLamp")) // 바닥 전등
                    {
                        ZoomIn(hit.collider, floorLampPos, floorLampQua);
                    }
                    else if (tag.Equals("lampPass"))
                    {
                        ZoomIn(hit.collider, passAreaPos, passAreaQua);
                    }
                    else if (tag.Equals("passCover"))
                    {
                        if (inventory.GetIsHandkerchiefCombined() && itemType.Equals("handkerchief"))
                        {
                            hit.transform.gameObject.SetActive(false);
                            UseItem();
                        }
                    }
                    else
                    {
                        // 전등 스위치가 꺼져 있으면 바닥 전등 외 다른 사물 확대 불가
                        if (ceilingLight.enabled)
                        {
                            switch (tag)
                            {
                                case "wallBtnArea": // 벽 버튼
                                    if (!isPushed)
                                    {
                                        ZoomIn(hit.collider, wallBtnAreaPos, wallBtnAreaQua);
                                    }
                                    break;
                                case "wallBtn":
                                    hit.collider.enabled = false;
                                    sfx.PlaySfx("wallBtnOn");
                                    StartCoroutine("HiddenDoorOn");
                                    break;
                                case "shelf": // 장식장
                                    ZoomIn(hit.collider, shelfPos, shelfQua);
                                    break;
                                case "shelfpot1":
                                    if (!shelfPot1Moved)
                                    {
                                        hit.transform.position -= shelfPot1Dist;
                                        shelfPot1Moved = true;
                                    }
                                    else
                                    {
                                        hit.transform.position += shelfPot1Dist;
                                        shelfPot1Moved = false;
                                    }
                                    break;
                                case "shelfpot2":
                                    if (!shelfPot2Moved)
                                    {
                                        hit.transform.position += shelfPot2Dist;
                                        shelfPot2Moved = true;
                                    }
                                    else
                                    {
                                        hit.transform.position -= shelfPot2Dist;
                                        shelfPot2Moved = false;
                                    }
                                    break;
                                case "handkerchief":
                                    EatItem(hit.collider.gameObject, tag);
                                    break;
                                case "bookcase1": // 책장 1
                                    ZoomIn(hit.collider, bookcase1Pos, Vector3.zero);
                                    break;
                                case "z_bookcase1":
                                    ZoomIn(hit.collider, zBookcase1Pos, Vector3.zero);
                                    break;
                                case "blue_key":
                                    EatItem(hit.collider.gameObject, tag);
                                    break;
                                case "black_key":
                                    EatItem(hit.collider.gameObject, tag);
                                    break;
                                case "bookcase2": // 책장 2
                                    ZoomIn(hit.collider, bookcase2Pos, bookcase2Qua);
                                    break;
                                case "z_bookcase2_1":
                                    ZoomIn(hit.collider, zBookcase21Pos, zBookcase21Qua);
                                    break;
                                case "cabinet":
                                    if (!isCabinetUnlock) // 캐비넷이 잠겨 있으면
                                    {
                                        if (itemType == "cabinet_key")
                                        {
                                            UseItem();
                                            isCabinetUnlock = true;
                                        }
                                        else
                                        {
                                            sfx.PlaySfx("cabinetLock");
                                        }
                                    }
                                    else // 캐비넷 잠김이 풀리면
                                    {
                                        hit.transform.GetChild(1).Rotate(new Vector3(1, 0, 0), 90, Space.Self);
                                        hit.collider.enabled = false;
                                        isCabinetOpen = true;
                                    }
                                    break;
                                case "whale_key":
                                    EatItem(hit.collider.gameObject, tag);
                                    break;
                                case "z_bookcase2_2":
                                    ZoomIn(hit.collider, zBookcase22Pos, zBookcase22Qua);
                                    isColorPuzzleScene = true;
                                    break;
                                case "colorBtn1":
                                    colorPuzzle.RotateColor(tag);
                                    break;
                                case "colorBtn2":
                                    colorPuzzle.RotateColor(tag);
                                    break;
                                case "colorBtn3":
                                    colorPuzzle.RotateColor(tag);
                                    break;
                                case "colorBtn4":
                                    colorPuzzle.RotateColor(tag);
                                    break;
                                case "colorBtn5":
                                    colorPuzzle.RotateColor(tag);
                                    break;
                                case "colorBtn6":
                                    colorPuzzle.RotateColor(tag);
                                    break;
                                case "bookcase2Doors":
                                    if (!colorPuzzle.GetIsClear() && !isColorPuzzleScene)
                                    {
                                        sfx.PlaySfx("drawerLock");
                                    }
                                    else if (colorPuzzle.GetIsClear() && !isColorPuzzleScene)
                                    {
                                        hit.transform.GetChild(0).transform.Rotate(Vector3.up, 90, Space.World);
                                        hit.transform.GetChild(1).transform.Rotate(Vector3.up, -90, Space.World);
                                        hit.collider.enabled = false;
                                        hit.transform.GetChild(1).GetChild(2).gameObject.GetComponent<Collider>().enabled = false;
                                        isBookcaseDoorOpen = true;
                                    }
                                    break;
                                case "bottle":
                                    EatItem(hit.collider.gameObject, tag);
                                    break;
                                case "desk": // 사무 테이블
                                    ZoomIn(hit.collider, deskPos, deskQua);
                                    break;
                                case "drawer1":
                                    if (!isDrawer1Unlock) // 서랍이 잠겨 있으면
                                    {
                                        if (itemType == "blue_key")
                                        {
                                            UseItem();
                                            isDrawer1Unlock = true;
                                        }
                                        else
                                        {
                                            sfx.PlaySfx("drawerLock");
                                        }
                                    }
                                    else // 서랍 잠김이 풀리면
                                    {
                                        if (!isDrawer1Open)
                                        {
                                            hit.transform.position += new Vector3(-0.8f, 0, 0);
                                            isDrawer1Open = true;
                                        }
                                        else
                                        {
                                            hit.transform.position += new Vector3(0.8f, 0, 0);
                                            isDrawer1Open = false;
                                        }
                                    }
                                    break;
                                case "battery":
                                    EatItem(hit.collider.gameObject, tag);
                                    break;
                                case "drawer2":
                                    if (!isDrawer2Open)
                                    {
                                        if (!numberPad.GetIsClear() && !isNumberPadScene)
                                        {
                                            sfx.PlaySfx("drawerLock");
                                        }
                                        else if (numberPad.GetIsClear() && !isNumberPadScene)
                                        {
                                            hit.transform.Rotate(Vector3.up, 123, Space.World);
                                            isDrawer2Open = true;
                                        }
                                    }
                                    else
                                    {
                                        hit.transform.Rotate(Vector3.up, -123, Space.World);
                                        isDrawer2Open = false;
                                    }
                                    break;
                                case "numberPad":
                                    ZoomIn(hit.collider, numberPadPos, numberPadQua);
                                    isNumberPadScene = true;
                                    break;
                                case "numberBtn1":
                                    numberPad.RotateNumber(tag);
                                    break;
                                case "numberBtn2":
                                    numberPad.RotateNumber(tag);
                                    break;
                                case "numberBtn3":
                                    numberPad.RotateNumber(tag);
                                    break;
                                case "numberBtn4":
                                    numberPad.RotateNumber(tag);
                                    break;
                                case "cabinet_key":
                                    EatItem(hit.collider.gameObject, tag);
                                    break;
                                case "booksnpaper":
                                    ZoomIn(hit.collider, bnpPos, bnpQua);
                                    break;
                                case "wallShelf": // 벽 선반
                                    ZoomIn(hit.collider, wallShelfPos, wallShelfQua);
                                    break;
                                case "wallShelf1":
                                    ZoomIn(hit.collider, wallShelf1Pos, wallShelf1Qua);
                                    break;
                                case "glassWhale":
                                    if (inventory.GetIsWhaleKeyCombined() && itemType.Equals("whale_key"))
                                    {
                                        UseItem();
                                        SetItemType("");
                                        StartCoroutine("LaserOn");
                                    }
                                    break;
                                case "wallShelf2":
                                    ZoomIn(hit.collider, wallShelf2Pos, wallShelf2Qua);
                                    break;
                                case "floorPlant": // 바닥 식물
                                    ZoomIn(hit.collider, floorPlantPos, floorPlantQua);
                                    break;
                                case "hintArea":
                                    ZoomIn(hit.collider, hintAreaPos, hintAreaQua);
                                    break;
                                case "crumpled_paper":
                                    EatItem(hit.collider.gameObject, tag);
                                    break;
                                case "wallHoleArea": // 벽 구멍
                                    ZoomIn(hit.collider, wallHoleAreaPos, wallHoleAreaQua);
                                    break;
                                case "door": // 출입문
                                    ZoomIn(hit.collider, doorPos, doorQua);
                                    break;
                                case "door_obj":
                                    if (!doorObj.GetIsOpen())
                                    {
                                        if (itemType == "black_key")
                                        {
                                            UseItem();
                                            doorObj.SetIsOpen(true);

                                        }
                                        else
                                        {
                                            sfx.PlaySfx("doorLock");
                                        }
                                    }
                                    else
                                    {
                                        hit.collider.enabled = false;
                                        doorObj.transform.Rotate(Vector3.up, 110, Space.World);
                                    }
                                    break;
                                case "exit":
                                    hit.collider.enabled = false;
                                    screenHandle.SetEnableArrow(false, false, false);
                                    inventory.gameObject.SetActive(false);
                                    StartCoroutine("ExitAnim");
                                    break;
                                default: break;
                            }
                        }
                    }
                }
            }
        }

    }

    IEnumerator ExitAnim()
    {
        Vector3 start = transform.position;
        Vector3 end = start + new Vector3(-7.9f, 0, 0);
        Vector3 velo = Vector3.zero;
        float time = 0;
        float animTime = 2.7f;
        float endPosX = -7.9f;
        while (transform.position.x > endPosX)
        {
            time += Time.deltaTime / animTime;
            transform.position = Vector3.Lerp(start, end, time);
            yield return null;
        }
        mainMenu.gameObject.SetActive(true);
        StartCoroutine("FadeInMainMenu");
    }

    IEnumerator FadeInMainMenu()
    {
        Color color = mainMenu.color;
        float start = 0;
        float end = 1;
        float time = 0;
        float fadeOutTime = 1.5f;
        while (color.a < 1.0f)
        {
            time += Time.deltaTime / fadeOutTime;
            color.a = Mathf.Lerp(start, end, time);
            mainMenu.color = color;
            yield return null;
        }
        mainMenu.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        StartCoroutine("FadeOutGameOverScreen");
    }

    IEnumerator FadeOutGameOverScreen()
    {
        Color color = gameOverScreen.color;
        float start = 1;
        float end = 0;
        float time = 0;
        float animTime = 2.2f;
        while (color.a > 0)
        {
            time += Time.deltaTime / animTime;
            color.a = Mathf.Lerp(start, end, time);
            gameOverScreen.color = color;
            yield return null;
        }
        gameOverScreen.gameObject.SetActive(false);
    }

    void ZoomIn(Collider col, Vector3 zoomPos, Vector3 zoomQua)
    {
        col.enabled = false;
        colInfo.Add(col);
        posInfo.Add(transform.position); // 사물 확대 전의 카메라 포지션 저장
        quaInfo.Add(transform.rotation); // 사물 확대 전의 카메라 회전각과 보는 방향 저장
        transform.position = zoomPos;
        transform.rotation = Quaternion.LookRotation(zoomQua);

        // (배열의 크기가 1일 때만) 처음 확대한 상태에서만 아래 이동 화살표 1회 활성화
        if (colInfo.Count == 1)
            screenHandle.SetEnableArrow(false, false, true);
    }

    // 아래 화살표 누르면 확대 전의 위치로 돌아감
    public void ZoomOut()
    {
        // 각 배열 끝에 저장된 값을 불러와 적용시키고 배열에서는 삭제
        colInfo[colInfo.Count - 1].enabled = true;
        transform.position = posInfo[posInfo.Count - 1];
        transform.rotation = quaInfo[quaInfo.Count - 1];
        colInfo.RemoveAt(colInfo.Count - 1);
        posInfo.RemoveAt(posInfo.Count - 1);
        quaInfo.RemoveAt(quaInfo.Count - 1);

        // (배열의 크기가 0일 때만) 확대를 다 빠져 나왔을 경우에만 좌우 이동 화살표 활성화
        if (colInfo.Count == 0)
        {
            screenHandle.SetDownArrowStatus(false);
            screenHandle.SetEnableArrow(true, true, false);
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }

        // 움직인 사물 원위치
        if (shelfPot1Moved)
        {
            GameObject.FindGameObjectWithTag("shelfpot1").transform.position += shelfPot1Dist;
            shelfPot1Moved = false;
        }
        if (shelfPot2Moved)
        {
            GameObject.FindGameObjectWithTag("shelfpot2").transform.position -= shelfPot2Dist;
            shelfPot2Moved = false;
        }
        if (isDrawer1Open)
        {
            GameObject.FindGameObjectWithTag("drawer1").transform.position += new Vector3(0.8f, 0, 0);
            isDrawer1Open = false;
        }
        if (isDrawer2Open)
        {
            GameObject.FindGameObjectWithTag("drawer2").transform.Rotate(Vector3.up, -123, Space.World);
            isDrawer2Open = false;
        }
        if (isCabinetOpen)
        {
            GameObject.FindGameObjectWithTag("cabinet").transform.GetChild(1).Rotate(new Vector3(1, 0, 0), -90, Space.Self);
            GameObject.FindGameObjectWithTag("cabinet").GetComponent<Collider>().enabled = true;
            isCabinetOpen = false;
        }
        if (isBookcaseDoorOpen)
        {
            GameObject.FindGameObjectWithTag("bc2doorLeft").transform.Rotate(Vector3.up, -90, Space.World);
            GameObject.FindGameObjectWithTag("bc2doorRight").transform.Rotate(Vector3.up, 90, Space.World);
            GameObject.FindGameObjectWithTag("bookcase2Doors").GetComponent<Collider>().enabled = true;
            GameObject.FindGameObjectWithTag("z_bookcase2_2").GetComponent<Collider>().enabled = true;
            isBookcaseDoorOpen = false;
        }

        // 바꾼 상태 원위치
        if (isColorPuzzleScene)
        {
            isColorPuzzleScene = false;
        }
        if (isNumberPadScene)
        {
            isNumberPadScene = false;
        }

    }

    public void TurnLeft()
    {
        // 기준으로 할 대상 지정 후 그 대상의 y축 기준으로 -90도 회전
        transform.RotateAround(Vector3.zero, Vector3.up, -90.0f);
    }

    public void TurnRight()
    {
        transform.RotateAround(Vector3.zero, Vector3.up, 90.0f);
    }

    public void SetItemType(string type)
    {
        itemType = type;
    }

    public string GetItemType()
    {
        return itemType;
    }

    void EatItem(GameObject inactiveTarget, string tag)
    {
        sfx.PlaySfx("itemEat");
        inactiveTarget.SetActive(false);
        inventory.AcquireItem(tag);
    }

    public void UseItem()
    {
        inventory.gameObject.transform.GetChild(2).Find("pf_" + itemType + "(Clone)").gameObject.SetActive(false);
        sfx.PlaySfx("itemUsed");
    }

    IEnumerator LaserOn()
    {
        // 데이터 설정하고 1초 멈춤
        LineRenderer lr = hit.transform.GetChild(1).GetComponent<LineRenderer>();
        screenHandle.gameObject.SetActive(false);
        wallShelf2.GetComponent<Collider>().enabled = false;
        yield return new WaitForSecondsRealtime(1f);
        
        // 레이저 쏘고 1초 멈춤
        lr.startWidth = 0.01f;
        lr.endWidth = 0.01f;
        lr.SetPosition(0, lr.transform.position);
        lr.SetPosition(1, wallButton.transform.GetChild(0).position);
        sfx.PlaySfx("laserRay");
        yield return new WaitForSecondsRealtime(1f);

        // 벽 버튼으로 화면 전환 후 0.5초 멈춤
        ZoomIn(wallButton.transform.parent.GetComponent<Collider>(), wallBtnAreaPos, wallBtnAreaQua);
        yield return new WaitForSecondsRealtime(0.5f);

        // 벽 버튼의 색 애니메이션 처리 후 1.5초 멈춤
        MeshRenderer mesh = wallButton.GetComponent<MeshRenderer>();
        Color color = mesh.materials[0].color;
        float startR = 94/255f;
        float endR = 1;
        float startG = 94/255f;
        float endG = 0;
        float startB = 94/255f;
        float endB = 0;
        float time = 0;
        float animTime = 1.75f;
        while (color.r < 1)
        {
            time += Time.deltaTime / animTime;
            color.r = Mathf.Lerp(startR, endR, time);
            color.g = Mathf.Lerp(startG, endG, time);
            color.b = Mathf.Lerp(startB, endB, time);
            mesh.materials[0].color = color;
            yield return null;
        }
        sfx.PlaySfx("wallBtnActive");
        yield return new WaitForSecondsRealtime(0.5f);
        lr.enabled = false;
        yield return new WaitForSecondsRealtime(1f);

        // 원래 화면으로 돌아오고 정상화
        ZoomOut();
        screenHandle.gameObject.SetActive(true);
        wallShelf2.GetComponent<Collider>().enabled = true;
        yield return new WaitForSecondsRealtime(1f);
        wallButton.GetComponent<Collider>().enabled = true;
    }

    IEnumerator HiddenDoorOn()
    {
        // 데이터 설정하고 1초 멈춤
        Collider wallHoleAreaCol = GameObject.FindGameObjectWithTag("wallHoleArea").GetComponent<Collider>();
        screenHandle.gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(1f);

        // 화면 전환하고 1초 멈춤
        ZoomIn(wallHoleAreaCol, wallHoleAreaPos, wallHoleAreaQua);
        yield return new WaitForSecondsRealtime(1f);

        // 문 열어 올리고 1초 멈춤
        float angle = 0;
        float start = 0;
        float end = 120;
        float time = 0;
        float animTime = 2;
        while (angle < end)
        {
            time += Time.deltaTime / animTime;
            angle = Mathf.Lerp(start, end, time);
            hiddenDoor.transform.Rotate(Vector3.forward, angle * Time.deltaTime, Space.World);
            yield return null;
        }
        yield return new WaitForSecondsRealtime(1f);

        // 화면 원위치 후 정상화
        ZoomOut();
        screenHandle.gameObject.SetActive(true);
        isPushed = true;
        GameObject.FindGameObjectWithTag("black_key").GetComponent<Collider>().enabled = true;
    }
}
