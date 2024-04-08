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
    public class mc : BaseSceneManager<mc>
    {
        private string[] versionNames = new string[] { "Base", "WorldBuilder", "ShorterLevels", "Animations" };
        private bool setUpvelocity;

        public GameType gameId;
        public int currentObjectLevel;
        public bool isActive = true;
        public bool isGameStarted;
        private int sessionsCount;
        public int currentPlayformId;
        public Transform currentPlatform;

        //
        private static int score = 0;
        public int scoreInRow = 1;
        public int scoreNow = 3;


        public GameObject AddMoneyRestart;
        public Text AddMoneyRestartText;
        //
        public Animator anim;
        public GameObject AnimatedButton;//
        public Text best;
        public Text yourScore;
        public GameObject bestUI;
        //
        public GameObject canvas;
        //
        public int currency;
        public List<int> currencyList;
        public Text currencyText;
        public GameObject currencyUI;


        public List<GameObject> objects;
        public float startDrag;
        public float finalDrag;
        // public PhysicMaterial mat; 
        public Material mcMat;


        public AudioSource AddMoneyRestartSound;
        public AudioSource deathAudio;
        public AudioSource passAudio;
        public AudioSource splashAudio;
        public AudioSource winLevelAudio;


        public ParticleSystem psExtraSplash;
        public ParticleSystem psBurn;
        public ParticleSystem psBurn1;
        public ParticleSystem psSplash;
        public ParticleSystem ballExploid;


        public List<GameObject> decal;

        [Header("Prefab")]

        // public GameObject plusAwesomePrefab; 

        // public GameObject plusPrefab; 


        public Text newRecord;
        public Text scoreNowText;
        public Text scoreText;


        public Text levelFrom;
        public Text levelTo;
        public Image LevelPassedPin;
        public GameObject levelUpButton;
        public Text levelUpText;

        public Image progression;

        public GameObject psPickup;


        public GameObject restartMenu;
        public Text restartPercentage;
        private bool reviveShown;


        public GameObject finishPrefab;
        public GameObject winMenu;
        public Text levelText;
        public Text levelNotextFail;
        public Text scoreTextLevelCompl;
        public Text bestScoreTextLevelCompl;

        public GameObject particle2;
        public int tempScore;
        public bool _isFirePowerOn;
        public GameObject ball;
        static bool _isAdsChanged = true;
        bool isNormalCollider;
        int magicPoofCount;

        private void Start()
        {
            //ball.GetComponent<Rigidbody>().isKinematic = true;
            this.currentObjectLevel = PlayerPrefs.GetInt("currentObjectLevel_towerAttack");
            //this.currentObjectLevel = UnityEngine.Random.Range(800,1000);

            this.currentPlatform = null;
            this.best.text = "best: " + PlayerPrefs.GetInt("bestScore_towerAttack", 0);
            this.levelFrom.text = (Base.currentLevel + 1).ToString();
            this.levelTo.text = (Base.currentLevel + 2).ToString();
            MultiplayerManagement_AI.instance.ScoreUpdate_UserA(multi_tower_attack_3d.GLOBALS_StackBall.levelSkinChange);
        }

        void Update()
        {
            this.scoreText.text = GLOBALS_StackBall.scoreNew.ToString();
            
            ball.transform.eulerAngles = new Vector3(0, 0, 0);
            base.GetComponent<Rigidbody>().drag = Mathf.Lerp(this.startDrag, this.finalDrag, (this.currentPlayformId * 1f) / ((float)(BaseSceneManager<Base>.Instance.platforms.Count - 1)));
            if (BaseSceneManager<Base>.Instance.platforms[this.currentPlayformId].tag != ("Platform"))
            {
                if (base.transform.position.y < BaseSceneManager<Base>.Instance.platforms[this.currentPlayformId].transform.position.y)
                {
                    this.passAudio.pitch = 1f + ((this.scoreInRow * 0.3f) / ((float)(Base.currentLevel + 1)));
                    this.passAudio.Play();

                    base.StartCoroutine(this.destroyLayerCoroutine(BaseSceneManager<Base>.Instance.platforms[this.currentPlayformId]));
                    if ((this.currentPlatform == null) || (this.currentPlatform.transform.parent != BaseSceneManager<Base>.Instance.platforms[this.currentPlayformId].transform))
                    {
                        this.currentPlayformId++;
                        score += this.scoreInRow;
                        this.scoreInRow += Base.currentLevel + 1;
                    }
                    else
                    {
                        this.currentPlayformId++;
                        this.scoreInRow = Base.currentLevel + 1;
                        score += this.scoreInRow;
                    }
                    this.currentPlatform = null;
                }
            }
           //print("tempScore "+ tempScore);

            if (tempScore > 5)
            {
                WhiteFillingStart();
            }
            if (_isFirePowerOn)
            {
                RedFillingDecrease();
            }
            if (tempScore == 0 && !_isFirePowerOn)
            {
                UI.instance.whiteFillCircle.fillAmount = 0;
                UI.instance.whiteFillCircle.color = Color.white;
                UI.instance.fillCirclePanel.SetActive(false);
            }
            if (UI.instance.whiteFillCircle.fillAmount == 0 && UI.instance.redFillCircle.fillAmount == 1)
            {
                UI.instance.fillCirclePanel.SetActive(false);
            }
            if (UI.instance.TapToNextLevelPopUp.activeInHierarchy)
            {
                UI.instance.fillCirclePanel.SetActive(false);
                particle2.SetActive(false);
            }
        }

        public void WhiteFillingStart()
        {
            UnityEngine.Debug.Log("WhiteFillingStart");
            UI.instance.fillCirclePanel.SetActive(true);
            UI.instance.whiteFillCircle.fillAmount += 0.02f;
            UI.instance.whiteFillCircle.color = Color.white;
            UI.instance.redFillCircle.gameObject.SetActive(false);
            UI.instance.whiteFillCircle.gameObject.SetActive(true);

            if (UI.instance.whiteFillCircle.fillAmount >= 1f)
            {
                UI.instance.whiteFillCircle.gameObject.SetActive(false);
                UI.instance.redFillCircle.gameObject.SetActive(true); // red circle
                particle2.SetActive(true);
                _isFirePowerOn = true;
            }
        }
        public void RedFillingDecrease()
        {
            //UnityEngine.Debug.Log("RedFillingDecrease");
            if (UI.instance.redFillCircle.fillAmount > 0)
            {
                UI.instance.redFillCircle.fillAmount -= 0.01f;
            }
            else
            {
                UI.instance.redFillCircle.fillAmount = 0;
                UI.instance.redFillCircle.gameObject.SetActive(false); // red circle
                UI.instance.fillCirclePanel.SetActive(false);
                particle2.SetActive(false);
                _isFirePowerOn = false;
                UI.instance.redFillCircle.fillAmount = 1;
                UI.instance.whiteFillCircle.fillAmount = 0;
                tempScore = 0;
            }
        }
        public void TapToPlayNextLevels()
        {
            // FB_AdManager.instance.ShowInterstitial(); 
           
            GLOBALS_StackBall.levelSkinChange++;           
            UI.instance.fillCirclePanel.SetActive(false);
            UI.instance.LoadingPopUp.SetActive(true);           
            this.Win();
        }
        public void Win()
        {
            if (this.gameId == GameType.GAME_ANIM)
            {
                BaseSceneManager<MainCamera>.Instance.GoUp();
            }
            base.StartCoroutine(this.NextLevel());
        }

        public void Fail()
        {
            UI.instance.fillCirclePanel.SetActive(false);
            this.scoreInRow = 0;

            this.isActive = false;
            base.GetComponent<Rigidbody>().isKinematic = true;

            this.deathAudio.Play();
            base.transform.localScale = new Vector3(2.3f, 1.7f, 2.3f);
            levelNotextFail.text = ((Base.currentLevel + 1)).ToString();
            this.restartPercentage.text = ((int)((BaseSceneManager<mc>.Instance.currentPlayformId * 100f) / ((float)(BaseSceneManager<Base>.Instance.platforms.Count - 1)))).ToString("D") + "% Completed!";

            MultiplayerManagement_AI.instance.GameOver();
             /*if (!this.reviveShown)
             {
                 //this.reviveShown = true;
                 BaseSceneManager<UI>.Instance.ShowRevivePopUp();
             }
             else
             {
                 this.restartMenu.SetActive(true);
             }*/
            this.ReportScore((float)score);

            if (PlayerPrefs.GetInt("bestScore_towerAttack") < GLOBALS_StackBall.scoreNew)
            {
                this.newRecord.gameObject.SetActive(true);
                PlayerPrefs.SetInt("bestScore_towerAttack", GLOBALS_StackBall.scoreNew);
                this.newRecord.text = PlayerPrefs.GetInt("bestScore_towerAttack").ToString();
                this.yourScore.text = GLOBALS_StackBall.scoreNew.ToString();
            }
            else
            {
                this.newRecord.text = PlayerPrefs.GetInt("bestScore_towerAttack").ToString();
                this.yourScore.text = GLOBALS_StackBall.scoreNew.ToString();
            }
        }

        public void ForceDestroy(Transform platform)
        {
            this.psExtraSplash.Play();

            score += this.scoreInRow;
            for (int i = 0; i < BaseSceneManager<Base>.Instance.platforms[this.currentPlayformId].transform.childCount; i++)
            {
                //            BaseSceneManager<Base>.Instance.platforms[this.currentPlayformId].transform.GetChild(i).GetComponent<MeshRenderer>().material = this.mcMat;
            }
            base.StartCoroutine(this.destroyLayerCoroutine(BaseSceneManager<Base>.Instance.platforms[this.currentPlayformId]));

            this.splashAudio.Play();
            this.setUpvelocity = true;
            this.anim.Play("Base Layer.Splash", 0, 0f);
            this.currentPlayformId++;
        }

        private void LateUpdate()
        {
            if (this.setUpvelocity)
            {
                base.GetComponent<Rigidbody>().velocity = new Vector3(0f, 40f, 0f);
                this.setUpvelocity = false;
            }
        }

        public void LevelUp()
        {
            if (this.currency >= this.currencyList[this.currentObjectLevel])
            {
                if (this.currentObjectLevel >= this.objects.Count)
                {

                    // BaseSceneManager<UI>.Instance.ShopClicked();
                }
                else
                {
                    this.currentObjectLevel++;
                   // PlayerPrefs.SetInt("currentObjectLevel_towerAttack", this.currentObjectLevel);
                    this.UpdateObjects();
                    if (this.objects[this.currentObjectLevel - 1].GetComponent<Animator>() != null)
                    {
                        this.objects[this.currentObjectLevel - 1].GetComponent<Animator>().Play("Base Layer.appear", 0, 0f);
                    }
                }
            }
        }


        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.tag == "Normal")
            {
                isNormalCollider = true;
                if (UI.vibration == true)
                {
                    Vibration.Vibrate(15);
                }
            }
            if (other.collider.tag == "Finish")
            {
                UnityEngine.Debug.Log("Finish OnCollisionEnter");
                UI.instance.fillCirclePanel.SetActive(false);
                isNormalCollider = false;
                this.scoreInRow = 0;

               // this.levelText.text = "" + ((Base.levelSkinChange + 1)).ToString();

                scoreTextLevelCompl.text = GLOBALS_StackBall.scoreNew.ToString();
                if (PlayerPrefs.GetInt("bestScore_towerAttack") < GLOBALS_StackBall.scoreNew)
                {
                    PlayerPrefs.SetInt("bestScore_towerAttack", GLOBALS_StackBall.scoreNew);
                }
                bestScoreTextLevelCompl.text = PlayerPrefs.GetInt("bestScore_towerAttack", 0).ToString();

                this.GetComponent<Collider>().isTrigger = false;
                // UI.instance.TapToNextLevelPopUp.SetActive(true);
                base.GetComponent<Rigidbody>().velocity = new Vector3(0f, 40f, 0f);
                particle2.SetActive(false);
                if (magicPoofCount < 1)
                {
                    UnityEngine.Debug.Log("magicPoofCount");

                    //winLevelAudio.Play();
                    magicPoofCount = 1;
                    GameObject particleGO = Instantiate(UI.instance.magicPoofParticle.gameObject);
                    particleGO.transform.SetParent(GameObject.FindGameObjectWithTag("Platform").transform);
                    particleGO.transform.localPosition = Vector3.zero;
                    particleGO.SetActive(true);
                    particleGO.GetComponent<ParticleSystem>().Play();
                    UI.instance.TapToNextLevelPopUp.SetActive(true);
                    levelText.text = "" + ((GLOBALS_StackBall.levelSkinChange)).ToString() + "";
                    Invoke("TapToPlayNextLevels", 0.1f);
                    // WalletBalanceManager.instance.CheckTotalInterImpr();
                }

            }
            else if (this.currentPlatform != other.collider.transform)
            {
                if (UI.vibration == true)
                {
                    Vibration.Vibrate(15);
                }
                this.currentPlatform = other.collider.transform;
                if (this.scoreInRow > ((Base.currentLevel + 1) * 3))
                {

                    this.ForceDestroy(other.collider.transform.parent);
                    this.scoreInRow = Base.currentLevel + 1;
                }
                else
                {
                    this.scoreInRow = Base.currentLevel + 1;
                    if (this.currentPlatform.transform.childCount < 3)
                    {
                        GameObject decalObj = UnityEngine.Object.Instantiate<GameObject>(this.decal[UnityEngine.Random.Range(0, this.decal.Count)], new Vector3(base.transform.position.x, this.currentPlatform.transform.position.y + 0.9f, base.transform.position.z), Quaternion.identity, this.currentPlatform);
                        decalObj.transform.localScale = new Vector3(1f, 1f, 1f);
                        decalObj.transform.localRotation = Quaternion.Euler(new Vector3(90f, 0f, (float)UnityEngine.Random.Range(-180, 180)));
                    }

                    this.splashAudio.Play();
                    this.setUpvelocity = true;
                    this.anim.Play("Base Layer.Splash", 0, 0f);
                }
            }
            else
            {
                if (UI.vibration == true)
                {
                    Vibration.Vibrate(15);
                }
                this.scoreInRow = Base.currentLevel + 1;
                if (this.currentPlatform.transform.childCount < 3)
                {
                    GameObject decalObj = UnityEngine.Object.Instantiate<GameObject>(this.decal[UnityEngine.Random.Range(0, this.decal.Count)], new Vector3(base.transform.position.x, this.currentPlatform.transform.position.y + 0.9f, base.transform.position.z), Quaternion.identity, this.currentPlatform);
                    decalObj.transform.localScale = new Vector3(1f, 1f, 1f);
                    decalObj.transform.localRotation = Quaternion.Euler(new Vector3(90f, 0f, (float)UnityEngine.Random.Range(-180, 180)));
                }

                this.setUpvelocity = true;
                this.anim.Play("Base Layer.Splash", 0, 0f);
                if (this.scoreNow == 0)
                {
                    this.Fail();
                }
                else
                {
                    this.splashAudio.Play();
                }
            }
            this.psSplash.Play();

        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Normal")
            {
                isNormalCollider = false;
                if (Base.Instance.isMouseDown == 0)
                {
                    Base.Instance.TriggerFalseWait();
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {

            if (other.tag == "Fail")
            {
                UI.instance.fillCirclePanel.SetActive(false);
                if (Base.Instance.isMouseDown == 0)
                {
                    Base.Instance.TriggerFalseWait();
                }
            }
            if (other.tag == "Fail" && !_isFirePowerOn)
            {
                this.Fail();
            }

            if (other.tag == "Finish")
            {
                UnityEngine.Debug.Log("Finish OnTriggerEnter");

                if (magicPoofCount < 1)
                {
                    //winLevelAudio.Play();
                    magicPoofCount = 1;
                    GameObject particleGO = Instantiate(UI.instance.magicPoofParticle.gameObject);
                    particleGO.transform.SetParent(GameObject.FindGameObjectWithTag("Platform").transform);
                    particleGO.transform.localPosition = Vector3.zero;
                    particleGO.SetActive(true);
                    particleGO.GetComponent<ParticleSystem>().Play();
                    UI.instance.TapToNextLevelPopUp.SetActive(true);
                    levelText.text = "" + ((GLOBALS_StackBall.levelSkinChange)).ToString() + "";
                    Invoke("TapToPlayNextLevels",0.1f);
                   // WalletBalanceManager.instance.CheckTotalInterImpr();
                }
                UI.instance.fillCirclePanel.SetActive(false);
                Base.Instance.isGameover = true;
                transform.gameObject.GetComponent<Collider>().isTrigger = false;
                transform.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                transform.localPosition = new Vector3(transform.localPosition.x, other.transform.parent.localPosition.y + 3, transform.localPosition.z);
                base.GetComponent<Rigidbody>().velocity = new Vector3(0f, 40f, 0f);
                particle2.SetActive(false);
            }
        }

        public void Restart()
        {
            base.StartCoroutine(this.AddMoneyRestartCoroutine());
        }
        
        public void ResetValueOfFillCircle()
        {
            UI.instance.redFillCircle.gameObject.SetActive(false); // red circle
            UI.instance.fillCirclePanel.SetActive(false);
            particle2.SetActive(false);
            _isFirePowerOn = false;
            UI.instance.redFillCircle.fillAmount = 1;
            UI.instance.whiteFillCircle.fillAmount = 0;
            tempScore = 0;
        }

        public void ReviveSuccess()
        {
            UI.instance.reviveBlock.SetActive(false);
            UI.instance.revivePopUpPanel.SetActive(false);
            UI.instance.countDownAudio.Stop();
            ResetValueOfFillCircle();

            base.transform.localScale = new Vector3(1.816f, 1.816f, 1.816f);
            this.isActive = true;
            base.GetComponent<Rigidbody>().isKinematic = false;
            this.restartMenu.SetActive(false);
            if (currentPlayformId > 0)
            {
                this.currentPlayformId--;
            }
            Transform transform = base.transform;
            transform.position += new Vector3(0f, 20f, 0f);
            if (currentPlayformId > 0)
            {
                BaseSceneManager<Base>.Instance.Revive(this.currentPlayformId);
            }
            Transform transform2 = Camera.main.transform;
            transform2.position += new Vector3(0f, 20f, 0f);
        }



        /// <summary>
        ///  destroy animations
        /// </summary>
        /// <param name="layer"></param>
        /// <returns></returns>
        private IEnumerator destroyLayerCoroutine(GameObject layer)
        {
            print("tempScore " + tempScore);
            if (!_isFirePowerOn)
            {
                tempScore += 1;
            }
            GLOBALS_StackBall.scoreNew += 1;
           // MultiplayerManagement_AI.instance.ScoreUpdate_UserA(GLOBALS_StackBall.scoreNew);
            List<Transform> objectList = new List<Transform>();
            float speed = 0f;
            float time = 0f;
            if (this.gameId == GameType.GAME_ANIM)
            {
                //    BaseSceneManager<UI>.Instance.psGrow.Play();
                //    BaseSceneManager<UI>.Instance.blimAnimator.Play("Base Layer.Blim", 0, 0f);
            }
            if (layer.GetComponent<Animator>() != null)
            {
                layer.GetComponent<Animator>().enabled = false;
            }

            for (int j = 0; j < layer.transform.childCount; j++)
            {
                objectList.Add(layer.transform.GetChild(j));
            }

            for (int k = 0; k < objectList.Count; k++)
            {
                objectList[k].parent = null;
            }

            while (time < 0.3f)
            {
                time += Time.deltaTime;
                speed -= Time.deltaTime * Physics.gravity.y;

                for (int m = 0; m < objectList.Count; m++)
                {
                    Transform objectItem = objectList[m];
                    objectItem.transform.Translate(new Vector3(-1.5f, 2f, 0));
                    objectItem.transform.Rotate((float)(Time.deltaTime), Time.deltaTime, (float)(20 * Time.deltaTime));
                    //Vector3 vector2 = (Vector3)((objectItem.up* Time.smoothDeltaTime)* 300f);
                    //objectItem.position += new Vector3( vector2.x* speed, vector2.y *Time.smoothDeltaTime *speed , vector2.z *speed );
                    //objectItem.Rotate((float)(90 *Time.deltaTime), Time.deltaTime, (float)(30* Time.deltaTime));

                }
                yield return null;
            }

            for (int i = 0; i < objectList.Count; i++)
            {
                Destroy(objectList[i].gameObject);
            }

            layer.SetActive(false);
        }


        private IEnumerator NextLevel()
        {
            int i = 0;
            Dictionary<string, object> properties = new Dictionary<string, object>();
            float ypos = this.transform.position.y;
            properties["Level"] = Base.currentLevel;
            UI.instance.magicPoofParticle.gameObject.SetActive(false);


            if (this.gameId != GameType.GAME_ANIM)
            {
                this.winMenu.SetActive(true);
                yield return new WaitForSeconds(1.5f);
            }

            Base.currentLevel++;
           // PlayerPrefs.SetInt("currentLevel", Base.currentLevel);

            if (this.gameId != GameType.GAME_WORLD)
            {
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
            }
            else
            {
                BaseSceneManager<UI>.Instance.WheelofFortune();
            }
        }
        public void AddMoney(int money)
        {
            this.currency += money;
            PlayerPrefs.SetInt("currency", this.currency);
        }
        private void AuthCallback(bool success)
        {
            UnityEngine.Debug.Log("Authentication finished: " + success.ToString());
        }
        /* public void CallInterstiatialADS()
        {
            FB_AdManager.countAds++;
            if (FB_AdManager.countAds % FB_AdManager.Instance.interval_Interstitial_ads == 0)
            {
                FB_AdManager.admob_fbads_counter++;
                if (FB_AdManager.admob_fbads_counter % FB_AdManager.Instance.interstitial_ads_swipe == 0)
                {
                    if (_isAdsChanged == true)
                    {
                        _isAdsChanged = false;
                    }
                    else
                    {
                        _isAdsChanged = true;

                    }
                }
                if (_isAdsChanged)
                {
                    GoogleMobileAdsDemoScript.Instance.ShowInterstitial();
                }
                else
                {
                    FB_AdManager.Instance.ShowInterstitial_FB();

                }
            }
        }*/
        private void InitCallback()
        {
            //if (FB.IsInitialized)
            //{
            //    FB.ActivateApp();
            //}
            //else
            //{
            //    UnityEngine.Debug.Log("Failed to Initialize the Facebook SDK");
            //}
        }
        public void PostCallback(bool success)
        {
            UnityEngine.Debug.Log("Post finished: " + success.ToString());
        }
        private void ReportScore(float score)
        {
            UnityEngine.Debug.Log("Report Score: " + score.ToString());
            if (!Application.isEditor)
            {
            }
        }
        public void SocialAuthenticate()
        {
            if (!Application.isEditor && !Social.localUser.authenticated)
            {
                Social.localUser.Authenticate(new Action<bool>(this.AuthCallback));
            }
        }
        public void UpdateObjects()
        {
            for (int i = 0; i < this.objects.Count; i++)
            {
                if (i < this.currentObjectLevel)
                {
                    this.objects[i].SetActive(true);
                }
            }
            if (this.currency >= this.currencyList[this.currentObjectLevel])
            {
                this.AnimatedButton.SetActive(true);
            }
            else
            {
                this.AnimatedButton.SetActive(false);
            }
            this.levelUpText.text = this.currencyList[this.currentObjectLevel].ToString();
        }

        private IEnumerator AddMoneyRestartCoroutine()
        {
           // Base.levelSkinChange++;
            yield return new WaitForSeconds(0.2f);
            score = 0;
            GLOBALS_StackBall.scoreNew = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        private IEnumerator plusCoroutine(int plusScore)
        {
            /* GameObject plusObj = null;
             GameObject plusAwesomeObj = null;
             plusObj = UnityEngine.Object.Instantiate<GameObject>(this.plusPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity, this.canvas.transform);
             plusObj.GetComponent<Animator>().Play("Base Layer.plus", 0, 0f);
             plusObj.transform.GetChild(0).GetComponent<Text>().text = "+" + plusScore.ToString();
             if ((this.scoreInRow == ((Base.currentLevel + 1) * 3)) || (this.scoreInRow == ((Base.currentLevel + 1) * 5)))
             {
                 //plusAwesomeObj = UnityEngine.Object.Instantiate<GameObject>(this.plusAwesomePrefab, new Vector3(300f, 0f, 0f), Quaternion.identity, this.canvas.transform);
                 //plusAwesomeObj.GetComponent<Animator>().Play("Base Layer.plus", 0, 0f);
             }*/
            yield return new WaitForSeconds(0.95f);

            /* UnityEngine.Object.Destroy(plusObj);
             if (plusAwesomeObj != null)
             {
                 UnityEngine.Object.Destroy(plusAwesomeObj);
             }*/
        }
    }
}