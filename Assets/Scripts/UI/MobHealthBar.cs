using UnityEngine;
using UnityEngine.UI;

public class MobHealthBar : MonoBehaviour
{
    public Slider slider;
    [SerializeField] private Vector3 offset;

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetCurrentHealth(float health)
    {
        slider.value = health;
    }

    void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }
}