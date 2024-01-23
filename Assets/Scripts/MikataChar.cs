using System.Collections;
using System.Collections.Generic;
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
        if (!GameManager.instance.isLive)
            return;
        Movement(charType.MIKATA);
    }

    override protected void Update()
    {
        if (!GameManager.instance.isLive)
            return;
        if (skill2On)
        {
            skill2Object.transform.position = new Vector2(transform.position.x, transform.position.y - 0.3f);
            skill2Object.SetActive(true);
            charCurrentHP += Time.deltaTime * 5f;
        }
        if (UnitType == unitType.RANGE)
        {
            var target = Physics2D.CircleCast(transform.position, charAttackRange, Vector2.zero, 0, LayerMask.GetMask("Teki"));
            if (target && Vector2.Distance(transform.position, target.transform.position) <= charAttackRange)
            {
                isBattle = true;
                isMove = false;
                anime.SetBool("Run", false);
            }
            else if(!iscol)
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
            if (charId < 3)
                GameManager.instance.audioManager.PlayerSfx(AudioManager.Sfx.DIE1);
            else if (charId == 3 || charId == 4)
                GameManager.instance.audioManager.PlayerSfx(AudioManager.Sfx.DIE2);
            else if (charId == 5)
                GameManager.instance.audioManager.PlayerSfx(AudioManager.Sfx.HORSE_DIE);
            else if (charId >= 3 && charId <= 8)
                GameManager.instance.audioManager.PlayerSfx(AudioManager.Sfx.DIE3);
            else if (charId == 9 || charId == 10)
                GameManager.instance.audioManager.PlayerSfx(AudioManager.Sfx.DIE4);
            else if (charId == 11)
                GameManager.instance.audioManager.PlayerSfx(AudioManager.Sfx.FIRE4);
            else if (charId == 12 || charId == 13)
                GameManager.instance.audioManager.PlayerSfx(AudioManager.Sfx.DIE5);
            else if (charId == 14)
                GameManager.instance.audioManager.PlayerSfx(AudioManager.Sfx.FIRE6);
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
            iscol = true;
        }
        if(collision.collider.CompareTag("MikataChar"))
        {
            if(collision.transform.position.x > transform.position.x)
            {
                isMove = false;
                anime.SetBool("Run", false);
                iscol = true;

            }

        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("TekiChar"))
        {
            isBattle = false;
            isMove = true;
            iscol = false;

            anime.SetBool("Run", true);
        }
        else if (collision.collider.CompareTag("MikataChar"))
        {
            isMove = true;
            iscol = false;

            anime.SetBool("Run", true);

        }
    }
}
