using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    // keep track direction, default the snake goes to the right
    private Vector2 _direction = Vector2.right;

    // snake's length with each box object
    private List<Transform> _segments = new List<Transform>();

    // Snake's prefab
    public Transform segmentPrefab;

    // snake default size
    public int initialSize = 6;

    private void Start() {
        ResetState();
    }

    private void Update() { 
        if(Input.GetKeyDown(KeyCode.W)) {
            _direction = Vector2.up;
        } else if (Input.GetKeyDown(KeyCode.S)) {
            _direction = Vector2.down;
        } else if (Input.GetKeyDown(KeyCode.A)) {
            _direction = Vector2.left;
        } else if (Input.GetKeyDown(KeyCode.D)) {
            _direction = Vector2.right;
        }
    }

    // changed position of our snake in the game
    private void FixedUpdate() {
        
        // for as a reverver order for segment -> the tail would be changed to next segment 
        for(int i = _segments.Count - 1; i > 0; i--) {
            _segments[i].position = _segments[i - 1].position;
        }

        // snake's move in 2d
        // Mathf.round() makes sure the snake align to the grid
        this.transform.position = new Vector3(
            (Mathf.Round(this.transform.position.x)) + _direction.x,
            (Mathf.Round(this.transform.position.y)) + _direction.y,
            0.0f
        );
    }

    private void Grow() {
        
        Transform segment = Instantiate(this.segmentPrefab); 

        // add the segment to the tail of the snake
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
    }

    // used to detect collision including biting itself, hitting the wall and eating the food
    private void OnTriggerEnter2D(Collider2D other) {
        // check what the object is when it triggers the food
        if(other.tag == "Food") {
            Grow();
        } else if(other.tag == "Obstacle") {
            ResetState();
        }
    }

    private void ResetState() {
        // clear out the segment's stack excluding head's snake
        for(int i = 1; i < _segments.Count; i++) {
            Destroy(_segments[i].gameObject);
        }

        _segments.Clear();
        
        // snake's head
        _segments.Add(this.transform);

        // set default prefab for the segment
        for(int i = 1; i < this.initialSize; i++) {
            _segments.Add(Instantiate(this.segmentPrefab));
        }

        // snake's start position
        this.transform.position = Vector3.zero;
    }
}
