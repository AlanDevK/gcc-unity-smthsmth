using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoorScript : MonoBehaviour
{
    [SerializeField] float lerpTime = 2f;
    public bool doorOpen;
    float elapsedTime;
    public void OpenDoorInteraction(){
        doorOpen = true;
        StartCoroutine(OpenDoor());
    }
    public void CloseDoorInteraction(){
        doorOpen = false;
        StartCoroutine(CloseDoor());
    }
    IEnumerator OpenDoor(){
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z - 90);
        float elapsedTime = 0f;
        while (elapsedTime < lerpTime){
            elapsedTime+=Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime/lerpTime);
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            yield return null;
        }
        transform.rotation = endRotation;
    }
    IEnumerator CloseDoor(){
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + 90);
        float elapsedTime = 0f;
        while (elapsedTime < lerpTime){
            elapsedTime+=Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime/lerpTime);
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            yield return null;
        }
        transform.rotation = endRotation;
    }
}
