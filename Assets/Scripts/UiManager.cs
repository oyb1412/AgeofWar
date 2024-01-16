using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;

    [Header("Button")]
    public GameObject[] unitBtns;
    public GameObject towerBtns;
    public GameObject upgradeBtns;

    [Header("Text")]
    public Text baseHpText;
    public Text goldText;
    public Text expText;

    [Header("Sprite")]
    public Sprite[] level1_CharImage;
    public Sprite[] level1_TowerImage;
    public Sprite level1_SkillImage;
    public Sprite level1_UpgradeImage;

    [Header("Char")]
    public MikataChar[] level0_CharPrefabs;
    public GameObject[] level0_TowerPrefabs;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        expText.text = GameManager.instance.baseExp.ToString();
        goldText.text = string.Format("{0:f0}", GameManager.instance.currentGold);
    }

    public void Unit1Click()
    {
        if (level0_CharPrefabs[0].useGold < GameManager.instance.currentGold)
        {
            Instantiate(level0_CharPrefabs[0]);
            GameManager.instance.currentGold -= level0_CharPrefabs[0].useGold;
        }
    }

    public void Unit2Click()
    {
        if (level0_CharPrefabs[1].useGold < GameManager.instance.currentGold)
        {
            Instantiate(level0_CharPrefabs[1]);
            GameManager.instance.currentGold -= level0_CharPrefabs[1].useGold;

        }
    }

    public void Unit3Click()
    {
        if (level0_CharPrefabs[2].useGold < GameManager.instance.currentGold)
        {
            
            Instantiate(level0_CharPrefabs[2]);
            GameManager.instance.currentGold -= level0_CharPrefabs[2].useGold;
        }
    }
}
