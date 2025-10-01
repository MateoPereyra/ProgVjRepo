using UnityEngine;
using TMPro;

public class ArmeriaManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private TextMeshProUGUI textoOre;
    [SerializeField] private TextMeshProUGUI textMonedas;
    [SerializeField] private TextMeshProUGUI textArmas;
    [SerializeField] private TextMeshProUGUI textReclutas;

    [SerializeField] private GameObject panelArmeria;
    [SerializeField] private GameObject reclutaPrefab;

    [SerializeField][Range(10, 2000)] private int monedas;
    [SerializeField] private int resistencia;

    private int cantidadOre;
    private int cantidadArmas;
    private int cantidadRecluta;

    void Start() {

        cantidadOre = 0;
        cantidadArmas = 0;
        actTexMonedas();
        InvokeRepeating("crearArma", 0, 6);

        resistencia = 200;

    }

    //////////////////Actualizaciones de texto///////////////////////
    public void actTexOre () {
        textoOre.text = "x " + cantidadOre;
    }

    public void actTexArmas() {
        textArmas.text = "x " + cantidadArmas;
    }

    public void actTexMonedas() {
        textMonedas.text = monedas.ToString();
    }

    public void actTexReclutas() {
        textReclutas.text = "x " + cantidadRecluta;
    }
    /////////////////////////////////////////////////////////////////
    
    
    
    //Metodo para comprar recursos
    public void comprarOre() {

        if (GameManager.instancia.JuegoData.CostoOre <= monedas) {

            cantidadOre++;
            monedas -= GameManager.instancia.JuegoData.CostoOre;
            actTexOre();
            actTexMonedas();

        }

    }

    //Metodo para esconder/activar el panel
    public void OnMouseDown() {

        if (panelArmeria.activeSelf) {
            panelArmeria.SetActive(false);
        } else { 
        panelArmeria.SetActive(true);
        }

    }

    //Metodo para generar armas cada X segundos si hay recursos suficientes (será más util más adelante)
    public void crearArma() {

        if (cantidadOre > 0) {
            cantidadOre--;
            cantidadArmas++;
            actTexArmas();
            actTexOre();
        }

    }

    //Instancia un nuevo recluta para convertir en otra unidad segun su arma (por ahora, solo espadachin)
    public void reclutar() {
        if (GameManager.instancia.JuegoData.CostoRecluta <= monedas) {

            monedas -= GameManager.instancia.JuegoData.CostoRecluta;
            actTexMonedas();

            cantidadRecluta++;
            GameObject recluta = Instantiate(reclutaPrefab);
            //recluta.transform.position += Vector3.right * cantidadRecluta;
            int columnasPorFila = 2; // cada fila tiene 2 reclutas
            int columna = (cantidadRecluta % columnasPorFila) + 1; // +1 para empezar en X=1
            int fila = (cantidadRecluta / columnasPorFila) + 1;     // +1 para empezar en Y=1

            float distanciaX = 1f; // separación horizontal entre reclutas
            float distanciaY = 1f; // separación vertical entre filas
            float offsetX = -20f; // desplazamiento inicial en X

            recluta.transform.position = new Vector3(
                offsetX + (columna * distanciaX),
                fila * distanciaY,
                0f
            );
            actTexReclutas(); 
        }
    }

    public bool getArmas() {

        return cantidadArmas > 0;

    }

    //Cuando un recluta llega a la armeria
    private void OnTriggerEnter2D(Collider2D other) {


        if (other.GetComponent<ReclutaController>() != null) {

            cantidadArmas--;
            actTexArmas();
            cantidadRecluta--;
            actTexReclutas();

        }
    }


    // Método para recibir daño
    public void RecibirDanio(int danio) {
        resistencia -= danio;
            if (resistencia > 0) {
            Debug.Log("Armería: " + resistencia + " HP restantes");
        } else {
            Derrota();
        }
   }

    public void Derrota() { 
        GameManager.instancia.GameOver();
        Destroy(gameObject);
    }
}
