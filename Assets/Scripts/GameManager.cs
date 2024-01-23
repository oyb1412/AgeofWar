using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum baseType{ MIKATA,TEKI}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isLive;
    public Data data;
    public CameraMovemnet mainCamera;
    public AudioManager audioManager;
    [Header("--MikataBaseInfo--")]
    public int maxTowerFrameCount = 4;
    public int maxTowerCount = 3;
    public float[] baseMaxHp;
    public int baseMaxLevel = 4;
    public float currentGold;
    public double baseCurrentExp;
    public int[] frameAddGold;
    public int[] baseUpgradeEXP;
    [Header("--BasePrefabs--")]

    [Header("--UI--")]
    public Text goldText;
    public Text expText;
    public Sprite[] skillImage;
    public GameObject skillPrefab;
    public GameObject restartObject;
    [System.Serializable]
    public class Towers
    {
        public GameObject[] tower;
        public float[] buyGold;
        public float[] sellGold;
    }

    [System.Serializable]
    public class Chars
    {
        public CharBase[] charArray;
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
        public int currentTowerFrameCount;

        public float currentHp;
        public int currentLevel;
        public GameObject[] towerFrame;
        public GameObject[] BasePrefab;

        public Slider hpBar;
        public Text hpText;
    }

    public Towers[] towers;
    public Chars[] charClassArray;
    public MikataBase mikataBase;
    public TekiBase tekiBase;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
        Init();
        isLive = true;
        audioManager.PlayerBgm(true);
    }
    void Init()
    {
        baseMaxHp = data.base_hp;
        frameAddGold = data.slot_cost;
        baseUpgradeEXP = data.xp_cost;

        maxTowerCount = 4;
        maxTowerFrameCount = 4;
        mikataBase.towerFrame = new GameObject[maxTowerFrameCount];
        tekiBase.towerFrame = new GameObject[maxTowerFrameCount];

        mikataBase.baseObj = Instantiate(mikataBase.BasePrefab[mikataBase.currentLevel], null);
        mikataBase.currentHp = baseMaxHp[0];
        tekiBase.baseObj = Instantiate(tekiBase.BasePrefab[tekiBase.currentLevel], null);
        tekiBase.currentHp = baseMaxHp[0];

        tekiBase.baseObj.transform.position = new Vector3(13f, -2.1f, 0f);
        for (int i = 0; i < maxTowerCount; i++)
        {
            mikataBase.towerFrame[i] = mikataBase.baseObj.transform.GetChild(i).gameObject;
            tekiBase.towerFrame[i] = tekiBase.baseObj.transform.GetChild(i).gameObject;
        }
        mikataBase.currentTowerFrameCount = 1;
        tekiBase.currentTowerFrameCount = 1;
        int count = 0;
        for(int i = 0;i < towers.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                towers[i].buyGold[j] = data.turret_cost[count];
                towers[i].sellGold[j] = towers[i].buyGold[j] * 0.7f;
                    count++;
            }
        }

    }
    public void TowerFrameAddClick()
    {
        if (mikataBase.currentTowerFrameCount < maxTowerFrameCount)
        {
            if (currentGold >= frameAddGold[mikataBase.currentTowerFrameCount-1])
            {
                mikataBase.towerFrame[mikataBase.currentTowerFrameCount].SetActive(true);
                currentGold -= frameAddGold[mikataBase.currentTowerFrameCount-1];
                mikataBase.currentTowerFrameCount++;
            }
        }
    }


    public void BaseLevelUpClick()
    {
        if (baseUpgradeEXP[mikataBase.currentLevel] <= baseCurrentExp)
        {
            if (mikataBase.currentLevel < baseMaxLevel)
            {
                var percent = baseMaxHp[mikataBase.currentLevel] / mikataBase.currentHp;
                mikataBase.currentLevel++;

                Destroy(mikataBase.baseObj);
                mikataBase.baseObj = Instantiate(mikataBase.BasePrefab[mikataBase.currentLevel], null);
                mikataBase.currentHp = baseMaxHp[mikataBase.currentLevel] * percent;
                if (mikataBase.currentTowerFrameCount == 2)
                    mikataBase.towerFrame[1].SetActive(true);
                if (mikataBase.currentTowerFrameCount == 3)
                {
                    mikataBase.towerFrame[1].SetActive(true);
                    mikataBase.towerFrame[2].SetActive(true);
                }
                if (mikataBase.currentTowerFrameCount == 4)
                {
                    mikataBase.towerFrame[1].SetActive(true);
                    mikataBase.towerFrame[2].SetActive(true);
                    mikataBase.towerFrame[3].SetActive(true);
                }


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

    public void AgeUp()
    {

                var percent = baseMaxHp[tekiBase.currentLevel] / tekiBase.currentHp;
        tekiBase.currentLevel++;

        Destroy(tekiBase.baseObj);
        tekiBase.baseObj = Instantiate(tekiBase.BasePrefab[tekiBase.currentLevel], null);
        tekiBase.baseObj.transform.position = new Vector3(13f, -2.1f, 0f);

        tekiBase.currentHp = baseMaxHp[tekiBase.currentLevel] * percent;
                if (tekiBase.currentTowerFrameCount == 2)
            tekiBase.towerFrame[1].SetActive(true);
                if (tekiBase.currentTowerFrameCount == 3)
                {
                    tekiBase.towerFrame[1].SetActive(true);
            tekiBase.towerFrame[2].SetActive(true);
                }
                if (tekiBase.currentTowerFrameCount == 4)
                {
                    tekiBase.towerFrame[1].SetActive(true);
                    tekiBase.towerFrame[2].SetActive(true);
            tekiBase.towerFrame[3].SetActive(true);
                }


                for (int i = 0; i < maxTowerCount; i++)
                {
            tekiBase.towerFrame[i] = tekiBase.baseObj.transform.GetChild(i).gameObject;
                }
                if (tekiBase.currentTowerFrameCount == 1)
                    tekiBase.towerFrame[0].SetActive(true);
                if (tekiBase.currentTowerFrameCount == 2)
                {
                    tekiBase.towerFrame[0].SetActive(true);
                    tekiBase.towerFrame[1].SetActive(true);
                }
                if (tekiBase.currentTowerFrameCount == 3)
                {
                    tekiBase.towerFrame[0].SetActive(true);
                    tekiBase.towerFrame[1].SetActive(true);
                    tekiBase.towerFrame[2].SetActive(true);
                }
  
    }
    // Update is called once per frame
    void Update()
    {
        if (!isLive)
            return;

        if(mikataBase.currentHp <= 0 || tekiBase.currentHp <= 0)
        {
            audioManager.PlayerBgm(false);
            StopAllCoroutines();
            restartObject.SetActive(true);
            isLive = false;

        }
        UISetting();
    }

    public void GameRestart()
    {
        SceneManager.LoadScene(0);
    }
    void UISetting()
    {
        mikataBase.hpBar.value = mikataBase.currentHp / baseMaxHp[mikataBase.currentLevel];
        mikataBase.hpText.text = string.Format("{0:f0}", mikataBase.currentHp); 
        tekiBase.hpBar.value = tekiBase.hpBar.value = tekiBase.currentHp / baseMaxHp[tekiBase.currentLevel];
        tekiBase.hpText.text = string.Format("{0:f0}", tekiBase.currentHp);
        goldText.text = string.Format("{0:f0}", currentGold);
        expText.text = baseCurrentExp.ToString();
    }

    public static Collider2D MouseRayCast(string tag)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0f);

        if (hit.collider != null && hit.collider.CompareTag(tag))
        {
            return hit.collider;
        }
        else
            return null;
    }


}
