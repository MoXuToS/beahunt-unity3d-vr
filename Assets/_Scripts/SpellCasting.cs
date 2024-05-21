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
    public void AddInput(Vector2 input)
    {
        playerInput.Add(input);
    }

    // Сравнение введенных пользователем движений с паттерном заклинания
    public bool CompareSpellPattern()
    {
        if (playerInput.Count < spellPattern.Count)
        {
            return false;
        }

        for (int i = 0; i < spellPattern.Count; i++)
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
    public AvadaCedavra()
    {
        spellPattern.Add(new Vector2(0, 0));
    }
}

public class Patronus : SpellCasting
{
    public Patronus()
    {
        spellPattern.Add(new Vector2(0, 0));
        spellPattern.Add(new Vector2(1, -1));
        spellPattern.Add(new Vector2(2, -2));
        spellPattern.Add(new Vector2(3, -3));
        spellPattern.Add(new Vector2(3, -4));
        spellPattern.Add(new Vector2(3, -5));
        spellPattern.Add(new Vector2(3, -6));
        spellPattern.Add(new Vector2(2, -7));
        spellPattern.Add(new Vector2(1, -8));
        spellPattern.Add(new Vector2(0, -8));
        spellPattern.Add(new Vector2(-1, -9));
        spellPattern.Add(new Vector2(-2, -9));
        spellPattern.Add(new Vector2(-3, -9));
        spellPattern.Add(new Vector2(-4, -8));
        spellPattern.Add(new Vector2(-5, -7));
        spellPattern.Add(new Vector2(-6, -6));
        spellPattern.Add(new Vector2(-6, -5));
        spellPattern.Add(new Vector2(-6, -4));
        spellPattern.Add(new Vector2(-6, -3));
        spellPattern.Add(new Vector2(-6, -2));
        spellPattern.Add(new Vector2(-5, -1));
        spellPattern.Add(new Vector2(-4, 0));
        spellPattern.Add(new Vector2(-3, 0));
        spellPattern.Add(new Vector2(-2, 0));
        spellPattern.Add(new Vector2(-1, 0));
        spellPattern.Add(new Vector2(0, -1));
        spellPattern.Add(new Vector2(1, -2));
        spellPattern.Add(new Vector2(2, -3));
        spellPattern.Add(new Vector2(2, -4));
    }
}

public class Brachiabindo : SpellCasting
{
    public Brachiabindo()
    {
        spellPattern.Add(new Vector2(0, 0));
        spellPattern.Add(new Vector2(1, 0));
        spellPattern.Add(new Vector2(2, 0));
        spellPattern.Add(new Vector2(3, 0));
        spellPattern.Add(new Vector2(4, 0));
        spellPattern.Add(new Vector2(5, 0));
        spellPattern.Add(new Vector2(6, 1));
        spellPattern.Add(new Vector2(7, 2));
        spellPattern.Add(new Vector2(8, 3));
        spellPattern.Add(new Vector2(7, 4));
        spellPattern.Add(new Vector2(6, 5));
        spellPattern.Add(new Vector2(5, 5));
        spellPattern.Add(new Vector2(4, 5));
        spellPattern.Add(new Vector2(3, 5));
        spellPattern.Add(new Vector2(2, 5));
        spellPattern.Add(new Vector2(1, 5));
        spellPattern.Add(new Vector2(0, 4));
        spellPattern.Add(new Vector2(0, 3));
        spellPattern.Add(new Vector2(0, 2));
        spellPattern.Add(new Vector2(1, 2));
        spellPattern.Add(new Vector2(1, 1));
        spellPattern.Add(new Vector2(2, 1));
        spellPattern.Add(new Vector2(3, 1));
        spellPattern.Add(new Vector2(4, 1));
        spellPattern.Add(new Vector2(5, 2));
        spellPattern.Add(new Vector2(5, 3));
        spellPattern.Add(new Vector2(4, 4));
        spellPattern.Add(new Vector2(3, 4));
        spellPattern.Add(new Vector2(2, 4));
        spellPattern.Add(new Vector2(2, 3));
        spellPattern.Add(new Vector2(2, 2));
        spellPattern.Add(new Vector2(3, 2));
        spellPattern.Add(new Vector2(4, 2));
        spellPattern.Add(new Vector2(4, 3));
        spellPattern.Add(new Vector2(4, 4));
        spellPattern.Add(new Vector2(4, 5));
        spellPattern.Add(new Vector2(4, 6));
        spellPattern.Add(new Vector2(4, 7));
        spellPattern.Add(new Vector2(4, 8));
    }
}