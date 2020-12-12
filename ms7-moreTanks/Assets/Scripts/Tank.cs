using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour 
{
    [Header("Tank Stats")]
    protected int health = 10;
    public int maxHealth = 10;
    public float moveSpeed = 2.0f;
    public float rotateSpeed = 1.0f;
    public float turretRotateSpeed = 10.0f;
    public GameObject bullet = null;
    public GameObject explosion = null;

    [Header("Tank Parts")]
    public Transform turret;
    public Transform barrel;
    protected Transform turretTransform = null;
    protected Transform barrelTransform = null;

    private void Awake() {
        turretTransform = turret.transform;
        barrelTransform = barrel.transform;
        health = maxHealth;
    }

    public void StartFiring() {
        InvokeRepeating("Fire", 0.5f, 0.5f);
    }

    public void StopFiring() {
        CancelInvoke("Fire");
    }

    protected void Fire() {
        GameObject newBullet = Instantiate(bullet,
            barrelTransform.position,
            barrelTransform.rotation);

        newBullet.GetComponent<Rigidbody>().AddForce(barrelTransform.forward * 500.0f);
    }

    public void Damage(int damage) {
        health -= damage;
        if (health <= 0) {
            health = 0;
            GameObject e = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(e, 1.5f);
            Destroy(gameObject);
        }
    }

    public float GetHealthPerc() {
        return health / (float)maxHealth;
    }

    public void AimTurretToward(Vector3 target) {
        Vector3 direction = target - turret.transform.position;
        direction.y = 0;
        turret.transform.rotation = Quaternion.Slerp(turret.transform.rotation,
            Quaternion.LookRotation(direction),
            Time.deltaTime * rotateSpeed);
    }

    public void MoveTowards(Vector3 target) {
        Vector3 direction = target - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(direction),
            Time.deltaTime * rotateSpeed);
        transform.Translate(0, 0, Time.deltaTime * moveSpeed);
    }

    public void MoveAway(Vector3 target) {
        Vector3 direction = target - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(direction * -1.0f),
            Time.deltaTime * rotateSpeed);
        transform.Translate(0, 0, Time.deltaTime * moveSpeed);
    }
}
