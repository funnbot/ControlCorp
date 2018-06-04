using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class PeopleController : MonoBehaviour {

    public float wanderRadius;
    public float wanderTimer;

    private Transform target;
    private NavMeshAgent agent;
    private float timer;

    PlayerController player;

    // Use this for initialization
    void Start() {
        agent = GetComponent<NavMeshAgent>();
        timer = 0;

        agent.enabled = false;
        agent.enabled = true;

        player = PlayerController.Instance;
    }

    bool pressed;

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;

        if (player.buttonDown) {
            if (!pressed) {
                Debug.Log("pressed");
                pressed = true;
                IntoMC();
            }

        } else pressed = false;

        if (timer >= wanderTimer) {
            Vector3 newPos = randomPos();
            Debug.Log(newPos);
            agent.SetDestination(newPos);
            timer = Random.Range(-3, 5);
        }
    }

    Vector3 randomPos() {
        float x = Random.Range(-7, 8);
        float y = Random.Range(-7, 8);
        return new Vector3(x, 0.5f, y) + transform.parent.position;
    }

    [ContextMenu("Into MC")]
    void IntoMC() {
        timer = -5;
        agent.SetDestination(transform.parent.position + new Vector3(0.25f, 0.5f, 6.25f));
    }
}
