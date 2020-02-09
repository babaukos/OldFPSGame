/////////////////////////////////////////////////////////////
//                                                         //
//                                                         //
//                                                         //
/////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera/Other/Camera Shake Effect")]
public class CameraShake : MonoBehaviour
{
	// Время дрожания
	public float shake = 0f;
	// Амплитуда и величина
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;

    private Transform camTransform;
    private Vector3 originalPos;

    //----------------------------------------------------------------------------------------------------
	private void Awake()
	{
		if (camTransform == null)
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}
	}
    //
    private void OnEnable()
	{
		originalPos = camTransform.localPosition;
	}
    //
    private void LateUpdate()
	{
		if (shake > 0)
		{
			camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
			shake -= Time.deltaTime * decreaseFactor;
		}
		else
		{
			shake = 0f;
			camTransform.localPosition = originalPos;
           
		}
	}
    //-----------------------------------------------------------------------------------------------------
    public void Shake(float time) 
    {
        Shake(time, shakeAmount);
    }

    public void Shake(float time, float amount) 
    {
        shake = time;
        shakeAmount = amount;
    }
}