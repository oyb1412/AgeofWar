using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Build;
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
    public GameObject mouseOnPrefab;

    [Header("Slider")]
    public Slider createBar;

    [Header("Block")]
    public GameObject[] createBlock;

    [Header("Skill")]
    public GameObject[] skillPrefabs;
    private int skill0Count;
    private int skill3Count;

    private void Start()
    {
        mouseOnPrefab.SetActive(false);
    }
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

    IEnumerator TowerCreateCorutine(int index, int num)
    {
        while(true)
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseOnPrefab.transform.position = new Vector3(mousePos.x, mousePos.y, 0f);
            mouseOnPrefab.SetActive(true);
            yield return null;

            if(Input.GetMouseButtonDown(0))
            {
                var frame0 = MouseRayCast("TowerFrame0");
                var frame1 = MouseRayCast("TowerFrame1");
                var frame2 = MouseRayCast("TowerFrame2");
                var frame3 = MouseRayCast("TowerFrame3");
 
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
                            mouseOnPrefab.SetActive(false);
                            break;
                            }
                        }
                    }
            }
            else if(Input.GetMouseButtonDown(1))
            {
                mouseOnPrefab.SetActive(false);
                break;
            }
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

    }

    public Collider2D MouseRayCast(string tag)
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
