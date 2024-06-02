using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dementor : NPC
{  
    // Добавить название спела
    Patronus patronus = new Patronus();

    /// <summary>
    /// Отвечает за связь с движениями уничтожения
    /// </summary>
    public void DestroyNPC()
    {
        if(patronus.CompareSpellPattern() == true)
            print("Dementor leave from us");
    }
}