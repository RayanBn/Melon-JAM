using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class CharmSystem : MonoBehaviour
{
    public Rigidbody2D box;

    public List<GameObject> charms = new List<GameObject>();

    [SerializeField] public float charmXOffsetRadius = 1f;
    [SerializeField] public bool isCharmOrbiting = true;
    [SerializeField] public float charmOrbitSpeed = 45f;
    [SerializeField] public float charmRotationSpeed = 15f;
    [SerializeField] private int selectedCharm = -1;
    [SerializeField] public float fireCharmMaxDistance = 1.5f;
    [SerializeField] public float airCharmDetectionRadius = 1.25f;
    [SerializeField] public float airCharmForce = 50f;
    [SerializeField] public float airCharmCooldown = 1f;
    [SerializeField] public float airCharmCooldownTimer = 0f;

    private Vector2 lastCharmPosition;

    private void CharmSelector() {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ResetCharmsEffect();
            selectedCharm = 0;
            charms[selectedCharm].SetActive(true);
            charms[selectedCharm].transform.position = lastCharmPosition;
            charms[selectedCharm].GetComponent<SpriteRenderer>().color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ResetCharmsEffect();
            selectedCharm = 1;
            charms[selectedCharm].SetActive(true);
            charms[selectedCharm].transform.position = lastCharmPosition;
            charms[selectedCharm].GetComponent<SpriteRenderer>().color = Color.blue;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ResetCharmsEffect();
            selectedCharm = 2;
            charms[selectedCharm].SetActive(true);
            charms[selectedCharm].transform.position = lastCharmPosition;
            charms[selectedCharm].GetComponent<SpriteRenderer>().color = Color.green;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ResetCharmsEffect();
            selectedCharm = 3;
            charms[selectedCharm].SetActive(true);
            charms[selectedCharm].transform.position = lastCharmPosition;
            charms[selectedCharm].GetComponent<SpriteRenderer>().color = Color.white;
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            ResetCharmsEffect();
            charms[selectedCharm].SetActive(false);
            lastCharmPosition = new(charmXOffsetRadius, 0);
            selectedCharm = -1;
        }
    }

    void Update()
    {
        CharmSelector();

        switch(selectedCharm) {
            case 0: FireCharm(); break;
            case 1: WaterCharm(); break;
            case 2: EarthCharm(); break;
            case 3: AirCharm(); break;
        }

        if (selectedCharm > -1) {
            if (isCharmOrbiting) {
                charms[selectedCharm].transform.RotateAround(transform.position, Vector3.forward, charmOrbitSpeed * Time.deltaTime);
            }
            charms[selectedCharm].transform.Rotate(0, 0, charmRotationSpeed * Time.deltaTime);
        }
    }

    private void FireCharm() {
        if (Input.GetButtonDown("Fire1"))
        {
            isCharmOrbiting = false;

            Vector2 screenCoords = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            if (Vector2.Distance(screenCoords, transform.position) > fireCharmMaxDistance)
            {
                screenCoords = (screenCoords - (Vector2)transform.position).normalized * fireCharmMaxDistance + (Vector2)transform.position;
            }

            StartCoroutine(SmoothLerp(charms[selectedCharm].transform.position, screenCoords, charms[selectedCharm].transform, 0.15f));
        }
    }

    private void WaterCharm() {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Water Charm");
        }
    }

    private void EarthCharm() {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Earth Charm");
        }
    }

    private void AirCharm() {
        if (Input.GetButtonDown("Fire1") && airCharmCooldownTimer <= 0)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, airCharmDetectionRadius);

            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.CompareTag("FireSwitch")) {
                    Debug.Log("FireSwitch");
                    collider.gameObject.GetComponent<FireSwitch>().TurnOff();
                    continue;
                }

                Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
                if (rb != null && collider.gameObject.CompareTag("Movable"))
                {
                    rb.AddForce((collider.transform.position - transform.position).normalized * airCharmForce, ForceMode2D.Impulse);
                }
            }

            if (colliders.Length > 0) {
                StartCoroutine(AirCharmCooldownTimer());
            }
        }
    }

    public int GetSelectedCharm()
    {
        return selectedCharm;
    }

    private void ResetCharmsEffect() {
        if (selectedCharm < 0) {
            return;
        }

        if (selectedCharm == 0 && !isCharmOrbiting) {
            lastCharmPosition = new(charmXOffsetRadius, 0);
        } else {
            lastCharmPosition = charms[selectedCharm].transform.position;
        }
        charms[selectedCharm].transform.position = new(transform.position.x + charmXOffsetRadius, transform.position.y);
        charms[selectedCharm].SetActive(false);
        isCharmOrbiting = true;
    }

    private IEnumerator SmoothLerp (Vector2 startPos, Vector2 endPos, Transform target, float time)
    {
        float elapsedTime = 0;

        while (elapsedTime < time) {
            target.position = Vector2.Lerp(startPos, endPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator AirCharmCooldownTimer() {
        airCharmCooldownTimer = airCharmCooldown;

        while (airCharmCooldownTimer > 0) {
            airCharmCooldownTimer -= Time.deltaTime;
            yield return null;
        }
    }
}
