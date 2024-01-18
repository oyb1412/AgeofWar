using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerBullet : MonoBehaviour
{
    float damage;
    float speed;
    Rigidbody2D rigid;
    towerType bulletType;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    public void Init(float attackDamage, float bulletSpeed, Vector2 moveDir, towerType towerType)
    {
        damage = attackDamage;
        speed = bulletSpeed;
        bulletType = towerType;
        rigid.velocity = moveDir * speed;
    }

    // Start is called before the first frame update
  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(bulletType)
        {
            case towerType.MIKATA:
                if(collision.CompareTag("TekiChar"))
                {
                    var target = collision.GetComponent<TekiChar>();
                    target.currentHP -= damage;
                    Destroy(gameObject);
                }
                break;
            case towerType.TEKI:
                if (collision.CompareTag("MikataChar"))
                {
                    var target = collision.GetComponent<MikataChar>();
                    target.currentHP -= damage;
                    Destroy(gameObject);
                }
                break;
        }
    }
}
