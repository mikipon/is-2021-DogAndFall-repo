using UnityEngine;

public class DogManager : MonoBehaviour
{
    public GameObject target;            // 追いかけるtarget
    public float speed = 1.0f;           // dogの動くSpeed

    private static Transform targetTr;   // target
    private static Transform myTr;       // Dog
    private static Animator m_Animator;
    private bool Collided = false;      // 触れたかどうか
    private bool isWalking = false;
    private bool isSitting = false;

    void Start()
    {
        //初期化
        targetTr = target.transform;
        myTr = this.transform;
        m_Animator = GetComponent<Animator>();

    }

    void Update()
    {
        if (!Collided)
        { // targetに触れていない

            isWalking = true; // 歩く
            Chase(Collided); // 動く

        }
        else
        {
            Chase(Collided); // 動く
            isWalking = false;

            //if () // ボールを拾ったら
            //{
            // カメラの場所に返ってくる targetをCameraにすればいい
            // isWalkingをtrueにする
            // Chase(Collided)のColliderをfalseにする
            // カメラの位置に来たらColliderをfalse、isWalkingをfalse、isSittingをTrue
            //}

        }
    }

    /// <summary>
    /// 触れたとき
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "target")
        {
            Collided = true;
            Debug.Log("当たった");
        }

    }

    /// <summary>
    /// 触れるのをやめた
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerExit(Collider col)
    {
        //Collided = false;
        Debug.Log("抜けちゃった");
    }

    /// <summary>
    /// 追跡
    /// </summary>
    private void Chase(bool move)
    {
        //アニメーションをセット
        m_Animator.SetBool("isWalking", isWalking);
        m_Animator.SetBool("isSitting", isSitting);

        if (!move)
        {
            myTr.LookAt(target.transform);
            Vector3 relativePos = targetTr.position - myTr.position; // Targetの場所を把握
            myTr.position += relativePos.normalized * speed; // 移動
        }
        else
        {
            isSitting = true;
        }

    }
}
