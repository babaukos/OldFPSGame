using UnityEngine;
using System.Collections;

public class PlayerAudioController : MonoBehaviour 
{
    public AudioClip[] voiceSound;

	public AudioClip stepSound;
    public AudioClip jampSound;

	public AudioClip breatheSound;

    private CharacterController charContr;
    private AudioSource audSourc;


    bool jamping = false;

	// Use this for initialization
	void Start () 
	{
        charContr = GetComponent<CharacterController>();
        audSourc = GetComponent<AudioSource>();
        audSourc.clip = stepSound;
        audSourc.PlayOneShot(voiceSound[Random.Range(0,2)]);
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (charContr.isGrounded && charContr.velocity.magnitude > 2 && !audSourc.isPlaying) 
        {
            audSourc.volume = Random.Range(0.8f, 1);
            //audSourc.pitch = Random.Range(0.8f, 1);
            audSourc.Play();
        }
        if (Input.GetButtonDown("Jump")) 
        {
            jamping = true;    
        }
        //if (charContr.isGrounded && jamping)
        //{
        //    audSourc.PlayOneShot(jampHitSound, 0.3f);
        //    jamping = false;
        //}
	}
    void OnControllerColliderHit (ControllerColliderHit hit)
    {
       if (hit.gameObject.tag == "Ground")
       {
           if (jamping)
           {
               audSourc.PlayOneShot(jampSound, 0.3f);             
           }
       }
       jamping = false;    // reset jumping variable     
  }
    //
    public AudioSource GetAudiosourse()
    {
        return audSourc;
    }
}
