using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class MikataChar : CharBase
{
    public bool skill2On;
    public string charName;
    public GameObject skill2Prefabs;
    GameObject skill2Object;
    private void Awake()
    {
        skill2Object = Instantiate(skill2Prefabs, transform);
        skill2Object.gameObject.SetActive(false);
    }
    private void FixedUpdate()
    {
        Movement(charType.MIKATA);
    }

    override protected void Update()
    {
        if(skill2On)
        {
            skill2Object.transform.position = new Vector2(transform.position.x, transform.position.y - 0.3f);
            skill2Object.SetActive(true);
            currentHP += Time.deltaTime;
        }
        if (UnitType == unitType.RANGE)
        {
            LayerMask lay = LayerMask.GetMask("Teki");
            var target = Physics2D.CircleCast(transform.position, attackRange, Vector2.zero, 0, lay);
            if (target)
            {
                isBattle = true;
                anime.SetBool("Run", false);
                isMove = false;
            }
        }
        base.Update();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("TekiChar") || collision.collider.CompareTag("TekiBase"))
        {
            Debug.Log("Ãæµ¹ÇÔ");
            isBattle = true;
            isMove = false;
        }
        else if(collision.collider.CompareTag("MikataChar"))
        {
            if(collision.transform.position.x > transform.position.x)
            {
                isMove = false;
                anime.SetBool("Run", false);
            }
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("TekiChar"))
        {
            isBattle = false;
            isMove = true;
            anime.SetBool("Run", true);
        }
        else if (collision.collider.CompareTag("MikataChar"))
        {
            if (collision.transform.position.x > transform.position.x)
            {
                isMove = true;
                anime.SetBool("Run", true);

            }
        }
    }
}
