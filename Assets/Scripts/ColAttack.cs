using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("TekiChar") || collision.CompareTag("MikataChar"))
        {
            var parent = transform.parent.GetComponent<CharBase>();
            var target = collision.GetComponent<CharBase>();
            target.currentHP -= parent.damage;
        }
    }
}
