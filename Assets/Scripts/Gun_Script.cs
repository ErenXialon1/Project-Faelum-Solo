using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Script : MonoBehaviour
{
    public Transform Crosshair;
    private Camera cameraa;
    public GameObject bulletlauncher;
    public GameObject bulletPrefab;
    public GameObject bulletStart;
    public float bulletSpeed = 60.0f;
    public float bulletDamage = 25f;

    private Vector3 target;
    private Vector3 difference;
    private float rotationZ;
    private bool throwBool = false;

    private float throwCooldown = 0f;
    public float throwCooldownTime = 0.5f;

    void Start()
    {
        cameraa = GetComponent<Camera>();
        Cursor.visible = false;
    }

    void Update()
    {
        target = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        Crosshair.transform.position = new Vector2(target.x, target.y);

        difference = target - bulletlauncher.transform.position;
        rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        bulletlauncher.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        if (Input.GetMouseButtonDown(1) && throwCooldown <= 0f)
        {
            float distance = difference.magnitude;
            Vector2 direction = difference / distance;
            direction.Normalize();
            fireBullet(direction, rotationZ);
            throwCooldown = throwCooldownTime;
        }

        if (throwCooldown > 0f)
        {
            throwCooldown -= Time.deltaTime;
        }
    }

    void fireBullet(Vector2 direction, float rotationZ)
    {
        GameObject b = Instantiate(bulletPrefab) as GameObject;
        b.transform.position = bulletStart.transform.position;
        b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        b.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }
}