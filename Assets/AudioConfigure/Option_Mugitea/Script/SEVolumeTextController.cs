using UnityEngine;
using UnityEngine.UI;
using Mugitea.Option;
using TMPro;

public class SEVolumeTextController : MonoBehaviour
{

    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        text.text = VolumeManager.Instance.GetSEVolumeForText().ToString();
    }
}
