using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorObj : MonoBehaviour
{
    bool isOpen;

    private void Awake()
    {
        isOpen = false;
    }

    public void SetIsOpen(bool isOpen)
    {
        this.isOpen = isOpen;
    }

    public bool GetIsOpen()
    {
        return isOpen;
    }
}
