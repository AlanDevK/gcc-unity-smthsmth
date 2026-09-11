using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    Rigidbody2D rb;
    void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }
    void Update(){
        rb.linearVelocity = transform.up * bulletSpeed;
    }
    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Obstacles")){
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Enemies")){
            other.GetComponent<EnemyMovement>().TakeDamage();
            Destroy(gameObject);
        }
    }
}
