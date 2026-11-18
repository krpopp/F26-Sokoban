using UnityEngine;

public class SquareMove : MonoBehaviour
{
    Vector3 targetPosition;
    Vector3 restartPostion;
    
    public float speed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        restartPostion = transform.position;
        targetPosition = new Vector3(transform.position.x + 1.08f, transform.position.y + 1.08f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            transform.position = restartPostion;
        }
    }
}
