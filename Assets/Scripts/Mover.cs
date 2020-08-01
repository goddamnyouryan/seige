using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public int speed = 8;
    Aimer aimer;

    void Start() {
        aimer = gameObject.GetComponentInChildren<Aimer>();
    }

    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey(KeyCode.LeftArrow)) {
            aimer.TurnLeft();
            pos.x -= speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow)) {
            aimer.TurnRight();
            pos.x += speed * Time.deltaTime;
        }

        transform.position = pos;
    }
}
