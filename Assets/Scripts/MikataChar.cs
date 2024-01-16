using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MikataChar : CharBase
{
    // Start is called before the first frame update


    private void FixedUpdate()
    {
        Movement(charType.MIKATA);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("TekiChar"))
        {
            isMove = false;
            if (!isBattle)
            {
                anime.SetBool("Run", false);
                isBattle = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("TekiChar") ||
            collision.collider.CompareTag("MikataChar"))
        {
            isMove = true;
            isBattle = false;
            anime.SetBool("Run", true);

        }
    }
}
