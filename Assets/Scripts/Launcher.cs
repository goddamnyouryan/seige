using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    float inputStart;
    float inputEnd;
    bool charging;
    public float maxLaunchPowerTime = 2f;
    public GameObject powerMeterContainer;
    public GameObject barrel;
    public GameObject projectilePrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            inputStart = Time.time;
            inputEnd = inputStart + maxLaunchPowerTime;
            charging = true;
        }

        if (Input.GetKey(KeyCode.Space)) {
            float power = CalculatePower();

            if (power < 1) {
                float scaledPower = power * barrel.transform.localScale.x;
                powerMeterContainer.transform.localScale = new Vector3(scaledPower , 1, 1);
            } else if (charging) {
                Fire(1);
            }
        }

        if (Input.GetKeyUp(KeyCode.Space)) {
            float power = CalculatePower();

            if (power < 1) {
                Fire(power);
            }
        }
    }

    float CalculatePower() {
        float remaining = inputEnd - Time.time;
        return (1 - remaining / maxLaunchPowerTime);
    }

    void Fire(float power) {
        charging = false;
        float length = barrel.transform.localScale.x + projectilePrefab.transform.localScale.x;
        Console.Log("fartd");

        //gameObject.transform.position
        //GameObject projectile = Instantiate(projectilePrefab, barrelEnd, barrel.transform.rotation);
        //projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.right * 2000 * power);
    }
}
