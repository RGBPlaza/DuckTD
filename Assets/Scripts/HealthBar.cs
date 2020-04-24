using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider Slider;
    public Gradient Gradient;
    public Image Fill;
    public float slideSpeed = 25f;

    private float maxHP;
    private float sliderValue;

    public float MaxHP { get => maxHP; set { maxHP = value; Slider.maxValue = value; Slider.value = value; sliderValue = value; CurrentHP = value; } }
    public float CurrentHP { get; set; }

    private void Update()
    {
        sliderValue = Mathf.Lerp(sliderValue, CurrentHP, slideSpeed * Time.deltaTime);
        Slider.value = sliderValue;
        Fill.color = Gradient.Evaluate(sliderValue / MaxHP);

    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + Camera.main.transform.forward);
    }

}
