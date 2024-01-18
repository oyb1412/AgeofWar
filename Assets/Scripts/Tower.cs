using UnityEngine;
public enum towerType { MIKATA, TEKI }
public class Tower : MonoBehaviour
{
    public towerType towerType;
    public float attackTimer;
    public float attackSpeed;
    public float attackDamage;
    public float bulletSpeed;
    public float attackRange;
    public Vector2 moveDir;
    public GameObject bulletPrefabs;
    RaycastHit2D target;
    public CharBase charTarget;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        switch (towerType)
        {
            case towerType.MIKATA:
                if (target = Physics2D.CircleCast(transform.position, attackRange, Vector2.zero, 0, LayerMask.GetMask("Teki")))
                    charTarget = target.transform.GetComponent<TekiChar>();
                break;
            case towerType.TEKI:
                if(target = Physics2D.CircleCast(transform.position, attackRange, Vector2.zero, 0, LayerMask.GetMask("Mikata")))
                    charTarget = target.transform.GetComponent<MikataChar>();
                break;
                
        }

        if (charTarget != null)
        {
            attackTimer += Time.deltaTime;
            var angle = Mathf.Atan2(charTarget.transform.position.y - transform.position.y, charTarget.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            if (attackTimer > attackSpeed)
            {
                attackTimer = 0;
                moveDir = (charTarget.transform.position - transform.position).normalized;
                var bullet = Instantiate(bulletPrefabs, transform.position, Quaternion.identity);
                bullet.GetComponent<TowerBullet>().Init(attackDamage, bulletSpeed, moveDir, towerType);
                
            }
        }
    }
}
