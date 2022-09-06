using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{

    public float thrust;
    public float knockTime;
    public float damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("breakable"))
        {
            other.GetComponent<Chair>().Smash();
        }

        if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if (hit != null)
            {
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);
                Debug.Log("Coroutine Started - KnockBack");

                if (other.gameObject.CompareTag("enemy") && other.isTrigger)
                {
                   hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    other.GetComponent<Enemy>().Knock(hit, knockTime, damage);
                }
                if (other.gameObject.CompareTag("Player"))
                {
                    if (other.GetComponent<Player>().currentState != PlayerState.stagger)
                    {
                    hit.GetComponent<Player>().currentState = PlayerState.stagger;
                    other.GetComponent<Player>().Knock(knockTime, damage);
                    }
                }

            }
        }
    }
}
