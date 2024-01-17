using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum charType { MIKATA, TEKI}
public class CharBase : MonoBehaviour
{
    [Header("--BaseInfo")]
    public charType CharType;
    public float speed;
    public float damage;
    public float attackSpeed;
    protected float attackTimer;
    public float currentHP;
    public float maxHP;
    public float createTime;
    public int useGold;
    public bool isLive;
    public bool isMove;
    public bool isBattle;
    //protected Animator anime;
    // Start is called before the first frame update
    protected void Start()
    {
        //anime = GetComponent<Animator>();
        currentHP = maxHP;
        isLive = true;
        isMove = true;
    }

    // Update is called once per frame
    protected void Update()
    {
        //OnBattle();
    }

    //protected void OnBattle()
    //{
    //    if (!isBattle)
    //        return;
    //    attackTimer += Time.deltaTime;
    //    if(attackTimer >= attackSpeed)
    //    {
    //        anime.SetTrigger("Attack");
    //        attackTimer = 0;
    //    }
    //}

    protected void Movement(charType type)
    {
        if (!isMove)
            return;

        //anime.SetBool("Run", true);
        if (type == charType.MIKATA)
            transform.Translate(speed * Time.fixedDeltaTime, 0f, 0f);
        else
            transform.Translate(-speed * Time.fixedDeltaTime, 0f, 0f);
    }

    
}
