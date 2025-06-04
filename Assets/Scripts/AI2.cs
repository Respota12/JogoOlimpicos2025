using UnityEngine;
using TMPro;

public class AI2Chegada : MonoBehaviour
{
    public TextMeshProUGUI tempoAI2Text;
    public float tempoDeEspera = 3f; 
    public static float tempoAI2 = 0f;
    private bool AI2Finalizou = false;
    private bool corridaIniciada = false;

    void Start()
    {
        Invoke(nameof(IniciarCorrida), tempoDeEspera); 
    }

    void Update()
    {
        if (corridaIniciada && !AI2Finalizou)
        {
            tempoAI2 += Time.deltaTime;
            tempoAI2Text.SetText($" Tempo de Mozart: {tempoAI2:F2} s");
        }
    }

    void IniciarCorrida()
    {
        corridaIniciada = true;
        Debug.Log(" Corrida iniciada para Mozart!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Competidor>()) 
        {
            AI2Finalizou = true;
            Debug.Log($" Mozart finalizou em {tempoAI2:F2} segundos!");
        }
    }
}
