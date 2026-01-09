using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float vitesseDeplacement = 5f;
    public float hauteurSaut = 2f;
    public float gravite = -9.81f;
    public float sensibiliteSouris = 2f;
    public float vitesseMonteeEscalier = 3f;
    public float ajustementDetectionEscalierf = 0.3f;
    public float ajustementDetectionApresEscalierf = 0.7f;
    public float ajustementLoinEscalier = 1f;
    public new Transform camera;

    [Header("Système de Particules")]
    public ParticleSystem particulesMarche; // Particules lors de la marche
    public float seuilVitesseParticules = 0.1f; // Vitesse minimale pour déclencher les particules

    private AudioSource audioSource;
    private CharacterController controller;
    private float rotationVerticale = 0f;
    private Vector3 velocite;
    private bool estAuSol;
    private bool etaitAuSolPrecedent = false; // Pour détecter l'atterrissage
    private Vector3 dernierePosition;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        audioSource = GetComponent<AudioSource>();
        dernierePosition = transform.position;
    }

    void Update()
    {
        // Vérifie si le joueur touche le sol
        estAuSol = controller.isGrounded;

        if (estAuSol && velocite.y < 0) velocite.y = -2f;

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

        // Si joueur proche d'un escalier et qu'il avance, le faire monter automatiquement
        if (z > 0 && estAuSol)
        {
            Vector3 vectorPied = transform.position + Vector3.down * ((controller.height / 2) - ajustementDetectionEscalierf);
            Vector3 vectorMilieu = transform.position + Vector3.down * ((controller.height / 2) - ajustementDetectionApresEscalierf);
            Debug.DrawRay(vectorPied, transform.forward * ajustementLoinEscalier, Color.red);
            Debug.DrawRay(vectorMilieu, transform.forward * ajustementLoinEscalier, Color.blue);
            if (Physics.Raycast(vectorPied, transform.forward, out RaycastHit hit, ajustementLoinEscalier)
                && !Physics.Raycast(vectorMilieu, transform.forward, ajustementLoinEscalier))
            {
                Debug.DrawRay(hit.point, Vector3.up * vitesseMonteeEscalier * Time.deltaTime, Color.green);
                if (velocite.y < vitesseMonteeEscalier) velocite.y = vitesseMonteeEscalier;
            }
        }

        // Combine horizontal + vertical
        Vector3 mouvementTotal = (mouvement + velocite) * Time.deltaTime;
        controller.Move(mouvementTotal);

        // Gestion des particules de marche
        GererParticulesMarche();

        // Rotation souris
        float sourisX = Input.GetAxis("Mouse X") * sensibiliteSouris;
        float sourisY = Input.GetAxis("Mouse Y") * sensibiliteSouris;

        transform.Rotate(Vector3.up * sourisX);

        rotationVerticale -= sourisY;
        rotationVerticale = Mathf.Clamp(rotationVerticale, -90f, 90f);
        camera.localEulerAngles = new Vector3(rotationVerticale, 0f, 0f);

        // Mettre à jour l'état précédent
        etaitAuSolPrecedent = estAuSol;
        dernierePosition = transform.position;
    }

    void GererParticulesMarche()
    {
        if (particulesMarche == null) return;

        // Calculer la vitesse horizontale du joueur
        Vector3 vitesseHorizontale = (transform.position - dernierePosition) / Time.deltaTime;
        vitesseHorizontale.y = 0; // Ignorer le mouvement vertical
        float vitesse = vitesseHorizontale.magnitude;

        // Activer/désactiver les particules selon le mouvement
        if (estAuSol && vitesse > seuilVitesseParticules)
        {
            if (!particulesMarche.isPlaying)
            {
                particulesMarche.Play();
            }
        }
        else
        {
            if (particulesMarche.isPlaying)
            {
                particulesMarche.Stop();
            }
        }
    }
}