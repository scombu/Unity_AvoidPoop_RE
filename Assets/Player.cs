using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidbody;

    private Animator animator;

    private SpriteRenderer renderer;

    [SerializeField]private float speed = 3;

    private float horizontal;

    

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal"); //왼쪽 방향키(-1)~오른쪽방향키(+1)사이의 실수값을 horizontal에 대입한다

        if (GameManager.Instance.stopTrigger)
        {
            animator.SetTrigger("Start");
            PlayerMove();
        }

        if (!GameManager.Instance.stopTrigger)
        {
            animator.SetTrigger("Die");
        }

        ScreenChk();
    }

    private void PlayerMove()
    {
        animator.SetFloat("speed", Mathf.Abs(horizontal));    //Animator에 Float타입 변수 speed에 GetAxis값인 horizontal을 절대값으로 대입

        if (horizontal < 0)                                    // horizontal의 값이 0보다 아래일인 음수일 때(왼쪽으로 움직이기 시작할 때)
        {
            renderer.flipX = true;                            //렌더링되는 이미지를 x좌축으로 180도 뒤집는다
        }
        else if (horizontal > 0)                              //만약 아니라면(horizontal값이 0보다 큰 양수라면{오른쪽으로 움직이기 시작할 때})
        { 
            renderer.flipX = false;                           //렌더링되는 이미지를 x축으로 180도 뒤집던 것을 멈춘다
        }
       rigidbody.velocity = new Vector2(horizontal * speed, rigidbody.velocity.y); 
                                                             //캐릭터의 속도를 x좌표변수인 horizontal을 대입시켜 서서히 빨라지되 1 이상의 속도는 내지 못하게 한다.
    }

    private void ScreenChk()
    {
        Vector3 worlpos = Camera.main.WorldToViewportPoint(this.transform.position);
        if (worlpos.x < 0.05f) worlpos.x = 0.05f;
        if (worlpos.x > 0.95f) worlpos.x = 0.95f;
        this.transform.position = Camera.main.ViewportToWorldPoint(worlpos);
    }
}
