using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Griffin : NPC
{  
    // Добавить название спела
    AvadaCedavra AvadaCedavra = new AvadaCedavra();

    /// <summary>
    /// Отвечает за связь с движениями уничтожения
    /// </summary>
    public void DestroyNPC()
    {
        if(AvadaCedavra.CompareSpellPattern() == true)
            print("Manticore has destroyed");
    }
}