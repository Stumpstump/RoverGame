﻿using UnityEngine;
using System.Collections;
using ScionEngine;

public class AutoIntensity : MonoBehaviour
{
    public ScionPostProcess scion;
	public Gradient nightDayColor;

	public float maxIntensity = 3f;
	public float minIntensity = 0f;
	public float minPoint = -0.2f;

	public float maxAmbient = 1f;
	public float minAmbient = 0f;
	public float minAmbientPoint = -0.2f;


	public Gradient nightDayFogColor;
	public AnimationCurve fogDensityCurve;

    public AnimationCurve maxExposure;
    public AnimationCurve minExposure;

	public float fogScale = 1f;

	public float dayAtmosphereThickness = 0.4f;
	public float nightAtmosphereThickness = 0.87f;

	public Vector3 dayRotateSpeed;
	public Vector3 nightRotateSpeed;
    public bool go = false;

    public bool colorOnly = false;

	float skySpeed = 1;


	Light mainLight;
	Skybox sky;
	Material skyMat;

	void Start () 
	{
	
		mainLight = GetComponent<Light>();
		skyMat = RenderSettings.skybox;

	}

	void FixedUpdate () 
	{
	    if(go)
        {
            if(colorOnly)
            {
                float tRange = 1 - minPoint;
                float dot = Mathf.Clamp01((Vector3.Dot(mainLight.transform.forward, Vector3.down) - minPoint) / tRange);
                dot = Mathf.Clamp01((Vector3.Dot(mainLight.transform.forward, Vector3.down) - minAmbientPoint) / tRange);
                mainLight.color = nightDayColor.Evaluate(dot);
            }
            else
            {
                float tRange = 1 - minPoint;
                float dot = Mathf.Clamp01((Vector3.Dot(mainLight.transform.forward, Vector3.down) - minPoint) / tRange);
                float i = ((maxIntensity - minIntensity) * dot) + minIntensity;

                mainLight.intensity = i;

                tRange = 1 - minAmbientPoint;
                dot = Mathf.Clamp01((Vector3.Dot(mainLight.transform.forward, Vector3.down) - minAmbientPoint) / tRange);
                i = ((maxAmbient - minAmbient) * dot) + minAmbient;
                RenderSettings.ambientIntensity = i;

                mainLight.color = nightDayColor.Evaluate(dot);
                RenderSettings.ambientLight = mainLight.color;

                RenderSettings.fogColor = nightDayFogColor.Evaluate(dot);
                RenderSettings.fogDensity = fogDensityCurve.Evaluate(dot) * fogScale;
                // scion.minMaxExposure = new Vector2(minExposure.Evaluate(dot), maxExposure.Evaluate(dot));

                i = ((dayAtmosphereThickness - nightAtmosphereThickness) * dot) + nightAtmosphereThickness;
                skyMat.SetFloat("_AtmosphereThickness", i);

                if (dot > 0)
                    transform.Rotate(dayRotateSpeed * Time.deltaTime * skySpeed);
                else
                    transform.Rotate(nightRotateSpeed * Time.deltaTime * skySpeed);
            }
        }
	}
}
