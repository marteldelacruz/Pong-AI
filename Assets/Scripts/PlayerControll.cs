using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == "Player1")
        {
            if (Input.GetKey(KeyCode.W))
                MovePlayer();
            else if (Input.GetKey(KeyCode.S))
                MovePlayer(false);
        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow))
                MovePlayer();
            else if (Input.GetKey(KeyCode.DownArrow))
                MovePlayer(false);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isUp"></param>
    private void MovePlayer(bool isUp=true)
    {
        Vector3 dir = (isUp) ? Vector3.up : Vector3.down;
        dir *= Time.deltaTime * 2;
        gameObject.transform.position += dir;
    }
}
