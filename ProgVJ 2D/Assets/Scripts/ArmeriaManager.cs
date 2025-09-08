using UnityEngine;
using TMPro;

public class ArmeriaManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private TextMeshProUGUI textoOre;
    [SerializeField] private TextMeshProUGUI textMonedas;
    [SerializeField] private TextMeshProUGUI textArmas;
    [SerializeField] private GameObject panelArmeria;

    [SerializeField] [Range(10, 2000)] private int monedas;
    [SerializeField] [Range(10, 2000)] private int costoOre;

    private int cantidadOre;
    private int cantidadArmas;

    void Start() {

        cantidadOre = 0;
        cantidadArmas = 0;
        actTexMonedas();
        InvokeRepeating("crearArma", 0, 6);

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
    /////////////////////////////////////////////////////////////////
    
    
    
    //Metodo para comprar recursos
    public void comprarOre() {

        if (costoOre <= monedas) {

            cantidadOre++;
            monedas -= costoOre;
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

    //Metodo para generar armas cada X segundos si hay recursos suficientes
    public void crearArma() {

        if (cantidadOre > 0) {
            cantidadOre--;
            cantidadArmas++;
            actTexArmas();
            actTexOre();
        }

    }

}
