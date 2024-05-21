using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR.Interaction.Toolkit;

public class ForestRainController : MonoBehaviour
{
    private ParticleSystem _ps;
    public GameObject RainCloud;
    private bool IsRain = false;
    private bool IsMoving = false;
    private Vector3 moveDirection;
    private float TimeMoving;
    private float TimeStartUp;

    private float duration;
    public float delay = 3;
    public float TimeOfMovingDirection = 0;
    public AnimationCurve RateMoving;
    private bool IsFixed = false;
    public float value_func;

    [Range(1.0f,40.0f)]
    public float speed = 20.0f;

    private void Start()
    {
        // Запуск в реальном времени (игра)
        _ps = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (!IsMoving)
        {
            StartCoroutine(ActivateRain());
        }
        Move();
    }

    private void Move()
    {

        if (Time.realtimeSinceStartup - TimeStartUp < TimeMoving)
        {
            //анимация на движение в зависимости от текущего момента времени движения
            TimeOfMovingDirection += Time.deltaTime / TimeMoving;
            if(TimeOfMovingDirection >= 1) { TimeOfMovingDirection -= 1; }
            value_func = RateMoving.Evaluate(TimeOfMovingDirection);
            int number = (int)(RateMoving.Evaluate(TimeOfMovingDirection)*10);
            moveDirection = GetRandomTranslateVector3(number);
            transform.Translate(moveDirection * speed * Time.deltaTime);
           transform.position = new Vector3(Clamp(transform.position.x, 0, 500), transform.position.y, Clamp(transform.position.z, 0, 500));
           RainCloud.transform.position = transform.position;
        }
        else
        {
            IsMoving = false;
        }
    }

    private IEnumerator ActivateRain()
    {

        
        WaitForSecondsRealtime waiting = new WaitForSecondsRealtime(delay);
        duration = Random.Range(20, 40);
        if (!IsRain)
        {
            
            IsMoving = true;
            TimeMoving = duration;
            TimeStartUp = Time.realtimeSinceStartup;//количество реальных секунд в данный момент времени
            
            _ps.Play();
            IsRain = true;
        }
        else
        {
            _ps.Stop();
            yield return waiting;//ждать и выйти, а когда закончится ожидание,сделать IsRain = false
            IsRain = false;
            
        }
        
    }

   

    private Vector3 GetRandomTranslateVector3(int number)
    {
        int random_number = Random.Range(number, 8-number);
        Vector3 translation = new Vector3();
        
        switch (number)
        {
            case 1:
                {
                    translation = Vector3.right;
                    break;
                }
            case 2:
                {
                    translation = Vector3.up;
                    break;
                }
            case 3:
                {
                    translation = Vector3.left;
                    break;
                }
            case 4:
                {
                    translation = Vector3.down;
                    break;
                }
            case 5:
                {
                    translation = Vector3.right*(-1);
                    break;
                }
            case 6:
                {
                    translation = Vector3.down*(-1);
                    break;
                }
            case 7:
                {
                    translation = Vector3.left*(-1);
                    break;
                }
            case 8:
                {
                    translation = Vector3.up*(-1);
                    break;
                }

        }
        return translation;
    }

    private float Clamp(float value, float min, float max) { 
        if(value < min)
        {
            return min;
        }
        if(value > max)
        {
            return max;
        }
        return value;
    }
}
