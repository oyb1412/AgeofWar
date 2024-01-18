using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public enum baseType{ MIKATA,TEKI}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public CameraMovemnet mainCamera;
    [Header("--MikataBaseInfo--")]
    public int maxTowerFrameCount = 4;
    public int maxTowerCount = 3;
    public float baseMaxHp;
    public int baseMaxLevel = 4;
    public float currentGold;
    public int currentExp;
    public int[] frameAddGold = { 50, 100, 150, 200 };
    public int[] upgradeEXP = { 100, 150, 200,250,300 };
    public int[] maxEXP = { 100, 150, 200,250,300 };
    [Header("--BasePrefabs--")]

    [Header("--UI--")]
    public Text goldText;
    public Text expText;
    public Sprite[] skillImage;
    public GameObject skillPrefab;

    [System.Serializable]
    public class Towers
    {
        public GameObject[] tower;
        public int[] buyGold;
        public int[] sellGold;
    }

    [System.Serializable]
    public class Chars
    {
        public GameObject[] chars1;
        public int[] buyGold;
        public float[] createTime;
    }
    [System.Serializable]
    public class MikataBase 
    {
        public GameObject baseObj;
        public int currentTowerCount;
        public int currentTowerFrameCount;
        public float currentHp;
        public int currentLevel;
        public GameObject[] towerFrame;
        public GameObject[] BasePrefab;

        public Slider hpBar;
        public Text hpText;
    }

    [System.Serializable]
    public class TekiBase 
    {
        public GameObject baseObj;
        public int currentTowerCount;
        public float currentHp;
        public int currentLevel;
        public GameObject[] towerFrame;
        public GameObject[] BasePrefab;

        public Slider hpBar;
        public Text hpText;
    }

    public Towers[] towers;
    public Chars[] chars2;
    public MikataBase mikataBase;
    public TekiBase tekiBase;

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Init();
    }

    void Init()
    {
        baseMaxHp = 2000;

        maxTowerCount = 4;
        maxTowerFrameCount = 4;
        mikataBase.towerFrame = new GameObject[maxTowerFrameCount];
        tekiBase.towerFrame = new GameObject[maxTowerFrameCount];

        mikataBase.baseObj = Instantiate(mikataBase.BasePrefab[mikataBase.currentLevel], null);
        tekiBase.baseObj = Instantiate(tekiBase.BasePrefab[tekiBase.currentLevel], null);
        tekiBase.baseObj.GetComponent<SpriteRenderer>().flipX = true;
        tekiBase.baseObj.transform.position = new Vector3(13f, -2.1f, 0f);
        for (int i = 0; i < maxTowerCount; i++)
        {
            mikataBase.towerFrame[i] = mikataBase.baseObj.transform.GetChild(i).gameObject;
            tekiBase.towerFrame[i] = tekiBase.baseObj.transform.GetChild(i).gameObject;
        }
        mikataBase.currentTowerFrameCount = 1;
    }
    public void TowerFrameAddClick()
    {
        if (mikataBase.currentTowerFrameCount < maxTowerFrameCount)
        {
            if (currentGold >= frameAddGold[mikataBase.currentTowerFrameCount])
            {
                mikataBase.towerFrame[mikataBase.currentTowerFrameCount].SetActive(true);
                currentGold -= frameAddGold[mikataBase.currentTowerFrameCount];
                mikataBase.currentTowerFrameCount++;
            }
        }
    }


    public void BaseLevelUpClick()
    {
        if (upgradeEXP[mikataBase.currentLevel] >= maxEXP[mikataBase.currentLevel])
        {
            if (mikataBase.currentLevel < baseMaxLevel)
            {
                mikataBase.currentLevel++;
                Destroy(mikataBase.baseObj);
                mikataBase.baseObj = Instantiate(mikataBase.BasePrefab[mikataBase.currentLevel], null);
                skillPrefab.GetComponent<Image>().sprite = skillImage[mikataBase.currentLevel];
                for (int i = 0; i < maxTowerCount; i++)
                {
                    mikataBase.towerFrame[i] = mikataBase.baseObj.transform.GetChild(i).gameObject;
                }
                if (mikataBase.currentTowerFrameCount == 1)
                    mikataBase.towerFrame[0].SetActive(true);
                if (mikataBase.currentTowerFrameCount == 2)
                {
                    mikataBase.towerFrame[0].SetActive(true);
                    mikataBase.towerFrame[1].SetActive(true);
                }
                if (mikataBase.currentTowerFrameCount == 3)
                {
                    mikataBase.towerFrame[0].SetActive(true);
                    mikataBase.towerFrame[1].SetActive(true);
                    mikataBase.towerFrame[2].SetActive(true);
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        currentGold+= Time.deltaTime;
        UISetting();
    }

    void UISetting()
    {
        mikataBase.hpBar.value = mikataBase.currentHp / baseMaxHp;
        mikataBase.hpText.text = string.Format("{0:f0}", mikataBase.currentHp); 
        tekiBase.hpBar.value = tekiBase.currentHp / baseMaxHp;
        tekiBase.hpText.text = string.Format("{0:f0}", tekiBase.currentHp);
        goldText.text = string.Format("{0:f0}", currentGold);
        expText.text = currentExp.ToString();
    }
}
