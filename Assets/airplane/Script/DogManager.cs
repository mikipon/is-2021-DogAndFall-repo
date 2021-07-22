using UnityEngine;

public class DogManager : MonoBehaviour
{
    public Transform ball;            // 追いかけるtarget
    public Transform player;            // player
    public float speed = 1.0f;           // dogの動くSpeed

    private static Transform target;   // target
    private static Transform myTr;       // Dog
    private static Transform getBallTr;  // ボールを持つ場所
    private static Animator m_Animator;
    [SerializeField]private Mode mode = Mode.idle;

    private bool touchTarget  = false;   // 触れたかどうか
    private bool getting      = false;   // ボールを持ってるか
    private bool nearPlayer   = false;   // プレイヤーの近くか

    enum Mode { idle,chase,hold,drop}

    void Start()
    {
        //初期化
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
        if (touchTarget) // targetに触れた
        {
            move = false;
            Chase(); // 追跡
            
            if(getting)
            {
                //Invoke(nameof(comeBack), 0.8f); // playerの場所に行く
                comeBack();
            }
        }
        else // 触れていない
        {
            move = true;
            Chase(); // 追跡
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
            Chase(); // 追跡
        }
        else
        {
            Debug.Log("playerの近くじゃない");
            //targetTr.position = getBallTr.position; // ボールを口に運ぶ
            Chase(); // 追跡
        }
    }

    /// <summary>
    /// ボールを持つ
    /// </summary>
    private void getBall()
    {
        ball.position = getBallTr.position; // ボールを口に運ぶ
        ball.parent = getBallTr; // 口の子オブになる
        ball.GetComponent<Rigidbody>().isKinematic = true;
        Debug.Log("ボールを持った");
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
    /// 触れたとき
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
            Debug.Log("主だー！");
            DropBall();
            mode = Mode.idle;
        }
        else
        {
            touchTarget = true;
            mode = Mode.hold;
            Invoke(nameof(getBall), 0.8f); // ボールを取る
            Debug.Log("ボールの範囲に入った");
        }
    }
    
    /// <summary>
    /// 触れたとき
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerExit(Collider col)
    {
/*
        if (col.gameObject.tag == "target")
        {
            touchTarget = false;
            Debug.Log("ボールの範囲外");
            getting = false;
        }
        else if (col.gameObject.tag == "Player")
        {
            nearPlayer = false;
            Debug.Log("渡した");
        }*/
    }

    void StartChase(Transform newTarget)
    {
        target = newTarget;
        //アニメーションをセット
        m_Animator.SetBool("isWalking", true);
        mode = Mode.chase;
    }

    void StopChase()
    {
        //アニメーションをセット
        m_Animator.SetBool("isWalking", false);

    }

    /// <summary>
    /// 追跡
    /// </summary>
    private void Chase()
    {

        myTr.LookAt(target.transform); //　targetを見る
        Vector3 relativePos = target.position - myTr.position; // Targetの場所を把握

        myTr.position += relativePos.normalized * speed; // 移動

    }
}
