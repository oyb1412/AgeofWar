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
    GameObject gold;
    private void Awake()
    {
        
    }

    private void FixedUpdate()
    {
        Movement(charType.TEKI);
    }



    override protected void Update()
    {
        if (currentHP <= 0 && isLive)
        {
            gold = Instantiate(getGoldImage, GameObject.Find("WorldCanvas").transform);
            gold.transform.position = new Vector2(transform.position.x + 0.2f, transform.position.y);
            getGoldText = getGoldImage.GetComponentInChildren<Text>();
            getGoldText.text = getGold.ToString();
            gold.transform.Translate(0f, 1f * Time.deltaTime, 0f);
            if (gold.transform.position.y > -2.3f)
            {
                isLive = false;
                Destroy(gold);

            }
        }

        if (UnitType == unitType.RANGE)
        {
            LayerMask lay = LayerMask.GetMask("Mikata");

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
        if (collision.collider.CompareTag("MikataChar") || collision.collider.CompareTag("MikataBase"))
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
