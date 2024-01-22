using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
public enum towerType { MIKATA, TEKI }
public enum splitType { NORMAL,SPLIT}
public enum parabolaType { NORMAL,PARABOLA}
public class Tower : MonoBehaviour
{
    public towerType towerType;
    public splitType splitType;
    public parabolaType parabolaType;
    public float towerAttackTimer;
    public float bulletSpeed;
    public string towerName;
    public float towerCost;
    public float towerSellCost;
    public float towerAttackSpeed;
    public float towerAttackDamage;
    public float towerAttackRange;
    public int towerId;
    public Vector2 moveDir;
    public GameObject bulletPrefabs;
    RaycastHit2D target;
    public CharBase charTarget;
    Animator anime;
    // Start is called before the first frame update
    private void Start()
    {
        anime = GetComponent<Animator>();
        Init();
    }
    void Init()
    {
        var data = GameManager.instance.data;
        towerName = data.turret_name[towerId];
        towerCost = data.turret_cost[towerId];
        towerAttackSpeed = data.turret_speed[towerId];
        towerAttackDamage = data.turret_damage[towerId];
        towerAttackRange = data.towerAttackRange[towerId];
        towerSellCost = towerCost * 0.7f;
    }
    // Update is called once per frame
    void Update()
    {
        switch (towerType)
        {
            case towerType.MIKATA:
                var type = GameObject.FindGameObjectsWithTag("TekiChar");
                for(int i = 0;i<type.Length; i++)
                {
                    for (int j = 0; j < type.Length -1; j++)
                    {
                        if (type[j].transform.position.x - transform.position.x > type[j+1].transform.position.x - transform.position.x)
                        {
                            var save = type[j];
                            type[j] = type[j + 1];
                            type[j + 1] = save;
                        }
                    }
                }
                if (type.Length > 0)
                {
                    if (type[0].transform.position.x - transform.position.x < towerAttackRange)
                        charTarget = type[0].GetComponent<TekiChar>();
                }
                break;
            case towerType.TEKI:
                var type1 = GameObject.FindGameObjectsWithTag("MikataChar");
                for (int i = 0; i < type1.Length; i++)
                {
                    for (int j = 0; j < type1.Length - 1; j++)
                    {
                        if (transform.position.x - type1[j].transform.position.x > transform.position.x - type1[j + 1].transform.position.x )
                        {
                            var save = type1[j];
                            type1[j] = type1[j + 1];
                            type1[j + 1] = save;
                        }
                    }
                }
                if (type1.Length > 0)
                {
                    if (type1[0].transform.position.x - transform.position.x < towerAttackRange)
                        charTarget = type1[0].transform.GetComponent<MikataChar>();
                }
                break;
                
        }

        if (charTarget != null)
        {
            towerAttackTimer += Time.deltaTime;

            if (towerAttackTimer > towerAttackSpeed)
            {
                float angle;
                if(towerType == towerType.MIKATA)
                    angle = Mathf.Atan2(charTarget.transform.position.y - transform.position.y, charTarget.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
                else
                    angle = Mathf.Atan2(transform.position.y - charTarget.transform.position.y, transform.position.x - charTarget.transform.position.x) * Mathf.Rad2Deg;

                angle = Mathf.Clamp(angle, 0, 70f);
                transform.rotation = Quaternion.Euler(0, 0, angle);
                if (angle > 0 && angle < 70f)
                {
                    anime.SetTrigger("Attack");
                    moveDir = (charTarget.transform.position - transform.position).normalized;
                }
                towerAttackTimer = 0;
            }
        }
    }

    public void OnTowerAttack()
    {
        var bullet = Instantiate(bulletPrefabs, transform);
        if (charTarget)
        {
            if (parabolaType == parabolaType.PARABOLA)
            {
                bullet.GetComponent<TowerBullet>().Init(towerAttackDamage, bulletSpeed, moveDir, towerType, charTarget.transform.position.x, charTarget.transform.position.y);
                bullet.transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, 0f);
            }
            else
            {
                bullet.transform.position = transform.position;
                bullet.GetComponent<TowerBullet>().Init(towerAttackDamage, bulletSpeed, moveDir, towerType);
            }
        }
    }
}
