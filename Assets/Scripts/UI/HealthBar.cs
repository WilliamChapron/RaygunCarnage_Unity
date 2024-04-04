using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update

    public Slider slider;


    private void Start()
    {
    }

    public void PosUpdate()
    {
        transform.LookAt(Camera.main.transform);
    }

    public void SetMaxHealth(int value)
    {
        slider.value = value;
        //Debug.Log("Health bar assigned: ");
    }
    public void SetHealth(int value)
    {
        slider.value = value;
    }
}
