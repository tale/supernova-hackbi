using System;
using Microsoft.Xna.Framework.Graphics;


public class Planet {

    static Random rand = new Random();

    private const double gravityStrength = 0.000000000000001;
    private float radius, mass, changeInRotation, rotation;
    private Vector position;

    private Texture sprite;

    public Planet(Vector position, float radius, float mass, float changeInRotation) {

        this.position = position;
        this.radius = radius;
        this.mass = mass;
        this.sprite = makePlanetTexture();
        this.changeInRotation = changeInRotation;
        this.rotation = (float)(rand.NextDouble() * Math.PI * 2);
    }

    public void tick() {

        rotation = (float)((rotation + changeInRotation) % (Math.PI * 2));

    }

    public void render() {



    }

    public Vector gravity(Vector ObjectPosition) {

        float acceleration = (float)(gravityStrength * mass / Hypot(position.getX() - ObjectPosition.getX(), position.getY() - ObjectPosition.getY()));

        float angle = (float)Math.Atan((position.getY() - ObjectPosition.getY()) / (position.getX() - ObjectPosition.getX()));

        return new Vector((float)(acceleration * Math.Cos(angle)), (float)(acceleration * Math.Sin(angle)));

        

    }

    private static float Hypot(float a, float b) {

        return (float)Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));


    }

   



    private static Texture makePlanetTexture() {

        int type = (int)(rand.Next(1));

        switch (type) {

            case 0:
                return null;

            case 1:
                return null;

            default:
                return null;

        }



    }



}
