#pragma strict
//var totalTime : float;
var waitTime : float;
var rend : Renderer;
var alpha : float = 0.0;
var startMat : Material;
var finalMat : Material;
var duration : float = 2.0;
var finishedRezIn : boolean = false;
var derezzed : boolean = false;

function Start () {
	//totalTime = Random.Range(1.0,3.0);
	waitTime = Time.time + Random.Range(0.0,2.0);
	rend = GetComponent.<Renderer>();
	rend.enabled = false;
	rend.material.color.a = 0.0;
	startMat = rend.material;
	//Debug.Log("startMat = " + startMat);
	//DynamicGI.SetEmissive(rend, new Color(1f, 0.1f, 0.5f, 1.0f) * 0.0);  // no effect
	//rend.material.SetFloat("_EmissionValue", 0.0); // does nothing
	//rend.material.SetColor("_EmissionColor", color); // error; not sure how to set alpha in that color
	//rend.material.SetFloat("_Emission", 0.0); // does nothing
	//rend.material.SetColor("_EmissionColor", Color.red); // OK! changes color to red
	//rend.material.SetFloat("_EmissionValue", 0.0); // does nothing
	//rend.material.SetFloat("_EmissionScale", 0.0);
	//rend.material.SetFloat("_Emission", 0.0);
	
}

function Update () {

	// Change alpha over time.  Does not effect emission.  Cubes change from transparent to opaque.
	if (Time.time > waitTime && Time.time < waitTime+1) {
		alpha = Time.time - waitTime;
		rend.enabled = true;
		rend.material.color.a = alpha;
	}
	
	// Switch to final material -- bldg dark or bldg light
	if (Time.time > waitTime+1  && finishedRezIn == false) {
		alpha = 1.0;
		rend.material.color.a = alpha;
		rend.material = finalMat; 	// works fine, just a bit too sudden.
		finishedRezIn = true;
	}
	
	
/*	
	// De-rez animation for end of level
	if (Time.time > 15 + waitTime && !derezzed) {
		rend.material = startMat;
		rend.material.color.a = 16 - Time.time;
	}
	if (Time.time > 16 + waitTime) {
		rend.enabled = false;
		derezzed = true;
	}
*/
		
	/*
	// CODE THAT DOES NOT WORK...
	
	// Problem: finalMat does not have emission
	if (Time.time > waitTime+1) {
		//rend.material.color.a = 1.0;  // causes light blocks to be dark ???
		var lerp : float = Mathf.PingPong(Time.time, duration) / duration;
		rend.material.Lerp(startMat, finalMat, lerp);
	}

	// Problem trying to change the emissive value does not work in any way/method
	if (Time.time > waitTime && Time.time < waitTime+1) {
		alpha = Time.time - waitTime;
		//rend.material.color.a = alpha;  //cube starts out emitting full light.  it is transparent, then becomes opaque
		//rend.material.SetFloat("_EmissiveValue", alpha);
		//rend.material.SetFloat("_EmissionScale", alpha); // does nothing
		//DynamicGI.SetEmissive(rend, new Color(1f, 0.1f, 0.5f, 1.0f) * alpha); // no effect
		Debug.Log("alpha = " + alpha);
	}
	*/
}