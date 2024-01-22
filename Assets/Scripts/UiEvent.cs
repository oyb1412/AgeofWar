using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiEvent : MonoBehaviour
    , IPointerEnterHandler
    , IPointerExitHandler
{
    GameObject data;
    public Text mouseOnText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        var basedata = GameManager.instance.data;

        if (eventData.pointerCurrentRaycast.gameObject)
        {
            data = eventData.pointerCurrentRaycast.gameObject;
            if(data.GetComponent<Image>())
                data.GetComponent<Image>().color = Color.gray;
            switch (data.name)
            {
                case "UnitBtn":
                    mouseOnText.text = "train unit menu";
                    break;
                case "TowerBtn":
                    mouseOnText.text = "build turret menu";
                    break;
                case "SellBtn":
                    mouseOnText.text = "sell a turret";
                    break;
                case "AddBtn":
                    if (GameManager.instance.mikataBase.currentTowerFrameCount == GameManager.instance.maxTowerFrameCount)
                        mouseOnText.text = "can't build anymore";
                    else
                        mouseOnText.text = basedata.slot_cost[GameManager.instance.mikataBase.currentTowerFrameCount - 1] + "$ - add a turret spot";
                    break;
                case "LevelUpBtn":
                    if(GameManager.instance.mikataBase.currentLevel == GameManager.instance.baseMaxLevel)
                        mouseOnText.text = "you cannot evolve more anymore";
                    else
                        mouseOnText.text = basedata.xp_cost[GameManager.instance.mikataBase.currentLevel] + "xp - Evolve to next age";
                    break;
                case "Return":
                    mouseOnText.text = "return to previous menu";
                    break;

                case "Tower":
                    var tower = data.GetComponent<TowerButton>();
                    mouseOnText.text = tower.towerCost + "$ - " + tower.towerName;
                    break;

                case "Unit":
                    var unit = data.GetComponent<UnitButton>();
                    mouseOnText.text = unit.unitCost + "$ - " + unit.unitName;
                    break;
            }

          
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (data)
        {
            if (data.GetComponent<Image>())
                data.GetComponent<Image>().color = Color.white;

            mouseOnText.text = "";

        }
    }
}
