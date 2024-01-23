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
    protected bool iscol;


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
            if (transform.GetChild(0).GetComponent<Collider2D>())
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
        if (!GameManager.instance.isLive)
            return;
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
        if(isLive)
        {
            transform.position = new Vector3(transform.position.x, -3.5f);
        }
        if (hpBar)
        {
            hpBar.GetComponent<RectTransform>().transform.position = new Vector2(transform.position.x, transform.position.y + 0.6f);
            if (charCurrentHP < charMaxHP)
            {
                hpBar.value = charCurrentHP / charMaxHP;
            }
        }
    }
    public void OnMeleeCol()
    {
        StartCoroutine(OnMeleeColCorutine());
        switch (charId)
        {
            case 0:
                GameManager.instance.audioManager.PlayerSfx(AudioManager.Sfx.SWING0);
                break;
            case 2:
                GameManager.instance.audioManager.PlayerSfx(AudioManager.Sfx.SPEAR);
                break;
            case 3:
                GameManager.instance.audioManager.PlayerSfx(AudioManager.Sfx.SWING2);
                break;
            case 5:
                GameManager.instance.audioManager.PlayerSfx(AudioManager.Sfx.SPEAR);
                break;
            case 6:
                GameManager.instance.audioManager.PlayerSfx(AudioManager.Sfx.SPEAR);
                break;
            case 8:
                GameManager.instance.audioManager.PlayerSfx(AudioManager.Sfx.FIRE2);
                break;
            case 9:
                GameManager.instance.audioManager.PlayerSfx(AudioManager.Sfx.BLOOD);
                break;
            case 11:
                GameManager.instance.audioManager.PlayerSfx(AudioManager.Sfx.FIRE6);
                break;
            case 12:
                GameManager.instance.audioManager.PlayerSfx(AudioManager.Sfx.SWING5);
                break;

            case 14:
                GameManager.instance.audioManager.PlayerSfx(AudioManager.Sfx.FIRE4);
                break;

        }
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
        switch (charId)
        {
            case 1:
            case 4:
                GameManager.instance.audioManager.PlayerSfx(AudioManager.Sfx.BOW);
                break;
            case 7:
                GameManager.instance.audioManager.PlayerSfx(AudioManager.Sfx.FIRE1);
                break;
            case 8:
                GameManager.instance.audioManager.PlayerSfx(AudioManager.Sfx.FIRE2);
                break;
            case 10:
                GameManager.instance.audioManager.PlayerSfx(AudioManager.Sfx.FIRE3);
                break;

            case 13:
                GameManager.instance.audioManager.PlayerSfx(AudioManager.Sfx.SHOTGUN);
                break;
        }
    }
    IEnumerator OnMeleeColCorutine()
    {
        attackCol.enabled = true;
        yield return new WaitForSeconds(0.05f);
        attackCol.enabled = false;
    }

    protected IEnumerator DeadAnimeCorutine()
    {
        rigid.simulated = false;
        col.enabled = false;
        isBattle = false;
        isMove = false;
        isLive = false;
        Destroy(hpBar.gameObject);

        anime.SetBool("Run", false);
        anime.SetTrigger("Die");
        yield return new WaitForSeconds(deadAnimeTimer);

        Destroy(gameObject);
    }
 

    protected void Movement(charType type)
    {
        if (!isMove ||!isLive)
            return;

            anime.SetBool("Run", true);
            transform.Translate(charMoveSpeed * Time.fixedDeltaTime, 0f, 0f);
        
    }


}
