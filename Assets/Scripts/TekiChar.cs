using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TekiChar : CharBase
{
    // Start is called before the first frame update

    private void FixedUpdate()
    {
        Movement(charType.TEKI);
    }

    override protected void Update()
    {
        if (UnitType == unitType.RANGE)
        {
            var target = Physics2D.CircleCast(transform.position, attackRange, Vector2.zero, 0, LayerMask.GetMask("Mikata"));
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
        if (collision.collider.CompareTag("MikataChar"))
        {
            isBattle = true;
            isMove = false;
        }
        else if (collision.collider.CompareTag("TekiChar"))
        {
            if (collision.transform.position.x < transform.position.x)
            {
                isMove = false;
                anime.SetBool("Run", false);
            }
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("MikataChar"))
        {
            isBattle = false;
            isMove = true;
            anime.SetBool("Run", true);
        }
        else if (collision.collider.CompareTag("TekiChar"))
        {
            if (collision.transform.position.x < transform.position.x)
            {
                isMove = true;
                anime.SetBool("Run", true);

            }
        }
    }
}
