using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem.Controls;

public class DayCycleManager : MonoBehaviour
{
    //#0 - ������
    //#0.5 - �����
   //#0.5-1 - ����

    [Range(0, 1)]
    public float TimeOfDay;//������� �������

    public float DayDuration = 30.0f;//�����, ������� ����� ������� ����

    private float SunIntensity;//������� ����� ������
    private float MoonIntensity;//������� ����� ����

    public Material DaySkybox;//�������� ���
    public Material NightSkybox;//�������� ����

    public GameObject AudioManager;//���������� �������
    public AudioClip DayMusic;//������� ������
    public AudioClip NightMusic;//������ ������

    public AnimationCurve TimeCurveSun;//������ ������, ��� ����� ���������� ������� ����� ������
    public AnimationCurve TimeCurveMoon;//������ ������, ��� ����� ���������� ������� ����� ����
    public AnimationCurve TimeCurveSkybox;//������, ��� ����� ���������� �������� � ����������� �� ��� � ����


    public ParticleSystem stars;//������� ������ ��� ����

    public Light Sun;//�������� ����� ������
    public Light Moon;//�������� ����� ����

    private bool IsMusicDay = false;//����������, ������� ��������� ��������� ���������� ������ ���
    private bool IsMusicNight = false;//����������, ������� ��������� ��������� ���������� ������ ����
    private AudioSource component;//����������� ����

    void Start()
    {
        SunIntensity = Sun.intensity;
        MoonIntensity = Moon.intensity;
        component = AudioManager.transform.Find("Music").GetComponent<AudioSource>();//����� ������ �������� � �������� �������
    }

    // Update is called once per frame
    void Update()
    {
        TimeOfDay += Time.deltaTime / DayDuration;//������� ���
        if(TimeOfDay >= 1) { TimeOfDay -= 1; }//������ ���� �� �����

        RenderSettings.skybox.Lerp(DaySkybox,NightSkybox, TimeCurveSkybox.Evaluate(TimeOfDay));//������ skybox � ����������� �� �������
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
        DynamicGI.UpdateEnvironment();//�������� skybox

        var module = stars.main;
        module.startColor = new Color(1, 1, 1, 1 - TimeCurveSkybox.Evaluate(TimeOfDay));//������������ ����

        Sun.transform.localRotation = Quaternion.Euler(TimeOfDay * 360.0f,180,0);//��������� ������
        Moon.transform.localRotation = Quaternion.Euler(TimeOfDay * 360.0f+180, 180, 0);//���������� ����
        Sun.intensity = SunIntensity * TimeCurveSun.Evaluate(TimeOfDay);//������� ������
        Moon.intensity = MoonIntensity * TimeCurveMoon.Evaluate(TimeOfDay);//������� ����
    }
}
