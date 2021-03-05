using UnityEngine;
using UnityEngine.UI;
using Mugitea.Option;

public class BGMSliderController : MonoBehaviour
{

    private Slider slider;
    private bool is_start;

    private void Start()
    {
        is_start = true;

        slider = GetComponent<Slider>();

        slider.maxValue = VolumeManager.Instance.maxBGMVolume - 80;
        slider.minValue = -80;

        slider.value = VolumeManager.Instance.GetBGMVolume();

        is_start = false;
    }

    public void ValueChanged()
    {
        if (is_start) return;

        VolumeManager.Instance.ChangeBGMVolume(slider.value);
    }
}
