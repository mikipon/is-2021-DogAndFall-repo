/*
    directionToTarget * (自分の半径+ターゲットの半径)で、自分とターゲットの半径の長さ分の向きベクトルが求められる。
    つまり、元々のターゲット座標から、この長さのベクトルを引けば、ターゲットに重ならない。また、マージンとしてattackDistanceThresholdを用意している。
    これはoffsetでもpaddingでもmarginでもどんな変数でもよくて、とりあえず、敵の攻撃範囲としている。
 */

using UnityEngine;

public class DogManager : MonoBehaviour
{
    public GameObject target;            // 追いかけるtarget
    public float speed = 1.0f;           // dogの動くSpeed

    private static Transform targetTr;   // target
    private static Transform myTr;       // Dog
    private static Animator m_Animator;
    private bool Collided  = false;      // 触れたかどうか
    private bool isWalking = false;
    private bool isSitting = false;

    void Start()
    {
        //初期化
        targetTr = target.transform;
        myTr = this.transform;
        m_Animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!Collided) { // targetに触れていない

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

        //if(Collided) // 触れている
        //{
        //    Debug.Log("触れた");
        //    isSitting = true;
            
        //}

    }
    /*
    public Transform target;

    float myCollisionRadius;
    float targetCollisionRadius;

    [Range(1.0f, 10.0f)]
    public float attackDistanceThreshold = 1.0f;

    private NavMeshAgent agent;
    private float nextAttackTime;
    private bool touch = false;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        myCollisionRadius = GetComponent<CapsuleCollider>().radius;
        targetCollisionRadius = target.GetComponent<CapsuleCollider>().radius;

        StartCoroutine(UpdatePath());
    }

    void Update()
    {
        // Time.timeはゲーム開始からの秒。
        if (Time.time > nextAttackTime)
        {

            //// 距離を比較するときは、平方根(Mathf.Sqrt)のコストが高いので、距離の二乗通しを計算することで、パフォーマンスをあげる。
            //// 現在のターゲットと自身の距離の二乗。
            //float sqrMag = (target.position - transform.position).sqrMagnitude;
            //// 攻撃開始の閾値の二乗
            //float sqrAttackRange = Mathf.Pow(myCollisionRadius + targetCollisionRadius + attackDistanceThreshold, 2);
            //if (sqrMag < sqrAttackRange)
            //{
            //    nextAttackTime = Time.time + timeBetweenAttacks;
            //    Debug.Log("Attack");
            //}
        }
    }

    IEnumerator UpdatePath()
    {
        while (this.target.position == target.position)//(target != null)
        {
            // ターゲットの中心にまで移動する
            //Vector3 targetPosition = new Vector3(target.position.x, 0f, target.position.z);
            //agent.SetDestination(targetPosition);

            // 方向を求める
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            Vector3 targetPosition = target.position - directionToTarget * (myCollisionRadius + targetCollisionRadius + attackDistanceThreshold / 2);
            //Debug.Log(targetPosition);
            agent.SetDestination(targetPosition);

            if (touch)
            {
                Debug.Log("触った");
            }

            // １秒ウェイト
            yield return new WaitForSeconds(.1f);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "target")
        {
            touch = true;
        }

    }

    private void OnCollisionExit(Collision col)
    {
        touch = false;
    }
    */

    //public GameObject target;
    //private NavMeshAgent agent;

    //// Use this for initialization
    //void Start()
    //{
    //    agent = GetComponent<NavMeshAgent>();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    //agent.speed = speed;
    //    agent.destination = target.transform.position;

    //}



    /*
   public GameObject target;           // 追いかけるtarget
   public float speed = 10.0f;         // dogの動くSpeed

   private static Transform targetTr;  // target
   private static Transform myTr;      // Dog
   private bool Collided = false;      // 触れたかどうか
   private Animator m_Animator;
   private bool isWalking = false;
   private bool isSitting = false;

   // Start is called before the first frame update
   void Start()
   {
       //初期化
       targetTr = target.transform;
       myTr = this.transform;
       m_Animator = GetComponent<Animator>();

   }

   // Update is called once per frame
   void Update()
   {
       Chase();
   }

   /// <summary>
   /// 触れたとき
   /// </summary>
   /// <param name="col"></param>
   void OnCollisionEnter(Collision col)
   {
       if (col.gameObject.tag == "target")
       {
           Collided = true;
           Debug.Log("当たった");
           this.gameObject.SetActive(false);
       }

   }

   /// <summary>
   /// 触れるのをやめた
   /// </summary>
   /// <param name="col"></param>
   private void OnCollisionExit(Collision col)
   {
       Collided = false;
   }

   /// <summary>
   /// 追跡
   /// </summary>
   private void Chase()
   {
       //アニメーションをセット
       m_Animator.SetBool("isWalking", isWalking);
       m_Animator.SetBool("isSitting", isSitting);

       if (!Collided) // 触れてない
       {
           Debug.Log("触れてない");
           isWalking = true;
           myTr.LookAt(target.transform);
           Vector3 relativePos = targetTr.position - myTr.position; // Targetの場所を把握
           myTr.position += relativePos.normalized * speed * Time.deltaTime;


       }
       else // 触れている
       {
           Debug.Log("触れた");
           isWalking = false;
           isSitting = true;
       }

   }
   */

}
