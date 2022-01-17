using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    public int force;
    Rigidbody2D rb;
    int scoreP1;
    int scoreP2;
    Text scoreUIP1; //Meyimpan GameObjcet text
    Text scoreUIP2;
    GameObject panelSelesai;
    Text txPemenang;
    AudioSource audio;
    public AudioClip hitSound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
        panelSelesai = GameObject.Find ("PanelSelesai"); //Memanggil GameObject PanelSelesai yang terdapat di Hierarchy. 
        panelSelesai.SetActive (false); //Kemudian memastikan dalam keadaaan nonaktif.

        Vector2 arah = new Vector2 (2, 0).normalized; //Menyatakan arah dari Force, yaitu dua satuan ke kanan dan 0 satuan ke atas.
        rb.AddForce (arah * force); //Melontarkan bola sesuai dengan arah dan kekuatan.
        scoreP1 = 0;
        scoreP2 = 0;

        scoreUIP1 =  GameObject.Find ("Score1").GetComponent<Text>(); //Digunakan untuk mengakses GameObject yang memiliki nama Score1 dan Score2. 
        scoreUIP2 = GameObject.Find ("Score2").GetComponent<Text>(); //Kemudian dari GameObject tersebut dicari komponen Text yang ada didalamnya yang kemudian disimpan ke scoreUIP1 dan ScoreUIP2.

        audio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D (Collision2D coll) {
        audio.PlayOneShot(hitSound);
        if (coll.gameObject.name == "BatasKanan") {
            scoreP1 += 1; // ketika bola menyentuh "BatasKanan" maka score + 1
            TampilkanScore();
            if (scoreP1 == 5) {
                panelSelesai.SetActive(true);
                txPemenang = GameObject.Find("Pemenang").GetComponent<Text>();
                txPemenang.text = "Player Biru Menang!";
                Destroy(gameObject); //Hilangkan Bola.
                return; //Kode selesai dan tidak dilanjutkan untuk membaca kode selanjutnya.
            }

            ResetBall();
            Vector2 arah = new Vector2 (2, 0).normalized;
            rb.AddForce (arah * force);
        }
        if (coll.gameObject.name == "BatasKiri") {
            scoreP2 += 1; // ketika bola menyentuh "BatasKiri" maka score + 1
            TampilkanScore();
            if (scoreP2 == 5) {
                panelSelesai.SetActive(true);
                txPemenang = GameObject.Find("Pemenang").GetComponent<Text>();
                txPemenang.text = "Player Merah Menang!";
                Destroy(gameObject);
                return;
            }

            ResetBall();
            Vector2 arah = new Vector2 (-2, 0).normalized; //Menentukan arah pergerakan bola. Nilai 2 berarti bola mengarah ke kanan.
            rb.AddForce (arah * force); //Melontarkan bola berdasarkan arah dan kecepatan bola.

        }
        if (coll.gameObject.name == "Pemukul1" || coll.gameObject.name == "Pemukul2") { //Memastikan objek yang bersentuhan dengan bola adalah GameObject Pemukul1 dan Pemukul2.
            float sudut =(transform.position.y - coll.transform.position.y) * 5f; //Kemudian hitung seberapa kemiringan yang diberikan ke bola.
            Vector2 arah = new Vector2(rb.velocity.x, sudut).normalized; //Menentukan arah bola yang akan dipantulkan.
            rb.velocity = new Vector2 (0, 0); //Reset gerak bola (dengan kode ini, bola akan diam).

            /*Implementasikan arah, kekuatan lontar dan kecepatan setelah bola menyentuh Paddle. 
            Di bagian ini Anda dapat memodifikasi untuk bola semakin cepat dengan menambahkan 
            kelipatan setiap menjalankan kode ini.*/
            rb.AddForce (arah * force * 2); 
        }
    }

    void ResetBall () {
        transform.localPosition = new Vector2 (0, 0);
        rb.velocity =  new Vector2 (0, 0);
    }
    void TampilkanScore() { //Digunakan untuk mengimplementasikan penampilan skor dengan memanggil fungsi
        Debug.Log ("Score P1: " + scoreP1 + " Score P2: " + scoreP2);
        scoreUIP1.text = scoreP1 + "";
        scoreUIP2.text = scoreP2 + "";
    }
}
