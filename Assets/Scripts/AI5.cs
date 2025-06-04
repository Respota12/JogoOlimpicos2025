using UnityEngine;
using TMPro;

public class AI5Chegada : MonoBehaviour
{
    public TextMeshProUGUI tempoAI5Text;
    public float tempoDeEspera = 3f; 
    public static float tempoAI5 = 0f;
    private bool AI5Finalizou = false;
    private bool corridaIniciada = false;

    void Start()
    {
        Invoke(nameof(IniciarCorrida), tempoDeEspera); 
    }

    void Update()
    {
        if (corridaIniciada && !AI5Finalizou)
        {
            tempoAI5 += Time.deltaTime;
            tempoAI5Text.SetText($" Tempo do Trump: {tempoAI5:F2} s");
        }
    }

    void IniciarCorrida()
    {
        corridaIniciada = true;
        Debug.Log(" Corrida iniciada para Trump!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Competidor>()) 
        {
            AI5Finalizou = true;
            Debug.Log($" Trump finalizou em {tempoAI5:F2} segundos!");
        }
    }
}
