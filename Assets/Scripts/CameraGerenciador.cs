using UnityEngine;
using Cinemachine;

public class CameraGerenciador : MonoBehaviour
{
    public CinemachineVirtualCamera cameraCorrida;
    public CinemachineVirtualCamera cameraSalto;
    public CinemachineVirtualCamera cameraSaltoBarra;

    void Start()
    {
        if (cameraCorrida == null || cameraSalto == null || cameraSaltoBarra == null)
        {
            Debug.LogError("Erro: Algumas c�meras Cinemachine n�o foram atribu�das corretamente!");
        }
    }

    public void AlternarParaAtletaSalto()
    {
        if (cameraCorrida != null && cameraSalto != null)
        {
            Debug.Log("Alternando c�mera: Corrida para Salto");

            cameraCorrida.Priority = 10;
            cameraSalto.Priority = 40;
            cameraSaltoBarra.Priority = 0;
        }
        else
        {
            Debug.LogError("Erro ao alternar para a c�mera do salto!");
        }
    }

    public void AlternarParaAtletaSaltoBarra()
    {
        if (cameraSaltoBarra != null)
        {
            Debug.Log("Alternando c�mera para o terceiro atleta...");

            cameraCorrida.Priority = 0;
            cameraSalto.Priority = 10;
            cameraSaltoBarra.Priority = 50;

            Debug.Log($"Nova prioridade da c�mera de salto em barra: {cameraSaltoBarra.Priority}");
        }
        else
        {
            Debug.LogError("Erro ao alternar para a c�mera do terceiro atleta! Verifique no Inspector.");
        }
    }

}
