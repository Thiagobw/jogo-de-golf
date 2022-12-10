using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tacada : MonoBehaviour
{
    public float mX, mY;
    private float x, y;
    private Vector2 pi, pf;
    LineRenderer lr;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        if (lr == null)
        {
            Debug.Debug.Log("add lR!");
            lr.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Input.TouchCount; i++)
        {
            Touch t = Input.GetTouch(i);

            if (t.phase == TouchPhase.Began)
            {
                pi = t.position;
                pf = t.position;
                x = 0;
                y = 0;
                lr.enabled = true;
                lr.SetPosition(0, transform.position);
                lr.SetPosition(1, transform.position);
            }

            if (t.phase == TouchPhase.Moved)
            {
                pf = t.position;
                x = (pi.x - pf.x) * 0.03f;
                y = (pi.y - pf.y) * 0.03f;
                if (x > mX)
                {
                    x = mX;
                }
                if (y > mY)
                {
                    y = mY;
                }
                lr.SetPosition(1, new Vector3(transform.position.x + x, transform.position.y, transform.position.z + y));
            }

            if (t.phase == TouchPhase.Ended)
            {
                GetComponent<RigidBody>().AddForce(mew Vector3(2 * x, 0, 2 * y), ForceMode.Impulse);
                lr.enabled = false;
            }
        }
    }
}
