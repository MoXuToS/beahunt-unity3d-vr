using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class MenuInterfaceKeyboard : MonoBehaviour
{
    public Canvas MainInterface;
    private bool IsActiveBook = false;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)){
            QuestBookController();
        }
    }
    public void QuestBookController()
    {
        if (!IsActiveBook)
        {
            MainInterface.transform.Find("B").transform.Find("QuestBook").gameObject.SetActive(true);
            IsActiveBook = true;
        }
        else if(IsActiveBook)
        {
            MainInterface.transform.Find("B").transform.Find("QuestBook").gameObject.SetActive(false);
            IsActiveBook = false;
        }
    }
}
