using UnityEngine;
using TMPro;

public class AtletaChegada : MonoBehaviour
{
    public TextMeshProUGUI tempoAtletaText;
    public static float tempoAtleta = 0f;
    private bool atletaFinalizou = false;
    private bool corridaIniciada = false;

    void Start()
    {
        Invoke(nameof(IniciarCorrida), 3f);
    }

    void Update()
    {
        if (corridaIniciada && !atletaFinalizou)
        {
            tempoAtleta += Time.deltaTime;
            tempoAtletaText.SetText($" Tempo de Sergio: {tempoAtleta:F2} s");
        }
    }

    void IniciarCorrida()
    {
        corridaIniciada = true;
        Debug.Log(" Corrida iniciada para o Sergio!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AtletaMovimentacao>())
        {
            atletaFinalizou = true;
            FinalizarCorrida();
        }
    }

    void FinalizarCorrida()
    {
        Debug.Log($" Sergio finalizou em {tempoAtleta:F2} segundos!");
        VencedorChegada.VerificarVencedor();
    }
}
