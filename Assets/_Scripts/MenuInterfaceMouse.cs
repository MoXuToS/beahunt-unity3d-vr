using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuInterfaceMouse : MonoBehaviour
{
    public Button M;
    private bool BookIsActivated = false;
    public void QuestBookActivate()
    {
        if (!BookIsActivated)
        {
            M.transform.Find("QuestBook").gameObject.SetActive(true);
        }
    }

    public void QuestBookDeactivate()
    {
        if (BookIsActivated)
        {
            M.transform.Find("QuestBook").gameObject.SetActive(false);
        }
    }
}
