using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public class AIManager : MonoBehaviour
{
    public enum agetype{ AGE0,AGE1,AGE2,AGE3,AGE4};
    int charLevel;
    int count;
    [System.Serializable]
    public class tekiChar
    {
        public CharBase[] charArray;
    }
    [System.Serializable]
    public class tekiTower
    {
        public Tower[] towerArray;
    }
    public tekiChar[] charClassArray;
    public tekiTower[] towerClassArray;
    Tower[] towerQueue;
    // Start is called before the first frame update
    void Start()
    {
        towerQueue = new Tower[4];
        StartCoroutine(SpawnTeki());
        StartCoroutine(Age0Corutine()); 
    }

  
    void CreateFrame(int index)
    {
        GameManager.instance.tekiBase.towerFrame[index].SetActive(true);
        GameManager.instance.tekiBase.currentTowerFrameCount++;
    }
    void CreateTower(int num,int slot)
    {
        switch(slot)
        {
            case 0:
                //12.2f , -1.7f
                towerQueue[slot] = Instantiate(towerClassArray[GameManager.instance.tekiBase.currentLevel].towerArray[num], null);
                towerQueue[slot].transform.position = new Vector3(12.2f, -1.7f);
                break;
            case 1:
                //12.2f, -0.5f
                towerQueue[slot] = Instantiate(towerClassArray[GameManager.instance.tekiBase.currentLevel].towerArray[num], null);
                towerQueue[slot].transform.position = new Vector3(12.2f, -0.5f);
                break;
            case 2:
                //12.2f, 0.5f
                towerQueue[slot] = Instantiate(towerClassArray[GameManager.instance.tekiBase.currentLevel].towerArray[num], null);
                towerQueue[slot].transform.position = new Vector3(12.2f,0.5f);
                break;
            case 3:
                //12.2f, 1.6f
                towerQueue[slot] = Instantiate(towerClassArray[GameManager.instance.tekiBase.currentLevel].towerArray[num], null);
                towerQueue[slot].transform.position = new Vector3(12.2f, 1.6f);
                break;
        }
    }
    void SellTower(int slot)
    {
        if (towerQueue[slot] != null)
             Destroy(towerQueue[slot].gameObject);
    }
    void CreateTeki()
    {
        var type = Random.Range(0, charLevel + 1);

        var trans = Instantiate(charClassArray[GameManager.instance.tekiBase.currentLevel].charArray[type], null);
        trans.transform.position = new Vector2(10f, -3.5f);
    }
    IEnumerator Age0Corutine()
    {
        while(true)
        {
            count++;

            switch (count)
            {
                case 25:
                    //turret0 slot0
                    CreateTower(0, 0);
                    break;
                case 37:
                    //unit upgrade
                    charLevel++;
                    break;
                case 100:
                    //sell slot0, turret1 slot0
                    SellTower(0);
                    CreateTower(1, 0);

                    break;
                case 125:
                    //unit upgrade
                    charLevel++;
                    break;
                case 150:
                    //sell slot0, turret2 slot0
                    SellTower(0);
                    CreateTower(2, 0);
                    break;
            }
            if(count == 200)
            {
                //age upgrade
                count = 0;
                charLevel = 0;
                GameManager.instance.AgeUp();
                StartCoroutine(Age1Corutine());
                break;
            }
            yield return new WaitForSeconds(1f);

        }
    }

    IEnumerator Age1Corutine()
    {
        while (true)
        {
            count++;
            Debug.Log(count);

            switch (count)
            {
                case 25:
                    //sell slot0, turret0 slot0
                    SellTower(0);
                    CreateTower(0, 0);
                    break;
                case 37:
                    //unit upgrade
                    charLevel++;
                    break;
                case 100:
                    //buy slot1, sell slot0 ,turret2 slot0
                    CreateFrame(1);
                    SellTower(0);
                    CreateTower(2, 0);
                    break;
                case 125:
                    //unit upgrade
                    charLevel++;

                    break;
                case 150:
                    //turret1 slot1
                    CreateTower(1, 1);
                    break;

            }
            if(count == 200)
            {
                //age upgrade
                count = 0;
                GameManager.instance.AgeUp();
                charLevel = 0;
                StartCoroutine(Age2Corutine());
                break;
            }
            yield return new WaitForSeconds(1f);

        }
    }

    IEnumerator Age2Corutine()
    {
        while (true)
        {
            count++;
            Debug.Log(count);

            switch (count)
            {
                case 25:
                    //sell slot0, turret0 slot0
                    SellTower(0);
                    CreateTower(0, 0);
                    break;
                case 37:
                    //unit upgrade
                    charLevel++;

                    break;
                case 100:
                    //buy slot, sell slot1, turret0 slot1
                    CreateFrame(2);
                    SellTower(1);
                    CreateTower(0, 1);
                    break;
                case 125:
                    //unit upgrade
                    charLevel++;

                    break;
                case 150:
                    //sell slot0, sell slot1, turret2 slot2
                    SellTower(0);
                    SellTower(1);
                    CreateTower(2, 2);
                    break;

            }
            if (count == 200)
            {
                //age upgrade
                count = 0;
                GameManager.instance.AgeUp();
                charLevel = 0;
                StartCoroutine(Age3Corutine());
                break;
            }
            yield return new WaitForSeconds(1f);

        }
    }

    IEnumerator Age3Corutine()
    {
        while (true)
        {
            count++;
            Debug.Log(count);

            switch (count)
            {
                case 37:
                    //unit upgrade
                    charLevel++;

                    break;
                case 125:
                    //unit upgrade
                    charLevel++;

                    break;
                case 175:
                    //sell slot0 sell slot2, turret1 slot1
                    SellTower(0);
                    SellTower(2);
                    CreateTower(1, 1);
                    break;
            }
            if (count == 200)
            {
                //age upgrade
                count = 0;
                GameManager.instance.AgeUp();
                charLevel = 0;
                StartCoroutine(Age4Corutine());
                break;
            }
            yield return new WaitForSeconds(1f);

        }
    }

    IEnumerator Age4Corutine()
    {
        while (true)
        {
            count++;
            Debug.Log(count);

            switch (count)
            {
                case 37:
                    //unit upgrade
                    charLevel++;

                    break;
                case 125:
                    //unit upgrade
                    charLevel++;

                    break;
                case 300:
                    //sell slot0, sell slot1, sell slot2, turret1 slot 1
                    SellTower(0);
                    SellTower(1);
                    SellTower(2);
                    CreateTower(1, 1);
                    break;
                case 500:
                    //buy slot3, sell all slots, turret2 slot 3
                    CreateFrame(3);
                    SellTower(0);
                    SellTower(1);
                    SellTower(2);
                    CreateTower(2, 3);
                    CreateTower(1, 2);
                    count = 0;
                    break;
            }
            yield return new WaitForSeconds(1f);

        }
    }
    IEnumerator SpawnTeki()
    {
        yield return new WaitForSeconds(1f);
        float ran = Random.Range(0f, 1f);
        if (ran < 0.3f && TekiChar.count < 6)
        {
            CreateTeki();
        }
        StartCoroutine(SpawnTeki());
    }


}
