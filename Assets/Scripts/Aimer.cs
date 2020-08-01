using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aimer : MonoBehaviour
{
    float aimAngle = 0f;
    static float aimAdjustRate = 0.005f;
    float aimedTime = 0.0f;
    bool facingRight = true;

    // Update is called once per frame
    void Update()
    {
        KeyCode upKey = facingRight ? KeyCode.UpArrow : KeyCode.DownArrow;
        KeyCode downKey = facingRight ? KeyCode.DownArrow : KeyCode.UpArrow;

        if (Time.time > (aimAdjustRate + aimedTime)) {
            if (Input.GetKey(upKey)) {
                aimedTime = Time.time;
                SetAimAngle(aimAngle + 1);
            }

            if (Input.GetKey(downKey)) {
                aimedTime = Time.time;
                SetAimAngle(aimAngle - 1);
            }
        }
    }

    void SetAimAngle(float angle) {
        float minAim = facingRight ? 0f : 90f;
        float maxAim = facingRight ? 90f : 180f;

        aimAngle = Mathf.Clamp(angle, minAim, maxAim);
        transform.eulerAngles = new Vector3(0, 0, aimAngle);
    }

    void ReflectAimAngle() {
        float diff = 90f - aimAngle;

        SetAimAngle(aimAngle + (2 * diff));
    }

    public void TurnLeft() {
        if (!facingRight) { return; }

        facingRight = false;
        ReflectAimAngle();
    }

    public void TurnRight() {
        if (facingRight) { return; }

        facingRight = true;
        ReflectAimAngle();
    }
}
