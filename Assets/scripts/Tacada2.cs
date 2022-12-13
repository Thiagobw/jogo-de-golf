using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tacada : MonoBehaviour
{
    public float velocity;
    private float x, z;
    public float maxX, maxZ;
    private Vector2 pi, pf;
    Rigidbody rb;
    LineRenderer lr;

    int shots = 0;
    int record = 999;
    public int par = 0;
    public int pontos = 0;
    public Text txtShots;
    public Text txtPar;
    public Text txtRecord;
    public Text txtPontos;
    public Text result;
    public Text txtVelocity;
    public GameObject winCanva;
    public Text txtMensagem;


    // Start is called before the first frame update
    void Start()
    {
        txtPar.text = "Par: " + par;
        txtRecord.text = "Recorde: " + PlayerPrefs.GetInt("record");
        txtPontos.text = PlayerPrefs.GetInt("score").ToString();
        lr = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody>();
        txtMensagem.enabled = false;
        result.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = rb.velocity.magnitude;
        txtVelocity.text = "Velocidade: " + velocity;
        if (lr.enabled)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch t = Input.GetTouch(i);

                if (t.phase == TouchPhase.Began)
                {
                    pi = t.position;
                    pf = t.position;
                    x = 0;
                    z = 0;
                    lr.enabled = true;
                    lr.SetPosition(0, transform.position);
                    lr.SetPosition(1, transform.position);
                }

                if (t.phase == TouchPhase.Moved)
                {
                    pf = t.position;
                    x = (pi.x - pf.x) * 0.03f;
                    z = (pi.y - pf.y) * 0.03f;
                    if (x > maxX)
                        x = maxX;
                    if (z > maxZ)
                        z = maxZ;
                    lr.SetPosition(1, new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z));
                }

                if (t.phase == TouchPhase.Ended)
                {
                    rb.AddForce(new Vector3(2 * x, 0, 2 * z), ForceMode.Impulse);
                    lr.enabled = false;
                    shots++;
                    txtShots.text = "Tacadas: " + shots;
                }
            }
        }

        if (velocity < 0.2)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            lr.enabled = true;
        }
        else
            lr.enabled = false;
    }

    void OnCollisionEnter(Collision outro)
    {
        int score = shots - par;
        if (outro.gameObject.tag == "fakewall")
            Destroy(GameObject.FindWithTag("fakewall"));
        if (outro.gameObject.tag == "Caixa")
        {
            Destroy(GameObject.FindWithTag("Bola"));
            if (score <= -3)
                txtMensagem.text = "Albatross";
            else if (score == -2)
                txtMensagem.text = "Eagle";
            else if (score == -1)
                txtMensagem.text = "Birdie";
            else if (score == 0)
                txtMensagem.text = "Par";
            else if (score == 1)
                txtMensagem.text = "Bogey";
            else if (score == 2)
                txtMensagem.text = "Double Bogey";
            else
                txtMensagem.text = "Triple Bogey";

            pontos = PlayerPrefs.GetInt("score") + score;
            PlayerPrefs.SetInt("score", pontos);
            txtPontos.text = PlayerPrefs.GetInt("score").ToString();
            result.text = score + " Pontos";
            txtMensagem.enabled = true;
            result.enabled = true;
            if (PlayerPrefs.GetInt("score") < record)
            {
                PlayerPrefs.SetInt("record", PlayerPrefs.GetInt("score"));
                txtRecord.text = "Recorde: " + PlayerPrefs.GetInt("record");
            }
            winCanva.SetActive(true);
        }
    }
}