using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Vector2 speed;
    public Vector2 limitX;

    Camera cam;
    Vector2 input;

    void Start() {
        cam = GetComponent<Camera>();
        input = new Vector2(0f, 0f);
    }

    void Update() {
        input += new Vector2(Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X")) * speed;
    }

    void LateUpdate() {
        input.x = Mathf.Clamp(input.x, limitX.x, limitX.y);
        transform.localEulerAngles = input;
    }

    int[] FibSequence() {
        int[] fib = new int[8];
        for (int i = 0; i < 8; i++) {
            fib[i] = Fib(i);
        }
        return fib;
    }
    int Fib(int i) {
        switch (i) {
            default: return 0;
            case 1: return 1;
            case 2: return 1;
            case 3: return 2;
            case 4: return 3;
            case 5: return 5;
            case 6: return 8;
            case 7: return 13;
        }
    }
}
