using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FireSwitch : MonoBehaviour
{
    private Animator animator;
    private Light2D light2D;
    [SerializeField] private bool isOn = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        light2D = GetComponent<Light2D>();
        animator.SetBool("isOn", isOn);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("FireCharm")) {
            TurnOn();
        }
    }

    public void TurnOff() {
        isOn = false;
        light2D.enabled = false;
        animator.SetBool("isOn", isOn);
    }

    public void TurnOn() {
        isOn = true;
        light2D.enabled = true;
        animator.SetBool("isOn", isOn);
    }
}
