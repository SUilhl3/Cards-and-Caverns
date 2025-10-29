using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetHealth(int current, int max)
    {
        if (slider)
            slider.value = (float)current / max;
    }

}
