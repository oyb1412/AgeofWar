using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
public class UiManager : MonoBehaviour
{
    [Header("Other")]
    public float currentCreateTime;
    public int createBlockCount;
    public int createBlockMaxCount;
    bool isCreate;

    [Header("Text")]
    public Text titleText;
    public Text mouseOnText;
    public Text createText;
    public Text towerSellText;


    [Header("Button")]
    public Button selectUnitBtns;
    public Button selectTowerBtns;
    public Button selectSellBtns;
    public Button selectAddBtns;
    public Button selectUpgradeBtns;

    [Header("Obj")]
    public GameObject[] unitPanel;
    public GameObject[] towerPanel;
    public GameObject skillObj;
    public GameObject selectMainPanel;
    public GameObject mouseOnPrefab;
    public GameObject towerSellImage;

    [Header("Slider")]
    public Slider createBar;

    [Header("Block")]
    public GameObject[] createBlock;

    [Header("Skill")]
    public GameObject[] skillPrefabs;
    private int skill0Count;
    private int skill3Count;

    CharBase mikata;
    CharBase teki;
    SpriteRenderer[] save;

    private void Start()
    {
        towerSellImage.SetActive(false);
        mouseOnPrefab.SetActive(false);
        save = new SpriteRenderer[4];
    }
    private void Update()
    {
        if (isCreate)
            currentCreateTime += Time.deltaTime;

        CharHpBarOnOff("MikataChar");
        CharHpBarOnOff("TekiChar");
    }
    void CharHpBarOnOff(string tag)
    {

        if (tag == "MikataChar")
        {
            if (GameManager.MouseRayCast(tag))
            {
                mikata = GameManager.MouseRayCast(tag).GetComponent<CharBase>();
                mikata.hpBar.gameObject.SetActive(true);
            }
            else if (mikata)
                mikata.hpBar.gameObject.SetActive(false);
        }
        else
        {
            if (GameManager.MouseRayCast(tag))
            {
               teki = GameManager.MouseRayCast(tag).GetComponent<CharBase>();
               teki.hpBar.gameObject.SetActive(true);
            }
            else if (teki)
                teki.hpBar.gameObject.SetActive(false);
        }
    }
    public void ClickUnitBtn()
    {
        selectMainPanel.SetActive(false);
        var level = GameManager.instance.mikataBase.currentLevel;
        unitPanel[level].SetActive(true);
        titleText.text = "Unit";
    }   
    public void ClickTowerBtn()
    {
        selectMainPanel.SetActive(false);
        var level = GameManager.instance.mikataBase.currentLevel;
        towerPanel[level].SetActive(true);
        titleText.text = "Tower";

    }

    public void CreateUnit0Btn(int index)
    {
        if (GameManager.instance.currentGold >= GameManager.instance.chars2[index].buyGold[0])
        {
            if (createBlockCount < createBlockMaxCount)
            {
                createBlock[createBlockCount].SetActive(true);
                switch(createBlockCount)
                {
                    case 0:
                        break;
                }
                createBlockCount++;
                isCreate = true;
                StartCoroutine(CreateUnit(GameManager.instance.chars2[index].createTime[0], index, 0));
            }
        }
    }
    IEnumerator CreateUnit(float createtime, int index, int unitnum)
    {
        GameManager.instance.currentGold -= GameManager.instance.chars2[index].buyGold[unitnum];

        while (true)
        {
            createBar.value = currentCreateTime / createtime;
            yield return null;
            if (createBar.value >= 1f)
            {
                var unit = Instantiate(GameManager.instance.chars2[index].chars1[unitnum]).transform;
                unit.position = transform.position;
                currentCreateTime = 0f;
                createBar.value = 0f;
                createBlockCount--;
                createBlock[createBlockCount].SetActive(false);

            }

            if (createBlockCount < 1)
            {
                isCreate = false;
                break;
            }
        }
    }


    public void CreateUnit1Btn(int index)
    {
        if (GameManager.instance.currentGold >= GameManager.instance.chars2[index].buyGold[1])
        {
            if (createBlockCount < createBlockMaxCount)
            {
                createBlock[createBlockCount].SetActive(true);
                createBlockCount++;
                isCreate = true;
                StartCoroutine(CreateUnit(GameManager.instance.chars2[index].createTime[1], index, 1));
            }
        }
    }    
    public void CreateUnit2Btn(int index)
    {
        if (GameManager.instance.currentGold >= GameManager.instance.chars2[index].buyGold[2])
        {
            if (createBlockCount < createBlockMaxCount)
            {
                createBlock[createBlockCount].SetActive(true);
                createBlockCount++;
                isCreate = true;
                StartCoroutine(CreateUnit(GameManager.instance.chars2[index].createTime[2], index, 2));
            }
        }
    }

