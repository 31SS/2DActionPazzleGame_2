using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string nextScene;
    private void Awake()
    {
        SceneManager.LoadScene(nextScene);
    }
}
