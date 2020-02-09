/*
 *  ver. 0.0.1
 *  Реакция задержки  от веса 
 *  и поворота оружия вешается на 
 *  родительский обьект камері
*/

using UnityEngine;
using System.Collections;

public class  WeaponSmoothing: MonoBehaviour 
{
    public Vector3 startPosition;

	public float amount = 0.02f;      // шаг сглаживания
    public float maxAmount = 0.03f;   // максимальное сглаживания
	public float smooth = 3;          //  

	private Vector3 factor = Vector3.zero;//
	private Vector3 def;              // вектор по умалчанию

    // 
	void Start ()
    {
        transform.parent.localPosition = startPosition;
        def = transform.localPosition;
	}
    //
    void LateUpdate() 
    {
        factor.x = Mathf.Clamp(-Input.GetAxis("Mouse X") * amount, -maxAmount, maxAmount);
        factor.y = Mathf.Clamp(-Input.GetAxis("Mouse Y") * amount, -maxAmount, maxAmount);

        transform.localPosition = Vector3.Lerp(transform.localPosition, def + factor, Time.deltaTime * smooth);
    }
}