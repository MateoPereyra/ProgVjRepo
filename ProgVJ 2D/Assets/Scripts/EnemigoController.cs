using UnityEngine;

public class EnemigoController : MonoBehaviour
{

    [SerializeField] private ArmeriaManager armeriamanager;
    [SerializeField] private Transform destino;
    [SerializeField] private ReclutaController recluta;


    private Vector3 puntoDestino;
    private bool enAldea;
    private bool enPelea;
    //private Animator anim;

    private int vida;
    private int poder;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        armeriamanager = FindFirstObjectByType<ArmeriaManager>();
        destino = GameObject.Find("Armeria").transform;
        puntoDestino = (destino.position - transform.position).normalized;

        //anim = GetComponentInChildren<Animator>();


        enAldea = false;
        enPelea = false;
        vida = 75;
        poder = 25;
    }

    // Update is called once per frame
    void Update() {

        if (!enPelea) {
            transform.Translate(puntoDestino * Time.deltaTime);
        }
    }


    //Detecta cuando el enemigo entra a la zona de la aldea para ser atacado, y cuando esta peleando
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.name == "Aldea") {
            enAldea = true;
        }

        if (other.GetComponent<ReclutaController>() != null) {

            enPelea = true;
            Vector3 pos = transform.position;
            transform.position = pos;

            InvokeRepeating("atacar", 1, 3);
            //AtacarAnim();
        }
    }

    public bool GetEnemigoPos() { 
    
        return enAldea;

    }

    private void OnTriggerExit2D(Collider2D other) {

        if (other.GetComponent<ReclutaController>() != null) {
            enPelea = false;
            
        }
    }

    private void atacar() {
        recluta.recibirDanio(poder);
    }

    //Aun no funciona, no esta bien implementado
    //private void AtacarAnim() {

    //    anim.Play("0_Attack_Normal");

    //}

}
