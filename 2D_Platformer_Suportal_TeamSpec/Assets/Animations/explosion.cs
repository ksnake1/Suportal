using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Source File Name: explosion.cs
 * Author's Name: Kyung Neung Lee
 * Last Modified by Kyung Neung Lee
 * Last Modified Date: Nov/22/2017
 * Program Description: It has destroy function for explosion animation
 * Revision History: Created explosion class and DestroyMe method to destroy explosion animation
 * 
*/

public class explosion : MonoBehaviour {


	public void DestroyMe(){
		//Destroy explosion animation when all animation is played once per trigger
		Destroy (gameObject);
	}

}
