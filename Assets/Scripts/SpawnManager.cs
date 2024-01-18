using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public int timer;
    public int[] charCount = {0, 5, 8, 11, 15 };
    public int towerCount;
    public int levelUpCount;
    public int createCount;
    public GameObject[] charPrefabs;

    private void Start()
    {
        StartCoroutine(TimerCorutine());
    }
    private void Update()
    {
       
    }

    IEnumerator TimerCorutine()
    {
        while (true)
        {
            if (timer == charCount[createCount])
            {
                Instantiate(charPrefabs[0], transform.position, Quaternion.identity);
                createCount++;
            }
            timer++;
            yield return new WaitForSeconds(1f);

            if (createCount >= charCount.Length)
                break;
        }
    }
}
