using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [Header("Other")]
    public float currentCreateTime;
    public int createBlockCount;
    public int createBlockMaxCount;
    bool isCreate;
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

    [Header("Slider")]
    public Slider createBar;

    [Header("Block")]
    public GameObject[] createBlock;

    private void Update()
    {
        if (isCreate)
            currentCreateTime += Time.deltaTime;
    }

    public void ClickUnitBtn()
    {
        selectMainPanel.SetActive(false);
        var level = GameManager.instance.mikataBase.currentLevel;
        unitPanel[level].SetActive(true);
    }   
    public void ClickTowerBtn()
    {
        selectMainPanel.SetActive(false);
        var level = GameManager.instance.mikataBase.currentLevel;
        towerPanel[level].SetActive(true);
    } 

    public void CreateUnit0Btn(int index)
    {
        if (GameManager.instance.currentGold >= GameManager.instance.chars2[index].buyGold[0])
        {
            if (createBlockCount < createBlockMaxCount)
            {
                createBlock[createBlockCount].SetActive(true);
                createBlockCount++;
                isCreate = true;
                StartCoroutine(CreateUnit(GameManager.instance.chars2[index].createTime[0], index, 0));
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
        if (GameManager.instance.currentGold >= GameManager.instance.towers[index].buyGold[0])
        {
            if (GameManager.instance.mikataBase.currentTowerCount < GameManager.instance.mikataBase.currentTowerFrameCount)
            {
                if (GameManager.instance.mikataBase.currentTowerCount < GameManager.instance.maxTowerCount)
                {
                    var unit = Instantiate(GameManager.instance.towers[index].tower[0]).transform;
                    switch (GameManager.instance.mikataBase.currentTowerCount)
                    {
                        case 0:
                            unit.position = new Vector3(-13.1f, -1.2f, 0f);
                            break;
                        case 1:
                            unit.position = new Vector3(-13.1f, 0f, 0f);
                            break;
                        case 2:
                            unit.position = new Vector3(-13.1f, 1f, 0f);
                            break;
                    }
                    GameManager.instance.mikataBase.currentTowerCount++;
                    GameManager.instance.currentGold -= GameManager.instance.towers[index].buyGold[0];
                }
            }
        }
    }
    public void CreateTower1Btn(int index)
    {
        if (GameManager.instance.currentGold >= GameManager.instance.towers[index].buyGold[1])
        {
            if (GameManager.instance.mikataBase.currentTowerCount < GameManager.instance.mikataBase.currentTowerFrameCount)
            {
                if (GameManager.instance.mikataBase.currentTowerCount < GameManager.instance.maxTowerCount)
                {
                    var unit = Instantiate(GameManager.instance.towers[index].tower[1]).transform;
                    switch (GameManager.instance.mikataBase.currentTowerCount)
                    {
                        case 0:
                            unit.position = new Vector3(-13.1f, -1.2f, 0f);
                            break;
                        case 1:
                            unit.position = new Vector3(-13.1f, 0f, 0f);
                            break;
                        case 2:
                            unit.position = new Vector3(-13.1f, 1f, 0f);
                            break;
                    }
                    GameManager.instance.mikataBase.currentTowerCount++;
                    GameManager.instance.currentGold -= GameManager.instance.towers[index].buyGold[1];
                }
            }
        }
    }
    public void CreateTower2Btn(int index)
    {
        if (GameManager.instance.currentGold >= GameManager.instance.towers[index].buyGold[2])
        {
            if (GameManager.instance.mikataBase.currentTowerCount < GameManager.instance.mikataBase.currentTowerFrameCount)
            {
                if (GameManager.instance.mikataBase.currentTowerCount < GameManager.instance.maxTowerCount)
                {
                    var unit = Instantiate(GameManager.instance.towers[index].tower[2]).transform;
                    switch (GameManager.instance.mikataBase.currentTowerCount)
                    {
                        case 0:
                            unit.position = new Vector3(-13.1f, -1.2f, 0f);
                            break;
                        case 1:
                            unit.position = new Vector3(-13.1f, 0f, 0f);
                            break;
                        case 2:
                            unit.position = new Vector3(-13.1f, 1f, 0f);
                            break;
                    }
                    GameManager.instance.mikataBase.currentTowerCount++;
                    GameManager.instance.currentGold -= GameManager.instance.towers[index].buyGold[2];
                }
            }
        }
    }

    public void ClickSellBtn()
    {

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

    }
}
