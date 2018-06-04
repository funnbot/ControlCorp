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
    public Transform button;
    public Material wallMaterial;

    Rigidbody rb;
    Vector3 input;
    Vector2 mouse;
    Vector3 offset;

    RaycastHit hit;
    bool lookingAt;

    public Text info;
    public Text speech;
    bool textSet;
    float textTimer;

    public bool buttonDown;

    void Start() {
        rb = GetComponent<Rigidbody>();
        mouse = new Vector2(transform.localEulerAngles.x, transform.localEulerAngles.y);
        info.text = "";
        speech.text = "";
        offset = transform.position - cam.transform.position;
    }

    void Update() {
        input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        mouse += new Vector2(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X")) * lookSpeed;

        Vector3 angles = transform.localEulerAngles;
        angles.y = mouse.y;
        transform.localEulerAngles = angles;

        RayCast();
        if (lookingAt) ToolTip();
        else SetInfo("");

        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene("Game");
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            if (lookingAt) Interact();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene("MainMenu");
        }


        if (textTimer <= 0f) {
            speech.text = "";
        } else textTimer -= Time.deltaTime;
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
        cam.transform.position = transform.position - offset;
        angles.y = transform.localEulerAngles.y;
        cam.transform.localEulerAngles = angles;
    }

    bool talkWithNan;

    void ToolTip() {
        GameObject go = hit.collider.gameObject;
        if (go.CompareTag("Nantucket")) {
            SetInfo("Press E to talk to the Nantucket Representitive");
        }
        if (go.CompareTag("Paul")) {
            SetInfo("Press E to talk to Paul Ryan");
        }
        if (go.CompareTag("RedButton")) {
            SetInfo("Press E to control their appetite");
        }
    }

    void Interact() {
        GameObject go = hit.collider.gameObject;
        if (go.CompareTag("Nantucket")) {
            SetSpeech("\"Paul Ryan will be reforming taxes soon, ensure that our needs are met in the new bill.\"");
            talkWithNan = true;
        }
        if (go.CompareTag("Paul")) {
            if (talkWithNan) {
                SetSpeech("\"Wow, your funraiser brought me over 10 million dollars, now what were those changes you wanted me to make again?\"");
            } else SetSpeech("Do I know you?");
        }
        if (go.CompareTag("RedButton")) {
            if (!buttonDown) {
                button.Translate(0f, 0f, -0.1f);
                wallMaterial.color = Color.red;
            } else {
                button.Translate(0f, 0f, 0.1f);
                wallMaterial.color = Color.white;
            }
            buttonDown = !buttonDown;
        }
    }

    void RayCast() {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        lookingAt = Physics.Raycast(ray, out hit, 2.5f);
    }

    void SetSpeech(string t) {
        textTimer = 5;
        speech.text = t;
    }

    void SetInfo(string t) {
        info.text = t;
    }



    public static PlayerController Instance;
    void Awake() {
        Instance = this;
    }
}