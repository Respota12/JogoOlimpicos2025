using UnityEngine;
using UnityEngine.AI;

public class Competidor : MonoBehaviour
{
    private NavMeshAgent agente;
    private Animator animator;
    public GameObject destinoFinal;
    public float tempoDeEspera = 3f;

    private bool corridaIniciada = false;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); 

        if (destinoFinal == null)
        {
            Debug.LogError("DestinoFinal não está atribuído! Defina-o no Inspector.");
            return;
        }

        agente.speed = 0f;
        animator.SetBool("Mover", false);

        Invoke("IniciarCorrida", tempoDeEspera);
    }

    void IniciarCorrida()
    {
        corridaIniciada = true;
        agente.speed = 5f;
        agente.SetDestination(destinoFinal.transform.position);
        animator.SetBool("Mover", true);
    }

    void Update()
    {
        if (!corridaIniciada) return;
        if (!agente.isOnNavMesh) return;

        if (agente.remainingDistance < 0.5f && corridaIniciada)
        {
            Debug.Log($" {gameObject.name} chegou ao destino final!");

            corridaIniciada = false;
            animator.SetBool("Mover", false);

            
            if (destinoFinal != null)
            {
                Debug.Log(" Removendo cubo do competidor...");
                Destroy(destinoFinal);
                destinoFinal = null;
            }
        }
    }
}
