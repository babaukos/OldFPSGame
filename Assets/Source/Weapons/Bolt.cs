using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    [Tooltip("масса стрелы")]
    public float mass;
    [Tooltip("наносимы урон")]
    public float damge;
    public float gravity;
    [Tooltip("сопротивление воздуху (коефициент формы)")]
    public float drag;
    public bool flaying;
    [Tooltip("звук удара об обект")]
    public AudioClip soundHit;

    private AudioSource audioSource;
    private Collider collider;

    private Vector3 velocity;
    private Vector3 direction;
    private Vector3 newPos;
    private Vector3 oldPos;

    private float distance;
    private float kineticFactor;
    private float dragFactor;
    private float impulsFactor;


    //--------------------------------------------------------------------------
    private void Start() 
    {
        if (!audioSource)
            audioSource = GetComponent<AudioSource>();
        if (!collider)
            collider = GetComponent<Collider>();
    }
    //--------------------------------------------------------------------------
    private void Update()
    {
        if (flaying)
        {
            newPos = transform.position;
            collider.enabled = false;
            transform.parent = null;
            dragFactor = 1 / (Mathf.Pow(velocity.magnitude, 2)) * drag;         // сопротивление воздуха
            kineticFactor = (mass * Mathf.Pow(velocity.magnitude, 2)) / 2;     // кинетическая енергия
            impulsFactor = mass * velocity.magnitude;                          // импульс равен

            newPos += (transform.forward + velocity) * Time.deltaTime;          // следующая позиция болта
            velocity.y -= gravity * Time.deltaTime;            // воздействие гравитации

            direction = newPos - oldPos;
            distance = direction.magnitude;
            direction /= distance;
            if (distance > 0)
            {
                RaycastHit hit;
                if (Physics.Raycast(oldPos, direction, out hit, distance))
                {
                   Collision(hit);
                }
                else 
                    {
                        oldPos = transform.position;
                        transform.position = newPos;
                        transform.rotation = Quaternion.LookRotation(direction);
                    }
            }
        }
    }
    //---------------------------------------------------------------------------
    private void Collision(RaycastHit hit) 
    {
        ObjectProperty eop = hit.collider.GetComponent<ObjectProperty>();
        //if (eop.tipeOfMaterial != MaterialProperty.TipeOfMaterial.Metal)
        //{
        //   Penetration(hit);
        //}
        //else
        //    {
        //        Deflection(hit);
        //    }
    }
    //---------------------------------------------------------------------------
    private void Penetration(RaycastHit hit)
    {
        flaying = false;
        transform.localPosition = hit.point;
        transform.parent = hit.transform;
        audioSource.PlayOneShot(soundHit);
        //actionInterface.active = true;
        collider.enabled = true;
        //Debug.Break();
    }
    //---------------------------------------------------------------------------
    private void Deflection(RaycastHit hit)
    {
        velocity = Vector3.Reflect(velocity, hit.normal);
        //velocity /= mass * 2;
        //actionInterface.active = true;
    }
    //---------------------------------------------------------------------------
#if UNITY_EDITOR
    void OnDrawGizmos() 
    {
        Custom.Utility.NGizmos.DrawSphere(transform.position, 0.007f, Color.yellow); // точка приложения сили
        Custom.Utility.NGizmos.DrawArrow(transform.position, direction * distance, Color.red);                                  // вектор скорости 
        if(flaying)
        {
            Custom.Utility.NGizmos.DrawArrow(transform.position, -transform.forward * dragFactor, Color.green);                // вектор сопротивления воздуха
            Custom.Utility.NGizmos.DrawArrow(transform.position, Vector3.down * gravity * 0.02f, Color.black);// вектор гравитации
        }
    }
#endif
    //---------------------------------------------------------------------------
    public void Run(float speed)
    {
        transform.parent = null;
        newPos = transform.position;
        oldPos = newPos;
        velocity = speed * transform.forward;      // начальная скорость болта
        flaying = true;
    }
}
