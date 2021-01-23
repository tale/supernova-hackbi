using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace SuperNova.Code.Object {

    public class Planet {

    static Random rand = new Random();

    private const double gravityStrength = 0.000000000000001;
    private float radius, mass, changeInRotation, rotation;
    private Vector2 position;

    private Texture sprite;

    public Planet(Vector2 position, float radius, float mass, float changeInRotation) {

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

    public Vector2 gravity(Vector2 ObjectPosition) {

        float acceleration = (float)(gravityStrength * mass / Hypot(position.X - ObjectPosition.X, position.Y - ObjectPosition.Y));

        float angle = (float)Math.Atan((position.Y - ObjectPosition.Y) / (position.X - ObjectPosition.X));

        return new Vector2((float)(acceleration * Math.Cos(angle)), (float)(acceleration * Math.Sin(angle)));

        

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
