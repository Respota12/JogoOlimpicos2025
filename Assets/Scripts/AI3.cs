using UnityEngine;
using TMPro;

public class AI3Chegada : MonoBehaviour
{
    public TextMeshProUGUI tempoAI3Text;
    public float tempoDeEspera = 3f; 
    public static float tempoAI3 = 0f;
    private bool AI3Finalizou = false;
    private bool corridaIniciada = false;

    void Start()
    {
        Invoke(nameof(IniciarCorrida), tempoDeEspera); 
    }

    void Update()
    {
        if (corridaIniciada && !AI3Finalizou)
        {
            tempoAI3 += Time.deltaTime;
            tempoAI3Text.SetText($" Tempo de Jeyniglis: {tempoAI3:F2} s");
        }
    }

    void IniciarCorrida()
    {
        corridaIniciada = true;
        Debug.Log(" Corrida iniciada para Jeyniglis!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Competidor>()) 
        {
            AI3Finalizou = true;
            Debug.Log($" Jeyniglis finalizou em {tempoAI3:F2} segundos!");
        }
    }
}
