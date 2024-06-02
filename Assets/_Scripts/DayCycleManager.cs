using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem.Controls;

public class DayCycleManager : MonoBehaviour
{
    //#0 - восход
    //#0.5 - закат
   //#0.5-1 - ночь

    [Range(0, 1)]
    public float TimeOfDay;//счЄтчик времени

    public float DayDuration = 30.0f;//врем€, сколько будет длитьс€ день

    private float SunIntensity;//€ркость света солнца
    private float MoonIntensity;//€ркость света луны

    public Material DaySkybox;//скайбокс дн€
    public Material NightSkybox;//скайбокс ночи

    public GameObject AudioManager;//управление музыкой
    public AudioClip DayMusic;//дневна€ музыка
    public AudioClip NightMusic;//ночна€ музыка

    public AnimationCurve TimeCurveSun;//график кривой, как будет измен€тьс€ €ркость света солнца
    public AnimationCurve TimeCurveMoon;//график кривой, как будет измен€тьс€ €ркость света луны
    public AnimationCurve TimeCurveSkybox;//график, как будет измен€тьс€ скайбокс в зависимости от дн€ и ночи


    public ParticleSystem stars;//система частиц дл€ звЄзд

    public Light Sun;//источник света солнце
    public Light Moon;//источник света луна

    private bool IsMusicDay = false;//переменна€, котора€ провер€ет состо€ние активности музыки дн€
    private bool IsMusicNight = false;//переменна€, котора€ провер€ет состо€ние активности музыки ночи
    private AudioSource component;//музыкальный трэк

    void Start()
    {
        SunIntensity = Sun.intensity;
        MoonIntensity = Moon.intensity;
        component = AudioManager.transform.Find("Music").GetComponent<AudioSource>();//найти ребЄнка родител€ в иерархии объекта
    }

    // Update is called once per frame
    void Update()
    {
        TimeOfDay += Time.deltaTime / DayDuration;//счЄтчик дн€
        if(TimeOfDay >= 1) { TimeOfDay -= 1; }//начать день по новой

        RenderSettings.skybox.Lerp(DaySkybox,NightSkybox, TimeCurveSkybox.Evaluate(TimeOfDay));//мен€ть skybox в зависимости от времени
        RenderSettings.sun = TimeCurveSkybox.Evaluate(TimeOfDay) >= 0.5f ? Moon: Sun;

        if (RenderSettings.sun == Sun)
        {
            if (IsMusicNight)
            {
                IsMusicNight = !IsMusicNight;
                component.Stop();
            }
            if (!IsMusicDay)
            {
                component.clip = DayMusic;
                IsMusicDay = true;
                component.Play();
            }
        }

        else if (RenderSettings.sun == Moon)
        {
            if(IsMusicDay)
            {
                IsMusicDay = !IsMusicDay;
                component.Stop();
            }
            if (!IsMusicNight) { 
                AudioSource component = AudioManager.transform.Find("Music").GetComponent<AudioSource>();
                component.clip = NightMusic;
                IsMusicNight = true;
                component.Play();
            }
        }
        DynamicGI.UpdateEnvironment();//обновить skybox

        var module = stars.main;
        module.startColor = new Color(1, 1, 1, 1 - TimeCurveSkybox.Evaluate(TimeOfDay));//прозрачность звЄзд

        Sun.transform.localRotation = Quaternion.Euler(TimeOfDay * 360.0f,180,0);//положение солнца
        Moon.transform.localRotation = Quaternion.Euler(TimeOfDay * 360.0f+180, 180, 0);//пооложение луны
        Sun.intensity = SunIntensity * TimeCurveSun.Evaluate(TimeOfDay);//€ркость солнца
        Moon.intensity = MoonIntensity * TimeCurveMoon.Evaluate(TimeOfDay);//€ркость луны
    }
}
