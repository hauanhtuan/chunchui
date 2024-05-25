using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private float minPos;
    [SerializeField] private float initPos;
    [SerializeField] private float speed = 1;
    
    private bool moving = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.S))
            Debug.Log(transform.lossyScale);
        if (Input.GetKeyDown(KeyCode.M))
            moving = !moving;
        if (moving)
        {
            Vector2 pos = Vector2.left * speed * Time.deltaTime * transform.lossyScale.x;
            transform.position += (Vector3)pos;
            if (transform.position.x < (minPos+initPos * -1) * transform.lossyScale.x)
            {
                var tempPos = transform.position;
              
                    tempPos.x = (initPos + minPos) * transform.lossyScale.x;
                transform.position = tempPos;
            }    
        }
    }
}
