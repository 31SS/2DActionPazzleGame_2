using UnityEngine;
using UnityEngine.UI;
using Mugitea.Option;

public class BackButtonManager : MonoBehaviour
{

    private Image buttonImage;

    private Color defaultColor;
    [SerializeField] private Color pushedColor = new Color(0.6f, 0.6f, 0.6f, 1f);

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
        defaultColor = buttonImage.color;
    }

    public void BackButtonPointerDown()
    {
        buttonImage.color = pushedColor;
    }

    public void BackButtonPointerUp()
    {
        buttonImage.color = defaultColor;
        OptionManager.Instance.CloseOptionWindow();
    }
}
