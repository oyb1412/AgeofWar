using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ColAttack : MonoBehaviour
{
    public GameObject bloodPrefab;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TekiChar") || collision.CompareTag("MikataChar"))
        {
            var parent = transform.parent.GetComponent<CharBase>();
            var target = collision.GetComponent<CharBase>();
            target.charCurrentHP -= parent.charDamage;
            var trans = Instantiate(bloodPrefab, null).transform;
            trans.position = target.transform.position;
        }
        if (collision.CompareTag("TekiBase"))
        {
            var parent = transform.parent.GetComponent<CharBase>();
            var target = GameManager.instance.tekiBase;
            target.currentHp -= parent.charDamage;

        }
        if (collision.CompareTag("MikataBase"))
        {
            var parent = transform.parent.GetComponent<CharBase>();
            var target = GameManager.instance.mikataBase;

            target.currentHp -= parent.charDamage;
        }
    }
}
