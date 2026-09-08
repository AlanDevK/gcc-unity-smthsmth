using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    InputAction moveInput;
    InputAction jumpInput;
    InputAction attackInput;
    [SerializeField] float speed = 20f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float rayLength = 0.5f;
    [SerializeField] LayerMask groundLayer;
    float moveDir;
    bool canAttack = true;
    float attackCooldown = 0.3f;
    int count = 0;
    Rigidbody2D rb;

    void Awake(){
        moveInput = InputSystem.actions.FindAction("MovePlatformer");
        jumpInput = InputSystem.actions.FindAction("Jump");
        attackInput = InputSystem.actions.FindAction("Attack");
        rb = GetComponent<Rigidbody2D>();
    }
    void Update(){
        Debug.DrawRay(transform.position, Vector2.down * rayLength, Color.red);
        moveDir = moveInput.ReadValue<float>();
        rb.linearVelocityX = moveDir * speed;
        bool hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, groundLayer);
        if (jumpInput.WasPressedThisFrame() && hit){
            rb.linearVelocityY = jumpForce;
        }
        if (attackInput.WasPressedThisFrame() && canAttack){
            Debug.Log("Attacking!");
            StartCoroutine(AttackCooldown());
        }
        else if (attackInput.WasPressedThisFrame() && !canAttack){
            Debug.Log("Attack is on cooldown!");
        }

    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Collectibles")){
            Destroy(other.gameObject);
            count++;
            Debug.Log($"Nguoi choi da nhat {count} xu!");
        }
    }

    IEnumerator AttackCooldown(){
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        Debug.Log("Can attack again!");
    }
}
