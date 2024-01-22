using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TekiChar : CharBase
{
    // Start is called before the first frame update
    public GameObject getGoldImage;
    public Text getGoldText;
    public int getGold;
    public static int count;
    GameObject gold;

    private void Awake()
    {
        count++;
    }

    private void FixedUpdate()
    {
        Movement(charType.TEKI);
    }

    protected IEnumerator TekiDeadAnimeCorutine()
    {
        rigid.simulated = false;
        col.enabled = false;
        isBattle = false;
        isMove = false;
        isLive = false;
        hpBar.gameObject.SetActive(false);
        anime.SetBool("Run", false);
        anime.SetTrigger("Die");
        yield return new WaitForSeconds(deadAnimeTimer);
        GameManager.instance.baseCurrentExp += charKillXP;
        GameManager.instance.currentGold += charKillGold;
        count--;
        Destroy(gold);
        Destroy(gameObject);
    }

    override protected void Update()
    {
        if (charCurrentHP <= 0 && isLive)
        {
            anime.SetBool("Run", false);
            gold = Instantiate(getGoldImage, GameObject.Find("WorldCanvas").transform);
            gold.transform.position = new Vector2(transform.position.x, transform.position.y);
            getGoldText = getGoldImage.GetComponentInChildren<Text>();
            getGoldText.text = "+" + getGold + "g";
            isBattle = false;
            isMove = false;

            StartCoroutine(TekiDeadAnimeCorutine());
        }
        
        if(gold != null)
            gold.transform.Translate(0f, 0.8f * Time.deltaTime, 0f);


        if (UnitType == unitType.RANGE)
        {
            LayerMask lay = LayerMask.GetMask("Mikata");

            var target = Physics2D.CircleCast(transform.position, charAttackRange, Vector2.zero, 0, lay);
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
        if (collision.collider.CompareTag("MikataChar") || collision.collider.CompareTag("MikataBase"))
        {
            isBattle = true;
            isMove = false;
        }
        if (collision.collider.CompareTag("TekiChar"))
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
                isMove = true;
                anime.SetBool("Run", true);
        }
    }
}
