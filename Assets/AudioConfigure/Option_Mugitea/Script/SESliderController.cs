using UnityEngine;
using UnityEngine.UI;
using Mugitea.Option;

public class SESliderController : MonoBehaviour
{

    private Slider slider;
    private bool is_start;

    private void Start()
    {
        is_start = true;

        slider = GetComponent<Slider>();

        slider.maxValue = VolumeManager.Instance.maxSEVolume - 80;
        slider.minValue = -80;

        slider.value = VolumeManager.Instance.GetSEVolume();

        is_start = false;
    }

    public void ValueChanged()
    {
        if (is_start) return;

        VolumeManager.Instance.ChangeSEVolume(slider.value);
    }
}
