using UnityEngine;

public class DogManager : MonoBehaviour
{
    public Transform ball;            // �ǂ�������target
    public Transform player;            // player
    public float speed = 1.0f;           // dog�̓���Speed

    private static Transform target;   // target
    private static Transform myTr;       // Dog
    private static Transform getBallTr;  // �{�[�������ꏊ
    private static Animator m_Animator;
    [SerializeField]private Mode mode = Mode.idle;

    private bool touchTarget  = false;   // �G�ꂽ���ǂ���
    private bool getting      = false;   // �{�[���������Ă邩
    private bool nearPlayer   = false;   // �v���C���[�̋߂���

    enum Mode { idle,chase,hold,drop}

    void Start()
    {
        //������
        myTr = this.transform;
        GameObject ballget = GameObject.Find("getTarget");
        getBallTr = ballget.transform;

        GameObject neck = GameObject.Find("Neck_M");
        

        ballget = GameObject.Find("getTarget");
        m_Animator = GetComponent<Animator>();

    }

    void Update()
    {
        switch (mode)
        {
            case Mode.idle:
                if(target != null)
                {
                    StartChase(ball);
                }
                break;
            case Mode.chase:
                Chase();
                break;
            case Mode.hold:
                
                break;
            case Mode.drop:
                break;
        }/*
        if (touchTarget) // target�ɐG�ꂽ
        {
            move = false;
            Chase(); // �ǐ�
            
            if(getting)
            {
                //Invoke(nameof(comeBack), 0.8f); // player�̏ꏊ�ɍs��
                comeBack();
            }
        }
        else // �G��Ă��Ȃ�
        {
            move = true;
            Chase(); // �ǐ�
        }*/
    }

    public void TargetSetBall()
    {
        target = ball;
    }

    private void comeBack()
    {
        if (nearPlayer)
        {
            target.GetComponent<Rigidbody>().isKinematic = false;
            target.parent = null;
            Chase(); // �ǐ�
        }
        else
        {
            Debug.Log("player�̋߂�����Ȃ�");
            //targetTr.position = getBallTr.position; // �{�[�������ɉ^��
            Chase(); // �ǐ�
        }
    }

    /// <summary>
    /// �{�[��������
    /// </summary>
    private void getBall()
    {
        ball.position = getBallTr.position; // �{�[�������ɉ^��
        ball.parent = getBallTr; // ���̎q�I�u�ɂȂ�
        ball.GetComponent<Rigidbody>().isKinematic = true;
        Debug.Log("�{�[����������");
        getting = true;

        StartChase(player);
    }

    void DropBall()
    {
        ball.GetComponent<Rigidbody>().isKinematic = false;
        ball.parent = null;
        target = null;
    }

    /// <summary>
    /// �G�ꂽ�Ƃ�
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter(Collider col)
    {
        if (target != null && col.gameObject.tag == target.tag)
        {
            NearTarget();
            StopChase();
        }

    }

    void NearTarget()
    {
        if (getting)
        {
            nearPlayer = true;
            Debug.Log("�傾�[�I");
            DropBall();
            mode = Mode.idle;
        }
        else
        {
            touchTarget = true;
            mode = Mode.hold;
            Invoke(nameof(getBall), 0.8f); // �{�[�������
            Debug.Log("�{�[���͈̔͂ɓ�����");
        }
    }
    
    /// <summary>
    /// �G�ꂽ�Ƃ�
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerExit(Collider col)
    {
/*
        if (col.gameObject.tag == "target")
        {
            touchTarget = false;
            Debug.Log("�{�[���͈̔͊O");
            getting = false;
        }
        else if (col.gameObject.tag == "Player")
        {
            nearPlayer = false;
            Debug.Log("�n����");
        }*/
    }

    void StartChase(Transform newTarget)
    {
        target = newTarget;
        //�A�j���[�V�������Z�b�g
        m_Animator.SetBool("isWalking", true);
        mode = Mode.chase;
    }

    void StopChase()
    {
        //�A�j���[�V�������Z�b�g
        m_Animator.SetBool("isWalking", false);

    }

    /// <summary>
    /// �ǐ�
    /// </summary>
    private void Chase()
    {

        myTr.LookAt(target.transform); //�@target������
        Vector3 relativePos = target.position - myTr.position; // Target�̏ꏊ��c��

        myTr.position += relativePos.normalized * speed; // �ړ�

    }
}
