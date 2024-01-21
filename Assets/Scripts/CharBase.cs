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
    protected float deadAnimeTimer = 0.8f;
    float attackColTimer = 0.1f;
    public bool isLive;
    public bool isMove;
    public bool isBattle;
    protected Animator anime;
    public Collider2D attackCol;
    protected Collider2D col;
    protected Rigidbody2D rigid;
    public GameObject bulletPrefabs;
    
    public Slider hpBarPrefabs;
    public Slider hpBar;

    [Header("CharBehavior")]
    public bool mikataFrontCol;
    public bool mikataBackCol;


    [Header("CharInfo")]
    public int charId;
    public string charName;
    public int charCost;
    public float charTrainingTime;
    public float charMaxHP;
    public float charDamage;
    public float charAttackSpeed;
    public float charCurrentHP;
    public float charAttackRange;
    public float charMoveSpeed;
    public float charAttackTimer;
    public float charKillXP;
    public float charKillGold;
    // Start is called before the first frame update
    protected void Start()
    {
        CharInit();
        anime = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        rigid = GetComponent<Rigidbody2D>();
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
    void CharInit()
    {
        var data = GameManager.instance.data;
        charName = data.charName[charId];
        charCost = data.charCost[charId];
        charTrainingTime = data.charTraningTime[charId];
        charMaxHP = data.charMaxHP[charId];
        charCurrentHP = charMaxHP;
        charDamage = data.charDamGE[charId];
        if (CharType == charType.TEKI)
        {
            charMoveSpeed = -1 * data.charMoveSpeed;
            charKillGold = data.tekiCharKillGold[charId];
            charKillXP = charKillGold * 2;
        }
        else
            charMoveSpeed = data.charMoveSpeed;

        charAttackRange = data.charAttackRange;
        if(UnitType == unitType.MELEE)
        {
            charAttackSpeed = data.charMeleeAttackSpeed[charId];
        }
        else
        {
            charAttackSpeed = data.charRangeAttackSpeed[charId];
        }
    }

    virtual protected void Update()
    {
        if (isBattle)
        {
            anime.SetBool("Run", false);
            charAttackTimer += Time.deltaTime;
            if (charAttackTimer > charAttackSpeed)
            {
                anime.SetTrigger("Attack");
                charAttackTimer = 0;
            }
        }


        hpBar.GetComponent<RectTransform>().transform.position = new Vector2(transform.position.x, transform.position.y + 0.6f);
        if (charCurrentHP < charMaxHP)
        {
            hpBar.value = charCurrentHP / charMaxHP;
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
                bullet.GetComponent<CharBullet>().Init(charDamage, (int)charType.MIKATA);

                break;
            case charType.TEKI:
                bullet.GetComponent<CharBullet>().Init(charDamage, (int)charType.TEKI);

                break;
        }
    }
    IEnumerator OnMeleeColCorutine()
    {
        attackCol.enabled = true;
        yield return new WaitForSeconds(attackColTimer);
        attackCol.enabled = false;
    }

    protected IEnumerator DeadAnimeCorutine()
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
        Destroy(gameObject);
    }
 

    protected void Movement(charType type)
    {
        if (!isMove)
            return;

            anime.SetBool("Run", true);
            transform.Translate(charMoveSpeed * Time.fixedDeltaTime, 0f, 0f);
        
    }


}
