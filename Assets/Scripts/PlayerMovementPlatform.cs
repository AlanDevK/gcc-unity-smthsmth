using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementPlatform : MonoBehaviour
{
    [SerializeField] InputActionReference moveActionRef;
    [SerializeField] float moveSpeed = 10f;
    Animator playerAnim;
    bool isFacingRight = true;
    float moveDir;
    Rigidbody2D rb;
    float currentScale;

    void Awake(){
        currentScale = transform.localScale.x;
        playerAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        moveDir = moveActionRef.action.ReadValue<float>();
        rb.linearVelocityX = moveDir * moveSpeed;
        Flip();
        playerAnim.SetFloat("HorizontalValue", moveDir);
    }

    void Flip()
    {
        if(moveDir  == 0 ) return;
        if(moveDir > 0 && !isFacingRight)
        {
            transform.localScale = new Vector3(1,1,1) * currentScale;
            isFacingRight = true;
            return;
        }
        if(moveDir < 0 && isFacingRight)
        {
            transform.localScale = new Vector3(-1,1,1) * currentScale;
            isFacingRight = false;
            return;
        }
    }
}
