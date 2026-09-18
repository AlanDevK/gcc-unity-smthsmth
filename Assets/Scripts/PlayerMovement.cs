using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    InputAction moveAction;
    InputAction shootAction;
    InputAction aimAction;
    InputAction reloadAction;
    InputAction interactAction;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform spawnPointPos;
    [SerializeField] float moveSpeed = 0.5f;
    Rigidbody2D rb;
    Vector2 moveDir;
    NPC npcScript;
    DoorScript doorScript;
    float dialogueTimer = 10f;
    bool canShoot = true;
    [SerializeField] float shootCooldown = 0.08f;
    Camera cam;
    Vector3 lookDir;
    [SerializeField] float knockbackStrength = 3f;
    [SerializeField] float knockbackTimer = 0.5f;
    bool isKnockedBack = false;
    [SerializeField] int totalBulletCount = 24;
    int currentRoundBulletCount = 6;
    int oneRoundBulletCount = 6;
    // [SerializeField] TextMeshProUGUI roundBulletCountUI;
    // [SerializeField] TextMeshProUGUI totalBulletCountUI;
    // [SerializeField] TextMeshProUGUI healthCountUI;
    [SerializeField] int playerHealth = 100;
    Animator playerAnim;

    void Awake(){
        playerAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        moveAction = InputSystem.actions.FindAction("Move");
        shootAction = InputSystem.actions.FindAction("Shoot");
        aimAction = InputSystem.actions.FindAction("Aim");
        reloadAction = InputSystem.actions.FindAction("Reload");
        interactAction = InputSystem.actions.FindAction("Interact");
        // roundBulletCountUI.text = $"Bullets In Round: {currentRoundBulletCount}";
        // totalBulletCountUI.text = $"Total Number Of Bullets: {totalBulletCount}";
        // healthCountUI.text = $"Amy's health: {playerHealth}";
    }
    void Update(){  
        if (isKnockedBack) return;
        moveDir = moveAction.ReadValue<Vector2>();
        rb.linearVelocity = moveDir * moveSpeed;
        playerAnim.SetFloat("HorizontalValue", moveDir.x);
        HandleShooting();
        HandleAiming();
        HandleReloading();
    }
    void HandleShooting(){
        if (shootAction.IsPressed() && canShoot && currentRoundBulletCount > 0){
            Instantiate(bullet, spawnPointPos.position, transform.rotation);
            Knockback();
            currentRoundBulletCount--;
            StartCoroutine(ShootCooldown());
            // roundBulletCountUI.text = $"Bullets In Round: {currentRoundBulletCount}";
        }
    }
    void Knockback(){
        isKnockedBack = true;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(-lookDir * knockbackStrength, ForceMode2D.Impulse);
        StartCoroutine(KnockbackTimer());
    }
    void HandleAiming(){
        Vector2 aimDir = aimAction.ReadValue<Vector2>();
        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(aimDir);
        mouseWorldPos.z = 0f;
        lookDir = (mouseWorldPos-transform.position).normalized;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0, angle-90);
    }
    void HandleReloading(){
        if (reloadAction.WasPressedThisFrame() && totalBulletCount > 0 && currentRoundBulletCount < oneRoundBulletCount){
            int numberOfReload = oneRoundBulletCount - currentRoundBulletCount;
            if (totalBulletCount >= oneRoundBulletCount){
                currentRoundBulletCount += numberOfReload;
                totalBulletCount-=numberOfReload;
            } else{
                numberOfReload = totalBulletCount<=(oneRoundBulletCount-currentRoundBulletCount)?totalBulletCount:(oneRoundBulletCount-currentRoundBulletCount);
                currentRoundBulletCount+=numberOfReload;
                totalBulletCount-=numberOfReload;
            }
            // roundBulletCountUI.text = $"Bullets In Round: {currentRoundBulletCount}";
            // totalBulletCountUI.text = $"Total Number Of Bullets: {totalBulletCount}";
        }
    }
    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Interactables")){
            npcScript = other.gameObject.GetComponent<NPC>();
            npcScript.Interact();
            StartCoroutine(DialogueTimer());
        }
    }
    void OnTriggerStay2D(Collider2D other){
        if (other.CompareTag("Doors") && interactAction.WasPressedThisFrame()){
            doorScript = other.gameObject.GetComponent<DoorScript>();
            if (!doorScript.doorOpen){
                doorScript.OpenDoorInteraction();
            } else if (doorScript.doorOpen){
                doorScript.CloseDoorInteraction();
            }
        }
    }
    void OnTriggerExit2D(Collider2D other){
        if (other.CompareTag("Interactables")){
            npcScript = other.gameObject.GetComponent<NPC>();
            npcScript.NotInteract();
        }
    }
    IEnumerator DialogueTimer(){
        yield return new WaitForSeconds(dialogueTimer);
        npcScript.NotInteract();
    }
    IEnumerator ShootCooldown(){
        canShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }
    IEnumerator KnockbackTimer(){
        yield return new WaitForSeconds(knockbackTimer);
        isKnockedBack = false;
    }
}
