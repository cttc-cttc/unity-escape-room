using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    public AudioSource bgm;
    public AudioSource sfx_itemEat;
    public AudioSource sfx_itemUsed;
    public AudioSource sfx_lightSwitch;
    public AudioSource sfx_doorLock;
    public AudioSource sfx_drawerLock;
    public AudioSource sfx_cabinetLock;
    public AudioSource sfx_crumplingPaper;
    public AudioSource sfx_laserRay;
    public AudioSource sfx_wallBtnActive;
    public AudioSource sfx_wallBtnOn;

    private void Start()
    {
        //bgm.Play();
    }

    public void PlaySfx(string sfx)
    {
        switch (sfx)
        {
            case "itemEat":
                sfx_itemEat.Play();
                break;
            case "itemUsed":
                sfx_itemUsed.Play();
                break;
            case "lightSwitch":
                sfx_lightSwitch.Play();
                break;
            case "doorLock":
                sfx_doorLock.Play();
                break;
            case "drawerLock":
                sfx_drawerLock.Play();
                break;
            case "cabinetLock":
                sfx_cabinetLock.Play();
                break;
            case "crumplingPaper":
                sfx_crumplingPaper.Play();
                break;
            case "laserRay":
                sfx_laserRay.Play();
                break;
            case "wallBtnActive":
                sfx_wallBtnActive.Play();
                break;
            case "wallBtnOn":
                sfx_wallBtnOn.Play();
                break;
            default: break;
        }
    }
}
