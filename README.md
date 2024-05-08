## **📃핵심 기술**

### ・유닛 예약 생성 시스템

🤔**WHY?**

예약 순서대로 자연스럽게 유닛을 생성하는 로직의 필요성

🤔**HOW?**

 관련 코드

- UiManager
    
    ```csharp
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.EventSystems;
    public class UiManager : MonoBehaviour
    {
        [Header("Other")]
        public float currentCreateTime;
        public int createBlockCount;
        public int createBlockMaxCount;
        
        [System.Serializable]
        public class Factory
        {
            public GameManager.Chars chars;
            public int index;
            public int num;
            public float createTime;
        }
    
        public Factory[] factory;
        
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
    }
    ```
    

🤓**Result!**

유닛의 생성을 즉발이 아닌 생성 시간이 완료되야 생성되고, 미리 예약한 순서대로 생성되도록 변경

### ・카메라 이동 및 카메라 쉐이크

🤔**WHY?**

카메라에 모두 담기지 않는, 넓은 크기의 씬과 각종 효과에 의한 카메라쉐이크의 필요성

🤔**HOW?**

 관련 코드

- CameraMovemnet
    
    ```csharp
    using System.Collections;
    using UnityEngine;
    
    public class CameraMovemnet : MonoBehaviour
    {
        public float cameraSpeed;
        public float rimitLeft;
        public float rimitRight;
        Vector3 currentPos;
    
        void Update()
        {
            if (!GameManager.instance.isLive)
                return;
            Move();
        }
    
        void Move()
        {
            var mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (transform.position.x <= rimitLeft)
            {
                if (mousepos.x <= rimitLeft)
                    mousepos.x = rimitLeft;
            }
            else if (transform.position.x >= rimitRight)
            {
                if (mousepos.x >= rimitRight)
                    mousepos.x = rimitRight;
            }
    
            if(mousepos.y < 1f)
            transform.position = Vector3.Lerp(transform.position, new Vector3(mousepos.x, 0f, -10f), cameraSpeed * Time.deltaTime);
        }
    
        public void ScreenShake()
        {
            currentPos = transform.position;
            StartCoroutine(Shake());
        }
    
        IEnumerator Shake()
        {
            float elapsedTime = 0f;
    
            while (elapsedTime < 2f)
            {
                Vector3 shakeOffset = Random.insideUnitSphere * 0.1f;
    
                transform.position = currentPos + shakeOffset;
    
                elapsedTime += Time.deltaTime;
    
                yield return null;
            }
    
        }
    }
    ```
    

🤓**Result!**

마우스를 외곽에 위치시켜 카메라를 각각 왼쪽, 오른쪽으로 Lerp효과를 주며 이동시켜, 부드러운 카메라 이동을 구현. 스킬 사용 등의 임팩트가 필요한 순간, 카메라를 일정시간동안 흔들어 연출력을 높임

### ・OnPoint인터페이스를 활용한 UI 인풋 시스템

🤔**WHY?**

UI에 마우스 커서를 올릴 시 특정 Text를 출력하는 등 UI와 마우스의 상호작용 구현의 필요성

🤔**HOW?**

 관련 코드

- UiEvent
    
    ```csharp
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
    ```
    

🤓**Result!**

지정한 UI에 마우스 커서를 가져다 대거나 땔 경우 지정해둔 Text가 출력되거나 사라지는등 UI와 마우스의 상호작용을 구현

### ・OnPoint인터페이스를 활용한 UI 인풋 시스템

🤔**WHY?**

UI에 마우스 커서를 올릴 시 특정 Text를 출력하는 등 UI와 마우스의 상호작용 구현의 필요성

🤔**HOW?**

 관련 코드

- UiEvent
    
    ```csharp
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
    ```
    

🤓**Result!**

지정한 UI에 마우스 커서를 가져다 대거나 땔 경우 지정해둔 Text가 출력되거나 사라지는등 UI와 마우스의 상호작용을 구현
