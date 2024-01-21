using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum baseType{ MIKATA,TEKI}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Data data;
    public CameraMovemnet mainCamera;
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
        Init();
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

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
                mikataBase.currentLevel++;
 
                Destroy(mikataBase.baseObj);
                mikataBase.baseObj = Instantiate(mikataBase.BasePrefab[mikataBase.currentLevel], null);
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


    // Update is called once per frame
    void Update()
    {
        UISetting();
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

    //public static Collider2D MouseRayCastAll(string layer)
    //{
    //    Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    RaycastHit2D[] hit = Physics2D.RaycastAll(mousePos, Vector2.zero, 0f, LayerMask.GetMask(layer));
    //    if (hit[0].transform.gameObject != null)
    //        return hit[0].collider;
    //    else return null;
    //}
}
