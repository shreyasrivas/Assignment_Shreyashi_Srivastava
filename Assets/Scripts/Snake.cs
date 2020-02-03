using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {
    [SerializeField]
    private Rigidbody bodyPrefab;
    private List<Rigidbody> BodyParts = new List<Rigidbody> ();
    private Rigidbody head;
    private Rigidbody main_body;
    float time_delta;
    bool move;
    float step_value = 1f;
    int direction;
    bool stopMovement = true;
    List<Vector3> deltaPosition;

    void Awake () {
        main_body = GetComponent<Rigidbody> ();
        InitializeBody ();
        deltaPosition = new List<Vector3> () {
            new Vector3 (step_value, 0f), //Right
            new Vector3 (-step_value, 0f), //Left
            new Vector3 (0f, step_value), //Up
            new Vector3 (0f, -step_value), //Down
        };
    }

    void Update () {
        if (stopMovement) {
            canMove ();
        }
            KeyboardInput ();
    }

    void FixedUpdate () {
        if (move) {
            move = false;
            Move ();
        }
    }

    void InitializeBody () {
        BodyParts.Add (transform.GetChild (0).GetComponent<Rigidbody> ());
        BodyParts.Add (transform.GetChild (1).GetComponent<Rigidbody> ());
        BodyParts.Add (transform.GetChild (2).GetComponent<Rigidbody> ());
        head = BodyParts[0];
    }

    void Move () {
        Vector3 stepPosition = deltaPosition[direction];
        Vector3 prevPosition;
        Vector3 newPosition = head.position;
        head.position = head.position + stepPosition;
        main_body.position = main_body.position + stepPosition;

        for (int i = 1; i < BodyParts.Count; i++) {
            prevPosition = BodyParts[i].position;
            BodyParts[i].position = newPosition;
            newPosition = prevPosition;
        }
    }

    void KeyboardInput () {
        if (Input.GetKeyDown (KeyCode.RightArrow)) {
            if (direction == 0 || direction == 1)
                return;
            direction = 0;
        } else
        if (Input.GetKeyDown (KeyCode.LeftArrow)) {
            if (direction == 0 || direction == 1)
                return;
            direction = 1;
        } else
        if (Input.GetKeyDown (KeyCode.UpArrow)) {
            if (direction == 2 || direction == 3)
                return;
            direction = 2;
        } else
        if (Input.GetKeyDown (KeyCode.DownArrow)) {
            if (direction == 2 || direction == 3)
                return;
            direction = 3;
        }

    }

    public void AddBody () {
        GameObject body_object = Instantiate (bodyPrefab, BodyParts[BodyParts.Count - 1].position, Quaternion.identity).gameObject;
        body_object.transform.SetParent (transform, true);
        BodyParts.Add (body_object.GetComponent<Rigidbody> ());
    }

    public void StopMove () {
        stopMovement = false;
    }

    void canMove () {
        time_delta += Time.deltaTime;
        if (time_delta >= 0.2f) {
            time_delta = 0;
            move = true;
        }
    }

}