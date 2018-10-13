using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float MoveSpeed = 2f;
    public float AffectedMinRadiusSqr = 0f;
    public bool controlEnabled = true;
    float targetRotation;

    // Use this for initialization
    void Start () {
        targetRotation = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (controlEnabled) {
            float targetX = Screen.width / 2 - Input.mousePosition.x;
            float targetY = Screen.height / 2 - Input.mousePosition.y;

            if (targetX * targetX + targetY * targetY >= AffectedMinRadiusSqr) {
                targetRotation = Mathf.Atan2(targetY, targetX) * Mathf.Rad2Deg;
            }
        }

        Quaternion targetQuaternion = Quaternion.Euler(0, 0, targetRotation);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetQuaternion, Time.deltaTime * MoveSpeed);
	}
}
