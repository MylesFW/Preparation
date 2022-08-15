using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{

    public float thrust;
    public float knockTime;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody2D Enemy = other.GetComponent<Rigidbody2D>();
            if (Enemy != null)
            {
                Enemy.GetComponent<WorldEnemy>().currentState = EnemyState.stagger;
                Vector2 difference = Enemy.transform.position - transform.position;
                difference = difference.normalized * thrust;
                Enemy.AddForce(difference, ForceMode2D.Impulse);
                StartCoroutine (KnockCo(Enemy));
                Debug.Log("Coroutine Started - KnockBack");
            }
        }
    }

    private IEnumerator KnockCo(Rigidbody2D Enemy)
    {
        if (Enemy != null)
        {
            yield return new WaitForSeconds(knockTime);
            Enemy.velocity = Vector2.zero;
            Enemy.GetComponent<WorldEnemy>().currentState = EnemyState.idle;
        }
    }
}
