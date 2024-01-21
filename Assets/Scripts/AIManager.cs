using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIManager : MonoBehaviour
{
//        1000 -> turret1 slot1
  

//    4000 -> sell slot1, turret2 slot1

//    6000 -> sell slot1, turret3 slot1
//   8000 -> upgrade age
//    Age2:
//    1000 -> sell slot1, turret1 slot1
//    4000 -> buy slot, sell slot1 ,turret3 slot1
//    6000 -> turret2 slot2
//    Age3:
//    1000 -> sell slot1, turret1 slot1
//    4000 -> buy slot, sell slot2, turret1 slot2
//    6000 -> sell slot1, sell slot2, turret3 slot3
//    Age4:
//    5000 -> turret1 slot 1
//    7000 -> sell slot1 sell slot3, turret2 slot2
//    Age5:
//    5000 -> turret1 slot1
//    12000 -> sell slot1, sell slot2, sell slot3, turret2 slot 2
//    20000 -> buy slot, sell all slots, turret3 slot 4


//all age 
//  1500 -> unit upgrade
//   5000 -> unit upgrade
// 8000 -> age upgrade

//all status / 40 = second


    int charLevel;
    int baseLevel;
    [System.Serializable]
    public class tekiChar
    {
        public CharBase[] charArray;
    }

    public tekiChar[] charClassArray;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnTeki());
    }

    IEnumerator SpawnTeki()
    {
        yield return new WaitForSeconds(1f);
        float ran = Random.Range(0f, 1f);
        if(ran < 0.3f && TekiChar.count < 6)
        {
            CreateTeki();
        }
        StartCoroutine(SpawnTeki());
    }

    void CreateTeki()
    {
        var type = Random.Range(0, charLevel + 1);

        var trans = Instantiate(charClassArray[baseLevel].charArray[type], null);
        trans.transform.position = new Vector2(10f, -3.5f);
    }
}
