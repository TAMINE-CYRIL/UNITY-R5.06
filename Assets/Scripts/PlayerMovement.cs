using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float vitesseDeplacement = 5f;
    public float hauteurSaut = 2f;
    public float gravite = -9.81f; // Valeur physique réaliste -9.81f
    public float sensibiliteSouris = 2f;
    public Transform camera;

    private AudioSource audioSource;
    private CharacterController controller;
    private float rotationVerticale = 0f;
    private Vector3 velocite;
    private bool estAuSol;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Vérifie si le joueur touche le sol
        estAuSol = controller.isGrounded;

        if (estAuSol && velocite.y < 0)
        {
            velocite.y = -2f; // colle le joueur au sol
        }

        // Déplacement horizontal (ZQSD)
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 direction = transform.right * x + transform.forward * z;
        Vector3 mouvement = direction.normalized * vitesseDeplacement;

        // Saut
        if (Input.GetButtonDown("Jump") && estAuSol)
        {
            velocite.y = Mathf.Sqrt(hauteurSaut * -2f * gravite);
            audioSource.Play();
        }

        // Gravité
        velocite.y += gravite * Time.deltaTime;

        // Combine horizontal + vertical
        Vector3 mouvementTotal = (mouvement + velocite) * Time.deltaTime;
        controller.Move(mouvementTotal);

        // Rotation souris
        float sourisX = Input.GetAxis("Mouse X") * sensibiliteSouris;
        float sourisY = Input.GetAxis("Mouse Y") * sensibiliteSouris;

        transform.Rotate(Vector3.up * sourisX);

        rotationVerticale -= sourisY;
        rotationVerticale = Mathf.Clamp(rotationVerticale, -90f, 90f);
        camera.localEulerAngles = new Vector3(rotationVerticale, 0f, 0f);
    }
}