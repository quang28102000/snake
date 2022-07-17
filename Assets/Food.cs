using UnityEngine;

public class Food : MonoBehaviour
{
    public BoxCollider2D gridArea;

    private void Start() {
        RandomPosition();
    }

    private void RandomPosition() {
        Bounds bounds = this.gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }

    // When the object's triggered aka the snake eats the food, we create a new food in random position in the grid
    private void OnTriggerEnter2D(Collider2D other) {
        // check what the object is when it triggers the food
        if(other.tag == "Player") {
            RandomPosition();
        }

        
    }
}
 