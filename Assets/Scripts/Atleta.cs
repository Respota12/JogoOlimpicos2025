using UnityEngine;

public class AtletaMovimentacao : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 velocity;
    private Animator animator;

    public float velocidadeCorrida = 8f;
    public float velocidadeCaminhada = 3f;
    public float gravidade = -9.81f;
    private float velocidadeAtual = 0f;
    private float suavizacaoVelocidade = 5f;

    public float tempoDeEspera = 3f; 
    private bool podeCorrer = false; 

    public GameObject destinoFinal; 

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        velocity.y = -2f;

        animator.SetBool("Correr", false);
        Invoke("LiberarCorrida", tempoDeEspera);
    }

    void LiberarCorrida()
    {
        podeCorrer = true;
        animator.SetBool("Correr", true);
    }

    void Update()
    {
        if (!podeCorrer) return;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveHorizontal, 0, moveVertical).normalized;

        float targetSpeed = Input.GetKey(KeyCode.LeftShift) ? velocidadeCorrida : velocidadeCaminhada;
        velocidadeAtual = Mathf.Lerp(velocidadeAtual, targetSpeed, Time.deltaTime * suavizacaoVelocidade);

        if (moveDirection.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), Time.deltaTime * 10f);
        }

        controller.Move(moveDirection * Time.deltaTime * velocidadeAtual);
        animator.SetBool("Correr", moveDirection.magnitude > 0.1f);

        velocity.y += gravidade * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (destinoFinal != null && other.gameObject == destinoFinal)
        {
            Debug.Log($" {gameObject.name} tocou o cubo! Removendo...");
            Destroy(destinoFinal);
            destinoFinal = null;
        }
    }
}
