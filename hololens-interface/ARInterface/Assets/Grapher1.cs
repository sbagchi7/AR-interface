﻿using UnityEngine;

public class Grapher1 : MonoBehaviour
{
    [Range(10, 100)]
    public int resolution = 10;

    private int currentResolution;
    private ParticleSystem.Particle[] points;


    public enum FunctionOption
    {
        Linear,
        Exponential,
        Parabola,
        Sine
    };
    public FunctionOption function;

    private delegate float FunctionDelegate(float x);
    private static FunctionDelegate[] functionDelegates = {
        Linear,
        Exponential,
        Parabola,
        Sine
    };


    void Update()
    {
        if (currentResolution != resolution || points == null)
        {
            CreatePoints();
        }

        FunctionDelegate f = functionDelegates[(int)function];

        for (int i = 0; i < resolution; i++)
        {
            Vector3 p = points[i].position;
            p.y = f(p.x);
            points[i].position = p;
            Color c = points[i].color;
            c.g = p.y;
            points[i].color = c;
        }

        GetComponent<ParticleSystem>().SetParticles(points, points.Length);
    }

    private void CreatePoints()
    {
        currentResolution = resolution;
        points = new ParticleSystem.Particle[resolution];
        float increment = 1f / (resolution - 1);
        for (int i = 0; i < resolution; i++)
        {
            float x = i * increment;
            points[i].position = new Vector3(x, 0f, 0f);
            points[i].color = new Color(x, 0f, 0f);
            points[i].size = 0.1f;
        }
    }

    private static float Linear(float x)
    {
        return x;
    }
    private static float Exponential(float x)
    {
        return x * x;
    }
    private static float Parabola(float x)
    {
        x = 2f * x - 1f;
        return x * x;
    }
    private static float Sine(float x)
    {
        return 0.5f + 0.5f * Mathf.Sin(2 * Mathf.PI * x + Time.timeSinceLevelLoad);
    }
}