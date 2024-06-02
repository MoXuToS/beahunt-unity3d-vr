using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
   public void StartButton()
   {
        SceneManager.LoadScene(2);
   }

    public void ExitButton()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
