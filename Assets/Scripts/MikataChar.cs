using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class MikataChar : CharBase
{
    public bool skill2On;
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
            charCurrentHP += Time.deltaTime * 5f;
        }
        if (UnitType == unitType.RANGE)
        {
            var target = Physics2D.CircleCast(transform.position, charAttackRange, Vector2.zero, 0, LayerMask.GetMask("Teki"));
            if (target)
            {
                isBattle = true;
                isMove = false;
                anime.SetBool("Run", false);
            }
            else
            {
                isBattle = false;
                isMove = true;
                anime.SetBool("Run", true);

            }
        }

        if (charCurrentHP <= 0 && isLive)
        {
            anime.SetBool("Run", false);
            isBattle = false;
            isMove = false;
            StartCoroutine(DeadAnimeCorutine());
        }
        base.Update();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("TekiChar") || collision.collider.CompareTag("TekiBase"))
        {
            isBattle = true;
            isMove = false;
        }
        if(collision.collider.CompareTag("MikataChar"))
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
            isMove = true;
            anime.SetBool("Run", true);

        }
    }
}
