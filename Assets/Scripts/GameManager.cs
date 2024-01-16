using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int currentTowerCount;
    public int maxTowerCount;
    public float currentGold;
    public float baseCurrentHp;
    public float baseMaxHp;
    public int baseExp;
    public int level1NeedExp;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        level1NeedExp = 1000;
        maxTowerCount = 3;
        baseCurrentHp = baseMaxHp;
    }

    // Update is called once per frame
    void Update()
    {
        currentGold += Time.deltaTime;
    }
}
