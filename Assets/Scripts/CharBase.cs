using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum charType { MIKATA, TEKI}
public enum unitType { MELEE, RANGE}
public class CharBase : MonoBehaviour
{
    [Header("--BaseInfo")]
    public charType CharType;
    public unitType UnitType;
    public float speed;
    public float damage;
    public float attackSpeed;
    public float attackRange;
    protected float attackTimer;
    public float currentHP;
    public float maxHP;
    public float createTime;
    float deadAnimeTimer = 0.8f;
    float attackColTimer = 0.1f;
    public int useGold;
    public bool isLive;
    public bool isMove;
    public bool isBattle;
    protected Animator anime;
    public Collider2D attackCol;
    Collider2D col;
    Rigidbody2D rigid;
    public GameObject bulletPrefabs;
    public Slider hpBarPrefabs;
    public Slider hpBar;

    // Start is called before the first frame update
    protected void Start()
    {
        anime = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        rigid = GetComponent<Rigidbody2D>();
        currentHP = maxHP;
        isLive = true;
        isMove = true;
        if (UnitType == unitType.MELEE)
        {
            attackCol = transform.GetChild(0).GetComponent<Collider2D>();
            attackCol.enabled = false;
        }
        hpBar = Instantiate(hpBarPrefabs, GameObject.Find("WorldCanvas").transform);
        hpBar.gameObject.SetActive(false);
    }

    virtual protected void Update()
    {
        if (isBattle)
        {
            anime.SetBool("Run", false);
            attackTimer += Time.deltaTime;
            if (attackTimer > attackSpeed)
            {
                anime.SetTrigger("Attack");
                attackTimer = 0;
            }
        }

        if (currentHP <= 0)
        {
            anime.SetBool("Run", false);
            isBattle = false;
            isMove = false;
            StartCoroutine(DeadAnimeCorutine());
        }
        hpBar.GetComponent<RectTransform>().transform.position = new Vector2(transform.position.x, transform.position.y + 0.6f);
        if (currentHP < maxHP)
        {
            hpBar.value = currentHP / maxHP;
        }
    }
    public void OnMeleeCol()
    {
        StartCoroutine(OnMeleeColCorutine());
    }

    public void OnRangeAttack()
    {
        var bullet = Instantiate(bulletPrefabs,transform.position, Quaternion.identity);
        switch(CharType)
        {
            case charType.MIKATA:
                bullet.GetComponent<CharBullet>().Init(damage, (int)charType.MIKATA);

                break;
            case charType.TEKI:
                bullet.GetComponent<CharBullet>().Init(damage, (int)charType.TEKI);

                break;
        }
    }
    IEnumerator OnMeleeColCorutine()
    {
        attackCol.enabled = true;
        yield return new WaitForSeconds(attackColTimer);
        attackCol.enabled = false;
    }

    IEnumerator DeadAnimeCorutine()
    {
        rigid.simulated = false;
        col.enabled = false;
        isBattle = false;
        isMove = false;
        hpBar.gameObject.SetActive(false);
        anime.SetBool("Run", false);
        anime.SetTrigger("Die");
        yield return new WaitForSeconds(deadAnimeTimer);
        Destroy(gameObject);
    }
 

    protected void Movement(charType type)
    {
        if (!isMove)
            return;

        anime.SetBool("Run", true);
        transform.Translate(speed * Time.fixedDeltaTime, 0f, 0f);
    }


}
