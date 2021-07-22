using UnityEngine;

public class DogManager : MonoBehaviour
{
    public GameObject target;            // 追いかけるtarget
    public float speed = 1.0f;           // dogの動くSpeed

    private static Transform targetTr;   // target
    private static Transform playerTr;   // target
    private static Transform myTr;       // Dog
    private static Transform getBallTr;  // ボールを持つ場所
    private static Animator m_Animator;

    private bool touchTarget  = false;      // 触れたかどうか
    private bool isWalking    = false;      // 歩くか
    private bool move         = true;       // 動いているかどうか
    private bool getting      = false;      // ボールを持ってるか

    void Start()
    {
        //初期化
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
        if (touchTarget) // targetに触れた
        {
            move = false;
            Chase(); // 追跡
            if(!getting)
                Invoke(nameof(getBall), 0.8f); // ボールを取る
            else
            {

            }
        }
        else // 触れていない
        {
            move = true;
            Chase(); // 追跡
        }
    }

    private void comeBack()
    {

    }

    /// <summary>
    /// ボールを持つ
    /// </summary>
    private void getBall()
    {
        targetTr.position = getBallTr.position; // ボールを口に運ぶ
        Debug.Log("ボールを持った");
        getting = true;
    }

    /// <summary>
    /// 触れたとき
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "target")
        {
            touchTarget = true;
            Debug.Log("ボールの範囲に入った");
        }

    }
    
    /// <summary>
    /// 触れたとき
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "target")
        {
            touchTarget = false;
            Debug.Log("ボールの範囲外");
            getting = false;
        }

    }

    /// <summary>
    /// 追跡
    /// </summary>
    private void Chase()
    {
        //アニメーションをセット
        m_Animator.SetBool("isWalking", isWalking);

        if (move) // Move & NoBallGet
        {
            myTr.LookAt(target.transform); //　targetを見る
            Vector3 relativePos = targetTr.position - myTr.position; // Targetの場所を把握

            isWalking = true; // 歩く

            myTr.position += relativePos.normalized * speed; // 移動

        }
        else //　動いていない
        {
            isWalking = false;
        }

    }
}