    public void CreateTower0Btn(int index)
    {
        StartCoroutine(TowerCreateCorutine(index,0));
    }

    void MouseOn(Collider2D tag, int index)
    {
        
        if (tag)
        {
            save[index] = tag.GetComponentsInChildren<SpriteRenderer>()[1];
            save[index].gameObject.transform.localScale = new Vector2(2f, 2f);
        }
        else if (save[index])
            save[index].gameObject.transform.localScale = Vector2.zero;
    }


    IEnumerator TowerCreateCorutine(int index, int num)
    {
        var btn = EventSystem.current.currentSelectedGameObject.GetComponent<TowerButton>();
        mouseOnPrefab.GetComponent<Image>().sprite = btn.towerImage;
        mouseOnPrefab.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.7f);
        while (true)
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseOnPrefab.transform.position = new Vector3(mousePos.x, mousePos.y, 0f);
            mouseOnPrefab.SetActive(true);

            var frame0 = GameManager.MouseRayCast("TowerFrame0");
            var frame1 = GameManager.MouseRayCast("TowerFrame1");
            var frame2 = GameManager.MouseRayCast("TowerFrame2");
            var frame3 = GameManager.MouseRayCast("TowerFrame3");
            MouseOn(frame0, 0);
            MouseOn(frame1, 1);
            MouseOn(frame2, 2);
            MouseOn(frame3, 3);
            yield return null;

