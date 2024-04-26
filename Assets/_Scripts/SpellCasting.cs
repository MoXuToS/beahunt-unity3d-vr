using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Спелы для ловли
/// </summary>
public class SpellCasting : MonoBehaviour
{
    public List<Vector2> spellPattern; // Паттерн заклинания - список точек в пространстве
    public const float threshold = 0.2f; // Пороговое значение для сравнения позиций
    private List<Vector2> playerInput = new List<Vector2>(); // Введенные пользователем движения

    // Добавление позиции движения пользователя
    public void AddInput(Vector3 input)
    {
        playerInput.Add(input);
    }

    // Сравнение введенных пользователем движений с паттерном заклинания
    public bool CompareSpellPattern()
    {
        if (playerInput.Count != spellPattern.Count)
        {
            return false;
        }

        for (int i = 0; i < playerInput.Count; i++)
        {
            if (Vector2.Distance(playerInput[i], spellPattern[i]) > threshold)
            {
                return false;
            }
        }

        return true;
    }
}

public class AvadaCedavra : SpellCasting
{
    // Здесь должна быть реализация создания переменной Spell pattern
    public AvadaCedavra()
    {
        spellPattern.Add(new Vector2(0, 0));
    }
}

public class Patronus : SpellCasting
{

}