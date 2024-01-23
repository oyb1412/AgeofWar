using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UiManager : MonoBehaviour
{
    [Header("Other")]
    public float currentCreateTime;
    public int createBlockCount;
    public int createBlockMaxCount;

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
    SpriteRenderer saveSell;
    bool trigger;

 
    [System.Serializable]
    public class Factory
    {
        public GameManager.Chars chars;
        public int index;
        public int num;
        public float createTime;
    }

    public Factory[] factory;

    private void Start()
    {
        towerSellImage.SetActive(false);
        mouseOnPrefab.SetActive(false);
        save = new SpriteRenderer[4];
    }
    private void Update()
    {
        if (!GameManager.instance.isLive)
            return;
        CharHpBarOnOff("MikataChar");
        CharHpBarOnOff("TekiChar");
        CreateUnitFactorySystem();

    }
    void CharHpBarOnOff(string tag)
    {

        if (tag == "MikataChar")
        {
            if (GameManager.MouseRayCast(tag))
            {
                mikata = GameManager.MouseRayCast(tag).GetComponent<CharBase>();
                if (mikata)
                mikata.hpBar.gameObject.SetActive(true);
            }
            else
            {
                if(mikata)
                    if(mikata.isLive)
                    mikata.hpBar.gameObject.SetActive(false);
            }
        }
        else
        {
            if (GameManager.MouseRayCast(tag))
            {
                teki = GameManager.MouseRayCast(tag).GetComponent<CharBase>();
                if (teki)
                    teki.hpBar.gameObject.SetActive(true);
            }
            else
            {
                if(teki)
                    if(teki.isLive)
                teki.hpBar.gameObject.SetActive(false);
            }
        }
    }
    public void ClickUnitBtn()
    {
        if (!trigger)
        {
            for (int i = 0; i < 3; i++)
            {
                factory[i].chars.charArray = new CharBase[3];
                Debug.Log(i);
            }
            trigger = true;
        }
        selectMainPanel.SetActive(false);
        var level = GameManager.instance.mikataBase.currentLevel;
        unitPanel[level].SetActive(true);
        titleText.text = "Unit";
    }   
    public void ClickTowerBtn()
    {
        if (!trigger)
        {
            for (int i = 0; i < factory.Length; i++)
            {
                factory[i].chars.charArray = new CharBase[3];
            }
            trigger = true;
        }
        selectMainPanel.SetActive(false);
        var level = GameManager.instance.mikataBase.currentLevel;
        towerPanel[level].SetActive(true);
        titleText.text = "Tower";

    }

    public void CreateUnit0Btn(int index)
    {
        if (GameManager.instance.currentGold >= GameManager.instance.charClassArray[index].charArray[0].charCost)
        {

            if (createBlockCount < createBlockMaxCount)
            {

                createBlockCount++;
                createBlock[createBlockCount-1].SetActive(true);
                factory[createBlockCount - 1].chars.charArray = GameManager.instance.charClassArray[index].charArray;
                factory[createBlockCount - 1].index = index;
                factory[createBlockCount - 1].num = 0;
                factory[createBlockCount - 1].createTime = GameManager.instance.charClassArray[index].charArray[0].charTrainingTime;
                GameManager.instance.currentGold -= GameManager.instance.charClassArray[index].charArray[0].charCost;
            }
        }
    }
 
    void CreateUnitFactorySystem()
    {
        if (createBlockCount > 0 && factory[0] != null)
        {
            currentCreateTime += Time.deltaTime;
            createBar.value = currentCreateTime / factory[0].createTime;
            createText.text = "Traning to " +factory[0].chars.charArray[factory[0].num].charName + "...";
            if (createBar.value >= 1f)
            {
                var unit = Instantiate(factory[0].chars.charArray[factory[0].num],null).transform;
                unit.position = transform.position;
                currentCreateTime = 0f;
                createBar.value = 0f;
                createText.text = "";
                var temp = factory[0];
                for (int i = 1; i < createBlockCount; i++)
                {
                    factory[i -1] = factory[i];
                }
                factory[createBlockCount - 1] = temp;


                createBlockCount--;

                createBlock[createBlockCount].SetActive(false);

            }


        }

    }
    public void CreateUnit1Btn(int index)
    {
        if (GameManager.instance.currentGold >= GameManager.instance.charClassArray[index].charArray[1].charCost)
        {
            if (createBlockCount < createBlockMaxCount)
            {
                createBlockCount++;

                createBlock[createBlockCount-1].SetActive(true);
 
                factory[createBlockCount - 1].chars.charArray = GameManager.instance.charClassArray[index].charArray;
                factory[createBlockCount - 1].index = index;
                factory[createBlockCount - 1].num = 1;
                factory[createBlockCount - 1].createTime = GameManager.instance.charClassArray[index].charArray[1].charTrainingTime;
                GameManager.instance.currentGold -= GameManager.instance.charClassArray[index].charArray[1].charCost;

            }
        }
    }    
    public void CreateUnit2Btn(int index)
    {
        if (GameManager.instance.currentGold >= GameManager.instance.charClassArray[index].charArray[2].charCost)
        {
            if (createBlockCount < createBlockMaxCount)
            {
                createBlockCount++;
                createBlock[createBlockCount-1].SetActive(true);
                factory[createBlockCount - 1].chars.charArray = GameManager.instance.charClassArray[index].charArray;
                factory[createBlockCount - 1].index = index;
                factory[createBlockCount - 1].num = 2;
                factory[createBlockCount - 1].createTime = GameManager.instance.charClassArray[index].charArray[2].charTrainingTime;
                GameManager.instance.currentGold -= GameManager.instance.charClassArray[index].charArray[2].charCost;

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
                 if (!tag.GetComponent<TowerFrame>().isUse)
                 {
                     save[index] = tag.GetComponentsInChildren<SpriteRenderer>()[1];
                     save[index].gameObject.transform.localScale = new Vector2(2f, 2f);
                 }
            }
            else if (save[index])
                save[index].gameObject.transform.localScale = Vector2.zero;
        
    }


    IEnumerator TowerCreateCorutine(int index, int num)
    {
        var btn = EventSystem.current.currentSelectedGameObject.GetComponent<TowerButton>();
        mouseOnPrefab.GetComponent<SpriteRenderer>().sprite = btn.towerImage;
        mouseOnPrefab.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.7f);
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

            var tower =  Physics2D.RaycastAll(mousePos, Vector2.zero, 0f, LayerMask.GetMask("Tower"));
            var fraems = Physics2D.CircleCast(mousePos, 0.1f, Vector2.zero, 0, LayerMask.GetMask("TowerFrame"));

                if(fraems)
                {
                    saveSell = fraems.transform.GetComponentsInChildren<SpriteRenderer>()[1];
                    saveSell.gameObject.transform.localScale = new Vector2(2f, 2f);

                }
                else if(saveSell)
                    saveSell.gameObject.transform.localScale = Vector2.zero;

            if (tower.Length > 0)
            {
                var target = Physics2D.RaycastAll(mousePos, Vector2.zero, 0f, LayerMask.GetMask("Tower"));
                var targetTower = target[0].transform.GetComponent<Tower>();
                towerSellText.text = targetTower.towerSellCost.ToString();



                if (Input.GetMouseButtonDown(0))
                {
                    
                    if (fraems)
                    {
                        var on = fraems.transform.GetComponent<TowerFrame>();
                        on.isUse = false;
                    }
                  
                    GameManager.instance.currentGold += targetTower.towerSellCost;
                    GameManager.instance.mikataBase.currentTowerCount--;
                    Destroy(targetTower.gameObject);
                    towerSellImage.SetActive(false);
                    saveSell.gameObject.transform.localScale = Vector2.zero;
                    break;
                }
            }
            else
                towerSellText.text = "";



            if (Input.GetMouseButtonDown(1))
            {
                towerSellImage.SetActive(false);
                saveSell.gameObject.transform.localScale = Vector2.zero;
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
                    GameManager.instance.audioManager.PlayerSfx(AudioManager.Sfx.SKILL3);

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
            if(skill3Count > 10)
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
            if (skill0Count > 30)
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
            fill.fillAmount += Time.deltaTime * 0.2f;
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
