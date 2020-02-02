using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_TexturePanning : MonoBehaviour
{
	
	public float ScrollX = -1f;
	public float ScrollY = -1f;
	//GameObject affecter 
	public GameObject AffectedGameObject;
	//ID du Materiel que tu veux faire glisser la texture
	public int MaterialSlotID = 0;
	public bool TapisRoulantActive = false;
	
	
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
		
        float OffsetX = Time.time * ScrollX;
		float OffsetY = Time.time * ScrollY;
		
		
		if(TapisRoulantActive == false){
			OffsetX = 0;
			OffsetY = 0;
		}
		
		AffectedGameObject.GetComponent<Renderer>().materials[MaterialSlotID].mainTextureOffset = new Vector2(OffsetX, OffsetY);
    }
}
