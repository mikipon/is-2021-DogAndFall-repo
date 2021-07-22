using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogDirecter : MonoBehaviour
{
    public Transform target;
    public Transform ball;
    public Transform player;
    public float targetDist = 1;
    public float ration = 0.3f;
    public float speed = 3;
    [SerializeField] Transform holdPos;
    [SerializeField] Mode mode = Mode.idle;
    Animator m_Animator;
    bool isWalking = false;
    Transform m_Transform;
    bool isHold = false;


    int walkState = Animator.StringToHash("isWalking");
    int runState = Animator.StringToHash("isRunning");

    enum Mode { throwing, idle, chase ,drop}

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        targetDist *= targetDist;
        m_Transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        switch (mode)
        {
            case Mode.idle:
                if (target != null) StartChase();
                break;

            case Mode.chase:
                Chase();
                break;

            case Mode.throwing:
                target = ball;
                break;
            case Mode.drop:

                break;
        }

    }
    

    void StartChase()
    {
        mode = Mode.chase;
        m_Animator.SetBool(walkState, true);
    }

    void Chase()
    {
        if (target == null) return;

        Vector3 dist = target.position - m_Transform.position;

        m_Transform.rotation = Quaternion.Lerp(m_Transform.rotation, Quaternion.LookRotation(dist, m_Transform.up), ration);
        m_Transform.position += m_Transform.forward * speed * Time.deltaTime;

        if (dist.sqrMagnitude > targetDist) return;

        StopChase();
        if (isHold)
        {
            mode = Mode.drop;
        }
        else
        {
            isHold = true;
            target.parent = holdPos;
            
            target.localPosition = new Vector3();
            target = player;
        }

    }

    void StopChase()
    {
        m_Animator.SetBool(walkState, false);
    }

}
