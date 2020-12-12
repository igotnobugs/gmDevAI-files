using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimAssist : Tank 
{
    private Animator anim = null;
    public float aimRange = 30.0f;
    [SerializeField] private TankAI[] tanksInRange = null;
    public Image hpBar;

    public Tank closestTarget = null;

    public float coolDown = 0.0f;

    private void Start() {
        anim = GetComponent<Animator>();
        tanksInRange = FindObjectsOfType<TankAI>();
        StartAiming();
    }

    private void Update() {
        hpBar.fillAmount = GetHealthPerc();

        if (coolDown <= 0.5f) {
            coolDown += Time.deltaTime;
        } else {
            if (Input.GetKey(KeyCode.Space)) {
                Fire();
                coolDown -= 0.5f;
            }
        }

    }

    public void StartAiming() {
        InvokeRepeating("GetClosestTank", 0.2f, 0.2f);
    }

    public void StopAiming() {
        CancelInvoke("GetClosestTank");
    }

    public void GetClosestTank() {
        if (tanksInRange.Length <= 0) return;

        Tank closestTank = tanksInRange[0];
        float closestDistance = 1000.0f;

        for(int i = 0; i < tanksInRange.Length; i++) {
            if (tanksInRange[i] != null) {
                float tankDistance = Vector3.Distance(transform.position,
                    tanksInRange[i].transform.position);

                if (tankDistance <= closestDistance) {
                    closestDistance = tankDistance;
                    closestTank = tanksInRange[i];
                }
            }
        }

        closestTarget = closestTank;
        anim.SetFloat("closestTargetDistance", closestDistance);
    }

    private void OnDestroy() {
        if (hpBar != null) {
            hpBar.fillAmount = 0;
        }
    }
}
