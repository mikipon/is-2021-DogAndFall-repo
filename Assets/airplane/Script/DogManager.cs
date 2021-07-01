using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogManager : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 45f;

    public GameObject target;
    private NavMeshAgent agent;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        //@ƒvƒŒƒCƒ„[‚Ì•ûŒü‚ğæ“¾
        var playerDirection = new Vector3(transform.position.x, transform.position.y, transform.position.z) - transform.position;
        //@“G‚ÌŒü‚«‚ğƒvƒŒƒCƒ„[‚Ì•ûŒü‚É­‚µ‚Ã‚Â•Ï‚¦‚é
        var dir = Vector3.RotateTowards(transform.forward, playerDirection, rotateSpeed * Time.deltaTime, 0f);
        //@Zo‚µ‚½•ûŒü‚ÌŠp“x‚ğ“G‚ÌŠp“x‚Éİ’è
        transform.rotation = Quaternion.LookRotation(dir);

        //agent.speed = speed;
        agent.destination = target.transform.position;
        
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "target")
        {
            Debug.Log("“–‚½‚Á‚½");
        }

    }



    /*
   public GameObject target;           // ’Ç‚¢‚©‚¯‚étarget
   public float speed = 10.0f;         // dog‚Ì“®‚­Speed

   private static Transform targetTr;  // target
   private static Transform myTr;      // Dog
   private bool Collided = false;      // G‚ê‚½‚©‚Ç‚¤‚©
   private Animator m_Animator;
   private bool isWalking = false;
   private bool isSitting = false;

   // Start is called before the first frame update
   void Start()
   {
       //‰Šú‰»
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
   /// G‚ê‚½‚Æ‚«
   /// </summary>
   /// <param name="col"></param>
   void OnCollisionEnter(Collision col)
   {
       if (col.gameObject.tag == "target")
       {
           Collided = true;
           Debug.Log("“–‚½‚Á‚½");
           this.gameObject.SetActive(false);
       }

   }

   /// <summary>
   /// G‚ê‚é‚Ì‚ğ‚â‚ß‚½
   /// </summary>
   /// <param name="col"></param>
   private void OnCollisionExit(Collision col)
   {
       Collided = false;
   }

   /// <summary>
   /// ’ÇÕ
   /// </summary>
   private void Chase()
   {
       //ƒAƒjƒ[ƒVƒ‡ƒ“‚ğƒZƒbƒg
       m_Animator.SetBool("isWalking", isWalking);
       m_Animator.SetBool("isSitting", isSitting);

       if (!Collided) // G‚ê‚Ä‚È‚¢
       {
           Debug.Log("G‚ê‚Ä‚È‚¢");
           isWalking = true;
           myTr.LookAt(target.transform);
           Vector3 relativePos = targetTr.position - myTr.position; // Target‚ÌêŠ‚ğ”cˆ¬
           myTr.position += relativePos.normalized * speed * Time.deltaTime;


       }
       else // G‚ê‚Ä‚¢‚é
       {
           Debug.Log("G‚ê‚½");
           isWalking = false;
           isSitting = true;
       }

   }
   */

}
