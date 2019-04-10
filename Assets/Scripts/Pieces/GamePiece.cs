using Manage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePiece : MonoBehaviour
{
    private bool _isMoving;
    protected bool pathComplete;
    protected Transform pieceTransform;
    public int speed;
    public Transform path;
    public bool isMoving
    {
        get { return _isMoving; }
        set { }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    protected IEnumerator followPath(Transform obj, Transform path)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();

        foreach (Transform child in path)
        {
            PathObj script = child.GetComponent<PathObj>();
            script.setHit(false);


            StartCoroutine(goToPathObj(rb, obj, child, script));
            if (this.tag == "Bus" )
            {
                Bus bus = (Bus)this;
                if (bus.getStopBus())
                {
                    rb.velocity = Vector2.zero;
                    yield return new WaitForSeconds(1);
                    script.setHit(true);
                    bus.setStopBus(false);
                }
           
            }

            yield return new WaitUntil(() => script.getHit());



        }

        pathComplete = true;

    }

    IEnumerator goToPathObj(Rigidbody rb, Transform obj, Transform pathObj, PathObj script)
    {
        while (!script.getHit())
        {
            obj.position = Vector3.Lerp(obj.position, pathObj.position, speed * Time.deltaTime);
            obj.rotation = pathObj.rotation;
            yield return new WaitForSeconds(.01f);
        }


    }



}
