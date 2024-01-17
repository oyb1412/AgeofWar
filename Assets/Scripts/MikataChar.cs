using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class MikataChar : CharBase
{

    private void FixedUpdate()
    {
        Movement(charType.MIKATA);
    }

    override protected void Update()
    {
        if (UnitType == unitType.RANGE)
        {
            var target = Physics2D.CircleCast(transform.position, attackRange, Vector2.zero, 0, LayerMask.GetMask("Teki"));
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
        if(collision.collider.CompareTag("TekiChar"))
        {
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
