using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Niffler : NPC
{  
    Brachiabindo brachiabindo = new Brachiabindo();

    /// <summary>
    /// Отвечает за связь с движениями уничтожения
    /// </summary>
    public void DestroyNPC()
    {
        if(brachiabindo.CompareSpellPattern() == true)
            print("Niffler has catched");
    }
}