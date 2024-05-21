using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acromantula : NPC
{  
    // Добавить название спела
    AvadaCedavra AvadaCedavra = new AvadaCedavra();

    /// <summary>
    /// Отвечает за связь с движениями уничтожения
    /// </summary>
    public void DestroyNPC()
    {
        if(AvadaCedavra.CompareSpellPattern() == true)
            print("Acromantula has destroyed");
    }
}