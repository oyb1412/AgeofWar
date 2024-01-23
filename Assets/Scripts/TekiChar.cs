using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TekiChar : CharBase
{
    // Start is called before the first frame update
    public GameObject getGoldImage;
    public Text getGoldText;
    public static int count;
    GameObject gold;

    private void Awake()
    {
        count++;

    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;
        Movement(charType.TEKI);
    }

    protected IEnumerator TekiDeadAnimeCorutine()
    {
        rigid.simulated = false;
        col.enabled = false;
        isBattle = false;
        isMove = false;
        isLive = false;

        anime.SetBool("Run", false);
        anime.SetTrigger("Die");
        GameManager.instance.baseCurrentExp += charKillXP;
        GameManager.instance.currentGold += charKillGold;
        gold = Instantiate(getGoldImage, GameObject.Find("WorldCanvas").transform);
        gold.transform.position = new Vector2(transform.position.x, transform.position.y);
        getGoldText = gold.GetComponentInChildren<Text>();
        getGoldText.text = string.Format("+ {0}g", charKillGold);
        yield return new WaitForSeconds(deadAnimeTimer);

        count--;
        if(gold)
        Destroy(gold);
        if (hpBar != null)
            Destroy(hpBar.gameObject);
        Destroy(gameObject);
    }

    override protected void Update()
    {
        if (!GameManager.instance.isLive)
            return;
        if (charCurrentHP <= 0 && isLive)
        {
            anime.SetBool("Run", false);

            isBattle = false;
            isMove = false;
            if (charId < 3)
                GameManager.instance.audioManager.PlayerSfx(AudioManager.Sfx.DIE1);
            else if(charId == 3 || charId == 4)
                GameManager.instance.audioManager.PlayerSfx(AudioManager.Sfx.DIE2);
            else if(charId == 5)
                GameManager.instance.audioManager.PlayerSfx(AudioManager.Sfx.HORSE_DIE);
            else if(charId >= 3 && charId <= 8)
                GameManager.instance.audioManager.PlayerSfx(AudioManager.Sfx.DIE3);
            else if(charId == 9 || charId == 10)
                GameManager.instance.audioManager.PlayerSfx(AudioManager.Sfx.DIE4);
            else if(charId == 11)
                GameManager.instance.audioManager.PlayerSfx(AudioManager.Sfx.FIRE4);
            else if(charId == 12 || charId == 13)
                GameManager.instance.audioManager.PlayerSfx(AudioManager.Sfx.DIE5);
            else if(charId == 14)
                GameManager.instance.audioManager.PlayerSfx(AudioManager.Sfx.FIRE6);






            StartCoroutine(TekiDeadAnimeCorutine());
        }
        
        if(gold != null)
            gold.transform.Translate(0f, 0.8f * Time.deltaTime, 0f);


        if (UnitType == unitType.RANGE)
        {
            LayerMask lay = LayerMask.GetMask("Mikata");

            var target = Physics2D.CircleCast(transform.position, charAttackRange, Vector2.zero, 0, lay);
            if (target && Vector2.Distance(transform.position, target.transform.position) <= charAttackRange)
            {
                isBattle = true;
                anime.SetBool("Run", false);
                isMove = false;
            }
            else if(!iscol)
            {
                isBattle = false;
                isMove = true;
                anime.SetBool("Run", true);
            }
        }
        base.Update();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("MikataChar") || collision.collider.CompareTag("MikataBase"))
        {
            isBattle = true;
            isMove = false;
            iscol = true;
        }
        if (collision.collider.CompareTag("TekiChar"))
        {
            if (collision.transform.position.x < transform.position.x)
            {
                isMove = false;
                iscol = true;

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
            iscol = false;

            anime.SetBool("Run", true);
        }
        else if (collision.collider.CompareTag("TekiChar"))
        {
            isMove = true;
            iscol = false;

            anime.SetBool("Run", true);
        }
    }
}
