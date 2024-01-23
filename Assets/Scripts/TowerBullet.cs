using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
public class TowerBullet : MonoBehaviour
{
    float damage;
    float speed;
    Rigidbody2D rigid;
    towerType bulletType;
    public splitType splitType;
    public parabolaType parabolaType;
    public GameObject splitBullet;
    public GameObject bloodPrefabs;
    public float limitPos;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    public void Init(float attackDamage, float bulletSpeed, Vector2 moveDir, towerType towerType, float x = 0f, float y = 0f)
    {
        damage = attackDamage;
        speed = bulletSpeed;
        bulletType = towerType;
        if (parabolaType == parabolaType.NORMAL)
        {
            limitPos = -4f;
            rigid.velocity = moveDir * speed;
        }
        else
        {
            if (bulletType == towerType.MIKATA)
            {
                limitPos = y-0.1f;
                transform.DOMoveX(x+0.4f, 1.5f).SetEase(Ease.OutQuad);
                transform.DOMoveY(y-0.2f, 1f).SetEase(Ease.InQuad);
            }
            else
            {
                limitPos = y - 0.1f;
                transform.DOMoveX(x - 0.4f, 1.5f).SetEase(Ease.OutQuad);
                transform.DOMoveY(y - 0.2f, 1f).SetEase(Ease.InQuad);
            }
        }
    }

    private void Update()
    {
        if (!GameManager.instance.isLive)
            return;
        if (transform.position.y < limitPos)
            Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower"))
            return;


        switch(bulletType)
        {
            case towerType.MIKATA:
                if(collision.CompareTag("TekiChar"))
                {
                    var target = collision.GetComponent<TekiChar>();
                    target.charCurrentHP -= damage;
                    var trans = Instantiate(bloodPrefabs, null);
                    trans.transform.position = target.transform.position;

                    if (splitType == splitType.SPLIT)
                    {
                        for(int i = 0; i < 4; i++)
                        {
                            var tr = Instantiate(splitBullet, null);
                            tr.transform.position = transform.position;
                            tr.GetComponent<SpritBullet>().Init(damage, (int)towerType.MIKATA);
                        }
                    }
                    Destroy(gameObject);
                }
                break;
            case towerType.TEKI:
                if (collision.CompareTag("MikataChar"))
                {
                    var target = collision.GetComponent<MikataChar>();
                    target.charCurrentHP -= damage;
                    var trans = Instantiate(bloodPrefabs, null);
                    trans.transform.position = target.transform.position;
                    if (splitType == splitType.SPLIT)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            var tr = Instantiate(splitBullet, null);
                            tr.transform.position = transform.position;
                            tr.GetComponent<SpritBullet>().Init(damage, (int)towerType.TEKI);

                        }
                    }
                    Destroy(gameObject);
                }
                break;
        }
    }
}
