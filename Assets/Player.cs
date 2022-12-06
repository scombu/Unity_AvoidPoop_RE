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
        horizontal = Input.GetAxis("Horizontal"); //���� ����Ű(-1)~�����ʹ���Ű(+1)������ �Ǽ����� horizontal�� �����Ѵ�

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
        animator.SetFloat("speed", Mathf.Abs(horizontal));    //Animator�� FloatŸ�� ���� speed�� GetAxis���� horizontal�� ���밪���� ����

        if (horizontal < 0)                                    // horizontal�� ���� 0���� �Ʒ����� ������ ��(�������� �����̱� ������ ��)
        {
            renderer.flipX = true;                            //�������Ǵ� �̹����� x�������� 180�� �����´�
        }
        else if (horizontal > 0)                              //���� �ƴ϶��(horizontal���� 0���� ū ������{���������� �����̱� ������ ��})
        { 
            renderer.flipX = false;                           //�������Ǵ� �̹����� x������ 180�� ������ ���� �����
        }
       rigidbody.velocity = new Vector2(horizontal * speed, rigidbody.velocity.y); 
                                                             //ĳ������ �ӵ��� x��ǥ������ horizontal�� ���Խ��� ������ �������� 1 �̻��� �ӵ��� ���� ���ϰ� �Ѵ�.
    }

    private void ScreenChk()
    {
        Vector3 worlpos = Camera.main.WorldToViewportPoint(this.transform.position);
        if (worlpos.x < 0.05f) worlpos.x = 0.05f;
        if (worlpos.x > 0.95f) worlpos.x = 0.95f;
        this.transform.position = Camera.main.ViewportToWorldPoint(worlpos);
    }
}
