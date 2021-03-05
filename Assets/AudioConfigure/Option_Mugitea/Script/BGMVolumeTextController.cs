using UnityEngine;
using UnityEngine.UI;
using Mugitea.Option;
using TMPro;

public class BGMVolumeTextController : MonoBehaviour
{

    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        text.text = VolumeManager.Instance.GetBGMVolumeForText().ToString();
    }
}
