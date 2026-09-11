using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform player;
    Camera mainCam;
    [Range(0f, 1f)] [SerializeField] float middlePointToCursorDist = 0.35f;
    [SerializeField] float maxMiddlePointDist = 4f;
    Vector2 aimInput;
    InputAction aimAction;
    string enemyTag = "Enemies";
    [SerializeField] float transitionSpeed = 3f;
    float currentWeight =0f;
    Collider2D[] collidersInView = new Collider2D[50];

    void Start(){
        mainCam = Camera.main;
        aimAction = InputSystem.actions.FindAction("Aim");
    }
    void Update(){
        if (player == null) return;
        bool enemiesOnScreen = CheckEnemiesInView();
        float targetWeight = enemiesOnScreen?1f:0f;
        currentWeight = Mathf.MoveTowards(currentWeight, targetWeight, transitionSpeed);
        Vector3 offset = Vector3.zero;
        aimInput = aimAction.ReadValue<Vector2>();
        Vector3 mouseWorldPos = mainCam.ScreenToWorldPoint(new Vector3(aimInput.x, aimInput.y, mainCam.nearClipPlane));
        mouseWorldPos.z = 0f;
        offset = mouseWorldPos - player.position;
        offset *= middlePointToCursorDist;
        offset = Vector3.ClampMagnitude(offset, maxMiddlePointDist);
        transform.position = player.position + (offset * currentWeight);
    }

    bool CheckEnemiesInView(){
        float height = mainCam.orthographicSize*2f;
        float width = height * mainCam.aspect;
        Vector2 camSize = new Vector2(width, height);
        int count = Physics2D.OverlapBoxNonAlloc(mainCam.transform.position, camSize, 0f, collidersInView);
        for (int i = 0; i<count; i++){
            if (collidersInView[i].CompareTag(enemyTag)){
                return true;
            }
        }
        return false;
    }
}