            if (Input.GetMouseButtonDown(0))
                {
                     if (GameManager.instance.currentGold >= GameManager.instance.towers[index].buyGold[num])
                    {
                        if (GameManager.instance.mikataBase.currentTowerCount < GameManager.instance.mikataBase.currentTowerFrameCount)
                        {
                            if (GameManager.instance.mikataBase.currentTowerCount < GameManager.instance.maxTowerCount)
                            {
                            if (frame0)
                            {
                             
                                var frame = frame0.GetComponent<TowerFrame>();
                                if (!frame.isUse)
                                {
                                    var unit = Instantiate(GameManager.instance.towers[index].tower[num]).transform;
                                    unit.position = frame.transform.position;
                                    frame.isUse = true;
                                }
                            }
                            else if (frame1)
                            {
                                var frame = frame1.GetComponent<TowerFrame>();
                                if (!frame.isUse)
                                {
                                    var unit = Instantiate(GameManager.instance.towers[index].tower[num]).transform;
                                    unit.position = frame.transform.position;
                                    frame.isUse = true;
                                }
                            }
                            else if (frame2)
                            {
                                var frame = frame2.GetComponent<TowerFrame>();
                                if (!frame.isUse)
                                {
                                    var unit = Instantiate(GameManager.instance.towers[index].tower[num]).transform;
                                    unit.position = frame.transform.position;
                                    frame.isUse = true;
                                }
                            }
                            else if (frame3)
                            {
                                var frame = frame3.GetComponent<TowerFrame>();
                                if (!frame.isUse)
                                {
                                    var unit = Instantiate(GameManager.instance.towers[index].tower[num]).transform;
                                    unit.position = frame.transform.position;
                                    frame.isUse = true;
                                }

                            }
                            else
                                continue;

                            GameManager.instance.mikataBase.currentTowerCount++;
                            GameManager.instance.currentGold -= GameManager.instance.towers[index].buyGold[num];
                            for (int i = 0; i < save.Length; i++)
                            {
                                if (save[i])
                                    save[i].gameObject.transform.localScale = Vector2.zero;
                            }
                            mouseOnPrefab.SetActive(false);
                            break;
                            }
                        }
                    }
            }
            else if(Input.GetMouseButtonDown(1))
            {
                for (int i = 0; i < save.Length; i++)
                {
                    if (save[i])
                        save[i].gameObject.transform.localScale = Vector2.zero;
                }
                mouseOnPrefab.SetActive(false);
                break;
            }
        }
        

    }

    IEnumerator TowerSellCorutine()
    {
        while (true)
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            towerSellImage.transform.position = new Vector3(mousePos.x, mousePos.y, 0f);
            towerSellImage.SetActive(true);

            var tower = GameManager.MouseRayCast("Tower");
            if (tower)
            {
                var target = GameManager.MouseRayCast("Tower").GetComponent<Tower>();
                towerSellText.text = target.towerSellGold.ToString();

 
                if (Input.GetMouseButtonDown(0))
                {
                    var fraems = Physics2D.CircleCast(target.transform.position, 0.1f, Vector2.zero, 0,LayerMask.GetMask("TowerFrame"));
                    
                    if (fraems)
                    {
                        var on = fraems.transform.GetComponent<TowerFrame>();
                        on.isUse = false;
                    }
                  
                    GameManager.instance.currentGold += target.towerSellGold;
                    GameManager.instance.mikataBase.currentTowerCount--;
                    Destroy(target.gameObject);
                    towerSellImage.SetActive(false);
                    break;
                }
            }
            else
                towerSellText.text = "";



            if (Input.GetMouseButtonDown(1))
            {
                towerSellImage.SetActive(false);
                break;
            }
            yield return null;
        }
    }
    public void CreateTower1Btn(int index)
    {
        StartCoroutine(TowerCreateCorutine(index,1));

    }
    public void CreateTower2Btn(int index)
    {
        StartCoroutine(TowerCreateCorutine(index,2));

    }

    public void ClickSellBtn()
    {
        titleText.text = "Sell";
        StartCoroutine(TowerSellCorutine());
    }
    public void ClickAddBtn()
    {
        GameManager.instance.TowerFrameAddClick();
    } 
    public void ClickUpgradeBtn()
    {
        GameManager.instance.BaseLevelUpClick();
    } 

    public void ClickSkillBtn()
    {
        var fill = skillObj.GetComponent<Image>();

        if (fill.fillAmount >= 1f)
        {
            fill.fillAmount = 0;
            StartCoroutine(SkillFillCorutine());

            switch (GameManager.instance.mikataBase.currentLevel)
            {
                case 0:
                    GameManager.instance.mainCamera.ScreenShake();
                    StartCoroutine(SkillZeroOneCorutine(0));
                    break;
                case 1:
                    GameManager.instance.mainCamera.ScreenShake();
                    StartCoroutine(SkillZeroOneCorutine(1));
                    break;
                case 2:
                    StartCoroutine(SkillTwoCorutine());
                    break;
                case 3:
                    skillPrefabs[3].SetActive(true);
                    break;
                case 4:
                    skillPrefabs[4].SetActive(true);
                    break;
            }
        }

 
    }
    IEnumerator SkillTwoCorutine()
    {
        while(true)
        {
            skill3Count++;
            var targets = GameObject.FindGameObjectsWithTag("MikataChar");
            for(int i = 0;i<targets.Length;i++)
            {
                targets[i].GetComponent<MikataChar>().skill2On = true;
            }
            yield return new WaitForSeconds(1f);
            if(skill3Count > 20)
            {
                for (int i = 0; i < targets.Length; i++)
                {
                        targets[i].GetComponent<MikataChar>().skill2On = false;
                }
                skill3Count = 0;
                break;
            }    
        }
    }
    IEnumerator SkillZeroOneCorutine(int index)
    {
        while (true)
        {
            var ranx = UnityEngine.Random.Range(-10f, 10f);
            var rany = UnityEngine.Random.Range(7.5f, 8.5f);
            var dir = new Vector2(ranx, rany);
            var obj = Instantiate(skillPrefabs[index]);
            obj.transform.position = dir;
            skill0Count++;

            yield return new WaitForSeconds(0.3f);
            if (skill0Count > 15)
            {
                skill0Count = 0;
                break;

            }
        }

    }
    IEnumerator SkillFillCorutine()
    {
        var fill = skillObj.GetComponent<Image>();

        while (true)
        {
            fill.fillAmount += Time.deltaTime / 60;
            yield return new WaitForSeconds(0.01f);

            if (fill.fillAmount >= 1f)
                break;
        }
    }

    public void ClickReturnBtn()
    {
        for(int i = 0; i < unitPanel.Length;i++)
        {
            unitPanel[i].SetActive(false);
            towerPanel[i].SetActive(false);
        }
        selectMainPanel.SetActive(true);
        titleText.text = "Menu";
    }


}
