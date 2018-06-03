using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {
    public Camera cam;
    [Range(0, 5)]
    public float speed;

    public Vector2 limitX;
    [Range(1, 10)]
    public float lookSpeed;

    Rigidbody rb;
    Vector3 input;
    Vector2 mouse;

    public Text info;
    float textTimer;

    void Start() {
        rb = GetComponent<Rigidbody>();
        mouse = new Vector2(transform.localEulerAngles.x, transform.localEulerAngles.y);
    }

    void Update() {
        input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        mouse += new Vector2(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X")) * lookSpeed;

        Vector3 angles = transform.localEulerAngles;
        angles.y = mouse.y;
        transform.localEulerAngles = angles;

        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene("Game");
        }
    }

    void FixedUpdate() {
        Vector3 pos = (input.x * rb.transform.right) + (input.z * rb.transform.forward);
        transform.position += pos * speed;
        // rb.MovePosition(transform.position + pos * speed);
    }

    void LateUpdate() {
        Vector3 angles = cam.transform.localEulerAngles;
        mouse.x = Mathf.Clamp(mouse.x, limitX.x, limitX.y);
        angles.x = mouse.x;
        cam.transform.position = transform.position;
        angles.y = transform.localEulerAngles.y;
        cam.transform.localEulerAngles = angles;
    }

    bool talkWithNan;

    void SetText(string t) {
        
    }
}