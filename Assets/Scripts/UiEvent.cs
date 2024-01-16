using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiEvent : MonoBehaviour
    , IPointerEnterHandler
    , IPointerExitHandler
{
    public Text infoText;
    GameObject data;
    public void OnPointerEnter(PointerEventData eventData)
    {
        data = eventData.pointerCurrentRaycast.gameObject;
        if (data)
        {
            data.GetComponent<Image>().color = Color.gray;
            data.transform.localScale = new Vector3(1.1f, 1.1f, 1f);
            
            switch(data.name)
            {
                case "Unit1Btn":
                    infoText.text = "summon melee unit \n-" + UiManager.Instance.level0_CharPrefabs[0].useGold + "g";
                    break;
                case "Unit2Btn":
                    infoText.text = "summon range unit \n-" + UiManager.Instance.level0_CharPrefabs[1].useGold + "g";
                    break;
                case "Unit3Btn":
                    infoText.text = "summon armor unit \n-" + UiManager.Instance.level0_CharPrefabs[2].useGold + "g";
                    break;
                case "TowerBtn":
                    //infoText.text = "Create Tower \n" + UiManager.Instance.level0_TowerPrefabs[0].useGold + "g";
                    break;
                case "SellBtn":
                    //infoText.text = "Sell Tower \n +" + UiManager.Instance.level0_TowerPrefabs[0].useGold + "g";
                    break;
                case "UpgradeBtn":
                    infoText.text = "Upgrade Level \n-" + GameManager.instance.level1NeedExp + "exp";
                    break;
            }
            infoText.gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (data)
        {
            data.GetComponent<Image>().color = Color.white;
            data.transform.localScale = Vector3.one;
            infoText.gameObject.SetActive(false);

        }
    }


}
