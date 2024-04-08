using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace multi_tower_attack_3d
{
	[System.Serializable]
	public class LevelS
	{
		public List<GameObject> Level;
	}
	public class Base : BaseSceneManager<Base>
	{

		public bool IsStackModeOn = false;
		public bool isGameover = false;

		private float constructAngle;
		private List<float> speedHistory;
		private Vector2 startPosition;

		public float currentAngle;
		//private float currentAngleRotLocal;
		public float currentAngleSpeed;
		public static int currentLevel = 0;
		public static int tempCurrentLevel = 0;
		public float currentYOffset;
		public List<GameObject> platforms;

		public List<GameObject> beginModels;
		public List<GameObject> easyModels;
		public List<GameObject> randomModels;
		public List<GameObject> midModels;
		public List<GameObject> hardModels;

		public GameObject finish;
		public List<int> beginCountConfig;
		public List<int> easyCountConfig;
		public List<int> randomCountConfig;
		public List<int> midCountConfig;
		public List<int> hardCountConfig;

		public GameObject mainBranch;
		public GameObject reviveBlock;

		public float maxRotateSpeed;
		public float rotateSpeed;
		public float minSwipeDistX;
		public float minSwipeDistY;
		public Material ballMaterial;
		public Material platformMaterial;
		public Material failPlatformMaterial;
		public Material branchMaterial;
		public Material bGMaterial;

		[SerializeField]
		public List<Color32> ballbaseColorList = new List<Color32>();
		public List<Color32> platformColorList = new List<Color32>();
		public List<Color32> failplatformColorList = new List<Color32>();
		public List<GameObject> ballprefab = new List<GameObject>();
		//	public List<Color32> ballbaseColorsList = new List<Color32>() ; 
		public Material[] GradientBG;
		public GameObject GradientBGReplace;
		public Material trailMat;
		public Material decalMat;
		[SerializeField]
		public List<LevelS> levelPrefabList = new List<LevelS>();

		private void Awake()
		{
			//PlayerPrefs.DeleteAll ();
			AddLevelPrefabs();

			Application.targetFrameRate = 60;
			this.speedHistory = new List<float>();
			this.platforms = new List<GameObject>();

			this.CreateLevel();
		}

		int randomNoforStack;
		public void AddLevelPrefabs()
		{
			beginModels.Clear();
			easyModels.Clear();
			randomModels.Clear();
			midModels.Clear();
			hardModels.Clear();

			randomNoforStack = Random.Range(0, levelPrefabList.Count);
			//		Debug.Log ("levellist" + levelPrefabList.Count+"randomNoforStack"+randomNoforStack);

			reviveBlock = levelPrefabList[randomNoforStack].Level[0];

			// add prefab to begin models
			int randTemp = Random.Range(0, levelPrefabList[randomNoforStack].Level.Count);
			for (int i = 0; i < 5; i++)
			{
				beginModels.Add(levelPrefabList[randomNoforStack].Level[randTemp]);
			}
			// add prefab to easy models
			int randTemp1 = Random.Range(0, levelPrefabList[randomNoforStack].Level.Count);
			for (int i = 0; i < 5; i++)
			{
				easyModels.Add(levelPrefabList[randomNoforStack].Level[randTemp1]);
			}

			// add prefab to random models
			int randTemp2;
			for (int i = 0; i < 5; i++)
			{
				randTemp2 = Random.Range(0, levelPrefabList[randomNoforStack].Level.Count);
				randomModels.Add(levelPrefabList[randomNoforStack].Level[randTemp2]);
			}
			// add prefab to midModels models
			for (int i = 0; i < levelPrefabList[randomNoforStack].Level.Count; i++)
			{
				midModels.Add(levelPrefabList[randomNoforStack].Level[i]);
			}
			// add prefab to hard models
			for (int i = 0; i < levelPrefabList[randomNoforStack].Level.Count; i++)
			{
				hardModels.Add(levelPrefabList[randomNoforStack].Level[i]);
			}

		}
		
		Color32 colorTemp;
		int beginPlatformNo;
		
		public void CreateLevel()
		{			
			//		PlayerPrefs.DeleteAll ();
			this.constructAngle = -20f;
			currentLevel = Random.Range(0, 15); //PlayerPrefs.GetInt("currentLevel");			

			for (int i = 0; i < ballprefab.Count; i++)
			{
				ballprefab[i].SetActive(false);
			}
			int activeBall = Random.Range(0, ballprefab.Count);
			ballprefab[activeBall].SetActive(true);

			trailMat.color = ballbaseColorList[activeBall];
			decalMat.color = ballbaseColorList[activeBall];
			ballMaterial.color = ballbaseColorList[activeBall];

			colorTemp = ballbaseColorList[activeBall];
			colorTemp.a = 110;
			trailMat.color = colorTemp;

			platformMaterial.color = platformColorList[activeBall];
			failPlatformMaterial.color = failplatformColorList[activeBall];
		
			GradientBGReplace.GetComponent<Renderer>().material = GradientBG[activeBall];

			if (currentLevel >= this.beginCountConfig.Count)
			{
				currentLevel = this.beginCountConfig.Count - 1;
			}
			
			int num = 1;
			if (BaseSceneManager<mc>.Instance.gameId == GameType.GAME_SHORT)
			{
				num = 3;
			}
			beginPlatformNo = Random.Range(0, 3);

			if (IsStackModeOn)
			{
				int allPlatformNum = (this.beginCountConfig[currentLevel] / num) + (this.easyCountConfig[currentLevel] / num) + (this.randomCountConfig[currentLevel] / num) + (this.midCountConfig[currentLevel] / num) + (this.hardCountConfig[currentLevel] / num);

				for (int i = 0; i < 5; i++)
				{
					this.platforms.Add(Object.Instantiate<GameObject>(this.reviveBlock, new Vector3(0f, this.currentYOffset, 0f), Quaternion.Euler(new Vector3(0f, this.constructAngle, 0f)), base.transform));
					this.currentYOffset -= 3;
					this.constructAngle -= 4;
				}
				for (int i = 0; i < (allPlatformNum + 1); i++)
				{
					if (i < (this.beginCountConfig[currentLevel] / num))
					{
						this.platforms.Add(Object.Instantiate<GameObject>(this.beginModels[Random.Range(0, this.beginModels.Count)], new Vector3(0f, this.currentYOffset, 0f), Quaternion.Euler(new Vector3(0f, this.constructAngle, 0f)), base.transform));

						if (beginPlatformNo == 0)
						{
							this.constructAngle -= 2;
						}
						else if (beginPlatformNo == 1)
						{
							this.constructAngle -= 4;
						}
						else if (beginPlatformNo == 2)
						{
							this.constructAngle -= 6;
						}
						this.currentYOffset -= 3;//15f;
					}
					else if (i < ((this.beginCountConfig[currentLevel] / num) + (this.easyCountConfig[currentLevel] / num)))
					{
						this.platforms.Add(Object.Instantiate<GameObject>(this.easyModels[Random.Range(0, this.easyModels.Count)], new Vector3(0f, this.currentYOffset, 0f), Quaternion.Euler(new Vector3(0f, this.constructAngle, 0f)), base.transform));
						//this.constructAngle += Mathf.Sign ((float)Random.Range (-5, 5)) * Random.Range (5, 10);
						if (beginPlatformNo == 0)
						{
							this.constructAngle -= 2;
						}
						else if (beginPlatformNo == 1)
						{
							this.constructAngle -= 4;
						}
						else if (beginPlatformNo == 2)
						{
							this.constructAngle -= 6;
						}
						this.currentYOffset -= 3; // 15f;

					}
					else if (i < (((this.beginCountConfig[currentLevel] / num) + (this.easyCountConfig[currentLevel] / num)) + (this.randomCountConfig[currentLevel] / num)))
					{

						if (beginPlatformNo == 0)
						{
							this.constructAngle -= 2;
						}
						else if (beginPlatformNo == 1)
						{
							this.constructAngle -= 4;
						}
						else if (beginPlatformNo == 2)
						{
							this.constructAngle -= 6;
						}

						this.platforms.Add(Object.Instantiate<GameObject>(this.randomModels[Random.Range(0, this.randomModels.Count)], new Vector3(0f, this.currentYOffset, 0f), Quaternion.Euler(new Vector3(0f, this.constructAngle, 0f)), base.transform));
						this.currentYOffset -= 3; // 17.5f;

					}
					else if (i < (((this.beginCountConfig[currentLevel] / num) + (this.easyCountConfig[currentLevel] / num)) + (this.randomCountConfig[currentLevel] / num) + (this.midCountConfig[currentLevel] / num)))
					{
						if (beginPlatformNo == 0)
						{
							this.constructAngle -= 2;
						}
						else if (beginPlatformNo == 1)
						{
							this.constructAngle -= 4;
						}
						else if (beginPlatformNo == 2)
						{
							this.constructAngle -= 6;
						}
						//					this.constructAngle += Mathf.Sign((float)Random.Range(-5, 5)) * Random.Range(90, 180);
						this.platforms.Add(Object.Instantiate<GameObject>(this.midModels[Random.Range(0, this.midModels.Count)], new Vector3(0f, this.currentYOffset, 0f), Quaternion.Euler(new Vector3(0f, this.constructAngle, 0f)), base.transform));
						this.currentYOffset -= 3; // 17.5f;
					}
					else if (i < ((((this.beginCountConfig[currentLevel] / num) + (this.easyCountConfig[currentLevel] / num)) + (this.randomCountConfig[currentLevel] / num) + (this.midCountConfig[currentLevel] / num)) + (this.hardCountConfig[currentLevel] / num)))
					{
						if (beginPlatformNo == 0)
						{
							this.constructAngle -= 2;
						}
						else if (beginPlatformNo == 1)
						{
							this.constructAngle -= 4;
						}
						else if (beginPlatformNo == 2)
						{
							this.constructAngle -= 6;
						}
						//					this.constructAngle += Mathf.Sign((float)Random.Range(45, 90)) * Random.Range(90, 270);
						this.platforms.Add(Object.Instantiate<GameObject>(this.hardModels[Random.Range(0, this.hardModels.Count)], new Vector3(0f, this.currentYOffset, 0f), Quaternion.Euler(new Vector3(0f, this.constructAngle, 0f)), base.transform));
						this.currentYOffset -= 3; // 18f;
					}
					else
					{
						this.constructAngle += Mathf.Sign((float)Random.Range(-5, 5)) * Random.Range(90, 180);

						this.platforms.Add(Object.Instantiate<GameObject>(this.finish, new Vector3(0f, this.currentYOffset, 0f), Quaternion.Euler(new Vector3(0f, this.constructAngle, 0f)), base.transform));
					}
				}

			}
			else
			{

				int allPlatformNum = (this.beginCountConfig[currentLevel] / num) + (this.easyCountConfig[currentLevel] / num) + (this.midCountConfig[currentLevel] / num) + (this.hardCountConfig[currentLevel] / num);

				for (int i = 0; i < (allPlatformNum + 1); i++)
				{
					if (i < (this.beginCountConfig[currentLevel] / num))
					{
						this.platforms.Add(Object.Instantiate<GameObject>(this.beginModels[Random.Range(0, this.beginModels.Count)], new Vector3(0f, this.currentYOffset, 0f), Quaternion.Euler(new Vector3(0f, this.constructAngle, 0f)), base.transform));
						this.constructAngle += Mathf.Sign((float)Random.Range(-5, 5)) * Random.Range(20, 60);
						this.currentYOffset -= 3;//15f;
					}
					else if (i < ((this.beginCountConfig[currentLevel] / num) + (this.easyCountConfig[currentLevel] / num)))
					{
						this.platforms.Add(Object.Instantiate<GameObject>(this.easyModels[Random.Range(0, this.easyModels.Count)], new Vector3(0f, this.currentYOffset, 0f), Quaternion.Euler(new Vector3(0f, this.constructAngle, 0f)), base.transform));
						this.constructAngle += Mathf.Sign((float)Random.Range(-5, 5)) * Random.Range(60, 80);
						this.currentYOffset -= 3; // 15f;
					}
					else if (i < (((this.beginCountConfig[currentLevel] / num) + (this.easyCountConfig[currentLevel] / num)) + (this.midCountConfig[currentLevel] / num)))
					{
						this.constructAngle += Mathf.Sign((float)Random.Range(-5, 5)) * Random.Range(80, 120);
						this.platforms.Add(Object.Instantiate<GameObject>(this.midModels[Random.Range(0, this.midModels.Count)], new Vector3(0f, this.currentYOffset, 0f), Quaternion.Euler(new Vector3(0f, this.constructAngle, 0f)), base.transform));
						this.currentYOffset -= 3; // 17.5f;
					}
					else if (i < ((((this.beginCountConfig[currentLevel] / num) + (this.easyCountConfig[currentLevel] / num)) + (this.midCountConfig[currentLevel] / num)) + (this.hardCountConfig[currentLevel] / num)))
					{
						this.constructAngle += Mathf.Sign((float)Random.Range(-5, 5)) * Random.Range(90, 180);
						this.platforms.Add(Object.Instantiate<GameObject>(this.hardModels[Random.Range(0, this.hardModels.Count)], new Vector3(0f, this.currentYOffset, 0f), Quaternion.Euler(new Vector3(0f, this.constructAngle, 0f)), base.transform));
						this.currentYOffset -= 3; // 18f;
					}
					else
					{
						this.constructAngle += Mathf.Sign((float)Random.Range(-5, 5)) * Random.Range(90, 180);
						this.platforms.Add(Object.Instantiate<GameObject>(this.finish, new Vector3(0f, this.currentYOffset, 0f), Quaternion.Euler(new Vector3(0f, this.constructAngle, 0f)), base.transform));
					}
				}
			}

			this.mainBranch.transform.position = new Vector3(0f, 6f + (this.currentYOffset / 2f), 0f);
			this.mainBranch.transform.localScale = new Vector3(8f, (12f - this.currentYOffset) / 2f, 8f);
			this.mainBranch.GetComponent<MeshRenderer>().material.mainTextureScale = new Vector2(1f, (12f - this.currentYOffset) / 6f);

		}


		public void Revive(int index)
		{
			this.currentAngle = 0f;

			this.platforms.Insert(index, Object.Instantiate<GameObject>(this.reviveBlock, new Vector3(0f, this.platforms[index].transform.position.y, 0f), platforms[mc.Instance.currentPlayformId].transform.localRotation, base.transform));
		}

		void Update()
		{
			if (BaseSceneManager<mc>.Instance.isActive)
			{

				this.UpdateInput();


				// this.currentAngleSpeed = Mathf.Lerp(this.currentAngleSpeed, 0f, 5f * Time.smoothDeltaTime);            
				// this.currentAngle += this.currentAngleSpeed * Time.smoothDeltaTime;

				//int num = (int)(this.currentAngle / 10f);
				//this.currentAngleRotLocal = Mathf.Lerp(this.currentAngleRotLocal, (float)(num * 10), 20f * Time.deltaTime);
				base.transform.localRotation = Quaternion.Euler(new Vector3(0f, this.currentAngle, 0f));
			}


		}
		public void TriggerFalseWaitFromMC()
		{
			Invoke("TriggerFalseWait", 0.1f);
		}
		public void TriggerFalseWait()
		{
			mc.Instance.GetComponent<Collider>().isTrigger = false;
			mc.Instance.GetComponent<Rigidbody>().velocity = Vector3.up;
		}
		public void RemoveForce()
		{
			mc.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
		}
		public void TapToPlayHomeSCreen()
		{
			PlayButtonClicked = true;
			UI.instance.mainMenu.SetActive(false);
		}

		public int isMouseDown;
		public bool PlayButtonClicked;
		bool ballColliderTrigger;

		private void UpdateInput()
		{
			currentAngle += 60f * Time.fixedDeltaTime;

			if (IsStackModeOn && PlayButtonClicked)
			{
				if (BaseSceneManager<mc>.Instance.isGameStarted)
				{
					if (Input.GetMouseButtonDown(0))
					{
						if (ballColliderTrigger)
						{
							mc.Instance.GetComponent<Collider>().isTrigger = false;
							ballColliderTrigger = false;
						}
					}
					else
					{
						if (Input.GetMouseButtonDown(0))
						{
							isMouseDown = 1;
							if (!isGameover)
							{
								mc.Instance.GetComponent<Collider>().isTrigger = true;
								mc.Instance.GetComponent<Rigidbody>().velocity = Vector3.down * 20;// * Time.deltaTime;
																								   //Debug.Log ("Mouse Down");
							}
						}
						else if (Input.GetMouseButton(0))
						{
							isMouseDown = 1;
							if (!isGameover)
							{
								if (!ballColliderTrigger)
								{
									mc.Instance.GetComponent<Collider>().isTrigger = true;
									mc.Instance.gameObject.transform.position = new Vector3(0, mc.Instance.gameObject.transform.position.y, -10.5f);
									ballColliderTrigger = true;
								}
								mc.Instance.GetComponent<Rigidbody>().velocity = Vector3.down * 30;
							}
						}
						else if (Input.GetMouseButtonUp(0))
						{
							mc.Instance.gameObject.transform.position = new Vector3(0, mc.Instance.gameObject.transform.position.y, -8f);
							isMouseDown = 0;
							ballColliderTrigger = false;
							mc.Instance.tempScore = 0;
							UI.instance.whiteFillCircle.fillAmount = 0;
							UI.instance.whiteFillCircle.gameObject.SetActive(false);
							//Debug.Log ("Mouse Up"); 
						}
					}
				}
			}
		}
	}
}
