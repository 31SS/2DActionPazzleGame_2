namespace Mugitea.Option
{
    using UnityEngine;
    using UnityEngine.Audio;
    using System;

    public class VolumeManager : KanKikuchi.AudioManager.SingletonMonoBehaviour<VolumeManager>
    {
        [Header("PlayerPrefsに登録するKey")]
        public string bgmVolumeKey = "BGMVolumeKey";
        public string seVolumeKey = "SEVolumeKey";

        [Header("音量の初期値. 最大値に対する割合.")]
        [SerializeField, Range(0f, 100f)] private int defaultBGMVolume = 50;
        [SerializeField, Range(0f, 100f)] private int defaultSEVolume = 50;

        [Header("音量の最大値.")]
        [Range(1f, 100f)] public int maxBGMVolume = 100;
        [Range(1f, 100f)] public int maxSEVolume = 100;

        private float bgmVolume;
        private float seVolume;

        [Header("AudioMixerのプロパティの名前")]
        [NonSerialized] public readonly string bgmVolumeName = "BGMVolume";
        [NonSerialized] public readonly string seVolumeName = "SEVolume";
        [NonSerialized] public readonly string alertsVolumeName = "AlertsVolume";
        [NonSerialized] public readonly string uiVolumeName = "UIVolume";

        private AudioMixer audioMixer;
        [SerializeField] private AudioMixer exampleAudioMixer;

        [Header("Scene遷移時もオブジェクトを残すか")]
        [SerializeField] private bool dontDestroyOnLoad = true;

        private void Start()
        {
            audioMixer = Resources.Load("AudioMixer") as AudioMixer;
            // exampleAudioMixer = Resources.Load("Audio/ExampleAudioMixer") as AudioMixer;
            // Debug.Log(audioMixer);
            // Debug.Log(exampleAudioMixer);

            bgmVolume = PlayerPrefs.GetFloat(bgmVolumeKey, (maxBGMVolume * (float)defaultBGMVolume / 100) );
            seVolume = PlayerPrefs.GetFloat(seVolumeKey, (maxSEVolume * (float)defaultSEVolume / 100) );

            ChangeBGMVolume(bgmVolume);
            ChangeSEVolume(seVolume);

            if(dontDestroyOnLoad) DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// Slider用のゲッター. 値域は -80 ~ maxBGMVolume.
        /// </summary>
        /// <returns></returns>
        public float GetBGMVolume()
        {
            //初期画面にBGMSliderがある場合に初期値が強制的に100になるため、苦肉の策として...
            bgmVolume = PlayerPrefs.GetFloat(bgmVolumeKey, (maxBGMVolume * (float)defaultBGMVolume / 100) - 80);
            return bgmVolume;
        }

        /// <summary>
        /// Slider用のゲッター. 値域は -80 ~ maxSEVolume.
        /// </summary>
        /// <returns></returns>
        public float GetSEVolume()
        {
            //初期画面にSESliderがある場合に初期値が強制的に100になるため、苦肉の策として...
            seVolume = PlayerPrefs.GetFloat(seVolumeKey, (maxSEVolume * (float)defaultSEVolume / 100) - 80);
            return seVolume;
        }

        /// <summary>
        /// テキスト用のゲッター. 値域は 0 ~ 100. maxBGMVolumeに対するbgmVolumeの割合を返す.
        /// </summary>
        /// <returns></returns>
        public float GetBGMVolumeForText()
        {
            var nowVolume = (float)(bgmVolume + Mathf.Abs(-80));
            var maxVolume = (float)(Mathf.Abs(maxBGMVolume));

            return (float)Math.Truncate(CalcPercentage(nowVolume, maxVolume));
        }

        /// <summary>
        /// テキスト用のゲッター. 値域は 0 ~ 100. maxSEVolumeに対するseVolumeの割合を返す.
        /// </summary>
        /// <returns></returns>
        public float GetSEVolumeForText()
        {
            var nowVolume = (float)(seVolume + Mathf.Abs(-80));
            var maxVolume = (float)(Mathf.Abs(maxSEVolume));

            return (float)Math.Truncate(CalcPercentage(nowVolume, maxVolume));
        }

        /// <summary>
        /// "value" の値域は -80 ~ maxBGMVolume - 80
        /// </summary>
        /// <param name="value"></param>
        public void ChangeBGMVolume(float value)
        {
            if (value <= -80f) value = -80f;
            else if (maxBGMVolume - 80 <= value) value = maxBGMVolume - 80;
            

            PlayerPrefs.SetFloat(bgmVolumeKey, value);
            audioMixer.SetFloat(bgmVolumeName, value);
            bgmVolume = value;
        }

        /// <summary>
        /// "value" の値域は -80 ~ maxSEVolume - 80
        /// </summary>
        /// <param name="value"></param>
        public void ChangeSEVolume(float value)
        {
            if (value <= -80f) value = -80f;
            else if (maxSEVolume -80 <= value) value = maxSEVolume - 80;

            PlayerPrefs.SetFloat(seVolumeKey, value);
            audioMixer.SetFloat(seVolumeName, value);
            exampleAudioMixer.SetFloat(alertsVolumeName, value);
            exampleAudioMixer.SetFloat(uiVolumeName, value);
            seVolume = value;
        }

        private float CalcPercentage(float value1, float value2)
        {
            return (value1 / value2) * 100;
        }
    }
}