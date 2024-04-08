using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace multi_tower_attack_3d
{
    public class UI : BaseSceneManager<UI>
    {
        public static UI instance;
        public Animator blimAnimator;

        public AudioSource clickAudio;
        public AudioSource countDownAudio;
        public AudioSource prizeSound;

        public ParticleSystem psGrow;
        ParticleSystem.MainModule psGrowMM;

        public GameObject fortuneSpinButtonRounded;

        public Color colorEnd;
        public Color colorStart;
        public Transform pointEnd;
        public Transform pointStart;

        public GameObject mainMenu;
        public GameObject levelObjct;


        public GameObject noadsButton;
        public GameObject gcButton;
        public GameObject restoreButton;
        public GameObject shopButton;
        public GameObject soundButton;
        public Animator skinsComingSoon;
        public GameObject reviveBlock;
        public GameObject revivePopUpPanel;
        public Text txtTimer;
        public Image filledImage;

        public GameObject prizeBGRounded;
        public Text prizeMoneyTextRounded;
        public GameObject rateusPanel;

        public GameObject SpinWheel;
        public GameObject spinWheelRounded;

        public GameObject TapToNextLevelPopUp;
        public GameObject LoadingPopUp;
        public GameObject settingObject;
        public static bool vibration = true;
        public GameObject HowToPlayPage;

        public Image whiteFillCircle;
        public Image redFillCircle;
        public GameObject fillCirclePanel;
        public ParticleSystem magicPoofParticle;
        public Image[] soundOnOff;
        public Image[] vibrateOnOff;

        int isActiveSettingObject;

        public void Awake()
        {
            instance = this;
            Physics.gravity = new Vector3(0, -120.0f, 0);
        }
        public void Start()
        {
          //  MatchmakingScript.instance.StartGameScene();
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Application.targetFrameRate = 60;

            if (PlayerPrefs.GetInt("_isStartFirstTimeHow") <= 1)
            {
                PlayerPrefs.SetInt("_isStartFirstTimeHow", PlayerPrefs.GetInt("_isStartFirstTimeHow") + 1);
                Base.Instance.PlayButtonClicked = false;
            }
            psGrowMM = this.psGrow.main;

            AudioListener.volume = (PlayerPrefs.HasKey("soundsOn_towerAttack") && (PlayerPrefs.GetInt("soundsOn_towerAttack") != 1)) ? 0f : 1f;
            bool flag = !PlayerPrefs.HasKey("soundsOn_towerAttack") || (PlayerPrefs.GetInt("soundsOn_towerAttack") == 1);
            flag = !flag;
            soundOnOff[0].enabled = !flag;
            soundOnOff[1].enabled = flag;

            vibrateOnOff[0].enabled = vibration;
            vibrateOnOff[1].enabled = !vibration;
        }
        public void Play()
        {
            this.mainMenu.SetActive(false);
            //this.levelObjct.SetActive (true);	
            BaseSceneManager<mc>.Instance.isGameStarted = true;
            Base.Instance.PlayButtonClicked = true;
           
        }
        public void VibrateBtn()
        {
            if (vibration)
            {
                vibration = false;
            }
            else
            {
                vibration = true;
            }
            vibrateOnOff[0].enabled = vibration;
            vibrateOnOff[1].enabled = !vibration;
        }

        public void Revive_Btn()
        {
            this.clickAudio.Play();
        }
        public void ReviveSuccessful_And_Level_NotFailed()
        {
            BaseSceneManager<mc>.Instance.ReviveSuccess();
        }
        public GameObject noThanksButton;
        public void EnableNoThanksButton()
        {
            noThanksButton.SetActive(true);
        }
        public void NoThanksBtn_Clicked()
        {
            noThanksButton.SetActive(false);
            BaseSceneManager<mc>.Instance.restartMenu.SetActive(true);
            revivePopUpPanel.SetActive(false);
        }
        public void ShowRevivePopUp()
        {
            BaseSceneManager<mc>.Instance.restartMenu.SetActive(true);               
            revivePopUpPanel.SetActive(false);
        }

        private IEnumerator reviveCoroutine()
        {
            float time = 5f;
            revivePopUpPanel.SetActive(true);
            BaseSceneManager<mc>.Instance.restartMenu.SetActive(false);
            this.mainMenu.SetActive(false);
            // this.countDownAudio.Play();
            while (time > 0f)
            {
                time -= Time.deltaTime;
                this.filledImage.fillAmount = time / 5f;
                int timer = Convert.ToInt32(time);
                if (timer >= 0)
                {
                    txtTimer.text = timer.ToString();
                }
                yield return null;
            }
            print("revive coroutine");
            revivePopUpPanel.SetActive(false);
            BaseSceneManager<mc>.Instance.restartMenu.SetActive(true);
            //this.countDownAudio.Stop();
        }
        public void SettingsClicked()
        {
            if (isActiveSettingObject == 0)
            {
                settingObject.SetActive(true);
                StartCoroutine(MoveObject(220.0f, 180));
                isActiveSettingObject = 1;
            }
            else
            {
                isActiveSettingObject = 0;
                StartCoroutine(MoveObject(-430.0f, -180));
            }
            this.clickAudio.Play();
        }

        IEnumerator MoveObject(float pos, float rot)
        {
            float scaleDuration = 0.17f; //animation duration in seconds       
            Vector3 targetPos = new Vector3(-10f, pos, 0);
            Vector3 targetEuler = new Vector3(0.0f, 0, rot);

            for (float t = 0; t < 1; t += Time.fixedDeltaTime / scaleDuration)
            {
                settingObject.GetComponent<RectTransform>().localPosition = Vector3.Lerp(settingObject.GetComponent<RectTransform>().localPosition, targetPos, t);
                yield return null;
            }
            yield return new WaitForSeconds(0f);
        }


        public void SoundsClicked()
        {
            bool flag = !PlayerPrefs.HasKey("soundsOn") || (PlayerPrefs.GetInt("soundsOn") == 1);
            flag = !flag;
            PlayerPrefs.SetInt("soundsOn", !flag ? 0 : 1);
            AudioListener.volume = !flag ? 0f : 1f;
            this.clickAudio.Play();
            UnityEngine.Debug.Log("flag" + flag);

            soundOnOff[0].enabled = flag;
            soundOnOff[1].enabled = !flag;
        }

        public void Spin()
        {
            base.StartCoroutine(this.SpinCoroutine());
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                float t = (BaseSceneManager<mc>.Instance.currentPlayformId * 1f) / ((float)(BaseSceneManager<Base>.Instance.platforms.Count - 1));
                BaseSceneManager<mc>.Instance.progression.fillAmount = t;
                BaseSceneManager<mc>.Instance.progression.color = (Color.Lerp(this.colorStart, this.colorEnd, t));
                this.pointStart.GetComponent<Image>().color = (Color.Lerp(this.colorStart, this.colorEnd, t));
                this.psGrowMM.startColor = Color.Lerp(this.colorStart, this.colorEnd, t);
                this.psGrow.transform.position = Vector3.Lerp(Camera.main.ScreenToWorldPoint(this.pointStart.position + new Vector3(0f, 0f, 1f)), Camera.main.ScreenToWorldPoint(this.pointEnd.position + new Vector3(0f, 0f, 1f)), t + 0.01f);

                if (BaseSceneManager<mc>.Instance.progression.fillAmount >= 1)
                {
                    this.pointEnd.GetComponent<Image>().color = (Color.Lerp(this.colorStart, this.colorEnd, t));
                }

            }

        }

        public void WheelofFortune()
        {
            this.SpinWheel.SetActive(true);
        }
        private IEnumerator SpinCoroutine()
        {
            yield return null;
        }
        public void ReviveSuccessful(bool success)
        {
            if (success)
            {
                //FB_AdManager.Instance.startAds = 0;
                this.reviveBlock.SetActive(false);
                BaseSceneManager<mc>.Instance.ReviveSuccess();
            }
        }
        public void RestorePurchases()
        {
            this.clickAudio.Play();
        }
        public void HowPlaySkip()
        {
            HowToPlayPage.SetActive(false);
        }
        public void BuyNoAds()
        {
            this.clickAudio.Play();
        }
        public void ShowGC()
        {
            Social.ShowLeaderboardUI();
            this.clickAudio.Play();
        }
    }
}
