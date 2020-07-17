using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    private float _prevHor = 0f;
    private float _prevVer = 0f;

    public Animator Anim;
    public Joystick joystick;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!LevelManager.isLevelFinished)
            MovePlayer();
        else
        {
            if((!LevelManager.isPlayerInfected && !LevelManager.isRewindOver) || (LevelManager.isPlayerInfected && LevelManager.isRewindOver))
                Anim.SetBool("IsWalking", false);
            else
                Anim.SetBool("IsWalking", true);
        }
    }

    void MovePlayer()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        if (horizontal != 0 || vertical != 0)
        {
            Anim.SetBool("IsWalking", true);
            Anim.SetFloat("Vertical", vertical);
            Anim.SetFloat("Horizontal", horizontal);
            Vector3 movement = new Vector3(horizontal, vertical, 0f);
            transform.position += movement * Time.deltaTime * _speed;

            _prevHor = horizontal;
            _prevVer = vertical;
        }

        else if (horizontal == 0 && vertical == 0)
        {
            Anim.SetBool("IsWalking", false);
            Anim.SetFloat("Vertical", _prevVer);
            Anim.SetFloat("Horizontal", _prevHor);
        }
    }
}
