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
    Aimer aimer;

    void Start() {
        Aimer aimer = gameObject.GetComponent<Aimer>();
    }

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
        // https://answers.unity.com/questions/759542/get-coordinate-with-angle-and-distance.html
        charging = false;
        float barrelLength = barrel.transform.localScale.x;
        float projectileDistance = barrelLength + (projectilePrefab.transform.localScale.x / 2);
        float aimAngle = barrel.transform.rotation.eulerAngles.z;
        float radians = aimAngle * Mathf.Deg2Rad;
        float x = Mathf.Cos(radians);
        float y = Mathf.Sin(radians);

        // not entirely sure why the below works
        Vector3 position = new Vector3(x, y, 0) * projectileDistance + (barrel.transform.position - (barrel.transform.right * 2));
        GameObject projectile = Instantiate(projectilePrefab, position, barrel.transform.rotation);
        //projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.right * 2000 * power);
    }
}
