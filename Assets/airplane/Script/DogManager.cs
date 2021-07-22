using UnityEngine;

public class DogManager : MonoBehaviour
{
    public GameObject target;            // �ǂ�������target
    public float speed = 1.0f;           // dog�̓���Speed

    private static Transform targetTr;   // target
    private static Transform playerTr;   // target
    private static Transform myTr;       // Dog
    private static Transform getBallTr;  // �{�[�������ꏊ
    private static Animator m_Animator;

    private bool touchTarget  = false;      // �G�ꂽ���ǂ���
    private bool isWalking    = false;      // ������
    private bool move         = true;       // �����Ă��邩�ǂ���
    private bool getting      = false;      // �{�[���������Ă邩

    void Start()
    {
        //������
        targetTr = target.transform;
        myTr = this.transform;
        GameObject ballget = GameObject.Find("getTarget");
        getBallTr = ballget.transform;

        GameObject neck = GameObject.Find("Neck_M");
        

        ballget = GameObject.Find("getTarget");
        m_Animator = GetComponent<Animator>();

    }

    void Update()
    {
        if (touchTarget) // target�ɐG�ꂽ
        {
            move = false;
            Chase(); // �ǐ�
            if(!getting)
                Invoke(nameof(getBall), 0.8f); // �{�[�������
            else
            {

            }
        }
        else // �G��Ă��Ȃ�
        {
            move = true;
            Chase(); // �ǐ�
        }
    }

    private void comeBack()
    {

    }

    /// <summary>
    /// �{�[��������
    /// </summary>
    private void getBall()
    {
        targetTr.position = getBallTr.position; // �{�[�������ɉ^��
        Debug.Log("�{�[����������");
        getting = true;
    }

    /// <summary>
    /// �G�ꂽ�Ƃ�
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "target")
        {
            touchTarget = true;
            Debug.Log("�{�[���͈̔͂ɓ�����");
        }

    }
    
    /// <summary>
    /// �G�ꂽ�Ƃ�
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "target")
        {
            touchTarget = false;
            Debug.Log("�{�[���͈̔͊O");
            getting = false;
        }

    }

    /// <summary>
    /// �ǐ�
    /// </summary>
    private void Chase()
    {
        //�A�j���[�V�������Z�b�g
        m_Animator.SetBool("isWalking", isWalking);

        if (move) // Move & NoBallGet
        {
            myTr.LookAt(target.transform); //�@target������
            Vector3 relativePos = targetTr.position - myTr.position; // Target�̏ꏊ��c��

            isWalking = true; // ����

            myTr.position += relativePos.normalized * speed; // �ړ�

        }
        else //�@�����Ă��Ȃ�
        {
            isWalking = false;
        }

    }
}
