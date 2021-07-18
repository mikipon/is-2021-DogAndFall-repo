using UnityEngine;

public class DogManager : MonoBehaviour
{
    public GameObject target;            // �ǂ�������target
    public float speed = 1.0f;           // dog�̓���Speed

    private static Transform targetTr;   // target
    private static Transform myTr;       // Dog
    private static Animator m_Animator;
    private bool Collided = false;      // �G�ꂽ���ǂ���
    private bool isWalking = false;
    private bool isSitting = false;

    void Start()
    {
        //������
        targetTr = target.transform;
        myTr = this.transform;
        m_Animator = GetComponent<Animator>();

    }

    void Update()
    {
        if (!Collided)
        { // target�ɐG��Ă��Ȃ�

            isWalking = true; // ����
            Chase(Collided); // ����

        }
        else
        {
            Chase(Collided); // ����
            isWalking = false;

            //if () // �{�[�����E������
            //{
            // �J�����̏ꏊ�ɕԂ��Ă��� target��Camera�ɂ���΂���
            // isWalking��true�ɂ���
            // Chase(Collided)��Collider��false�ɂ���
            // �J�����̈ʒu�ɗ�����Collider��false�AisWalking��false�AisSitting��True
            //}

        }
    }

    /// <summary>
    /// �G�ꂽ�Ƃ�
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "target")
        {
            Collided = true;
            Debug.Log("��������");
        }

    }

    /// <summary>
    /// �G���̂���߂�
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerExit(Collider col)
    {
        //Collided = false;
        Debug.Log("�����������");
    }

    /// <summary>
    /// �ǐ�
    /// </summary>
    private void Chase(bool move)
    {
        //�A�j���[�V�������Z�b�g
        m_Animator.SetBool("isWalking", isWalking);
        m_Animator.SetBool("isSitting", isSitting);

        if (!move)
        {
            myTr.LookAt(target.transform);
            Vector3 relativePos = targetTr.position - myTr.position; // Target�̏ꏊ��c��
            myTr.position += relativePos.normalized * speed; // �ړ�
        }
        else
        {
            isSitting = true;
        }

    }
}
