namespace Mugitea.Option
{
    using UnityEngine.SceneManagement;
    using UnityEngine;

    public class OptionManager : KanKikuchi.AudioManager.SingletonMonoBehaviour<OptionManager>
    {
        [Header("Scene遷移時もオブジェクトを残すか")]
        [SerializeField] private bool dontDestroyOnLoad = true;

        private void Start()
        {
            if(dontDestroyOnLoad) DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// Optionを開きます.
        /// </summary>
        public void OpenOptionWindow()
        {
            SceneManager.LoadScene("Option", LoadSceneMode.Additive);
        }

        /// <summary>
        /// Optionを閉じます.
        /// </summary>
        public void CloseOptionWindow()
        {
            SceneManager.UnloadSceneAsync("Option");
        }
    }
}