using System;
using Microsoft.Xna.Framework.Graphics;

public class Astroid {

    static Random rand = new Random();

    private float radius, changeInRotation, rotation;
    private Vector position, velocity, accleration;

    private Texture2D sprite;

    public Astroid(Vector position, Vector velocity, float radius, float changeInRotation) {

        this.position = position;
        this.velocity = velocity;
        this.accleration = new Vector();
        this.radius = radius;
        this.sprite = makeAstroidTexture();
        this.changeInRotation = changeInRotation;
        this.rotation = (float)(rand.NextDouble() * Math.PI * 2);
    }

    public void tick() {

        rotation = (float)((rotation + changeInRotation) % (Math.PI * 2));

        position.setX(position.getX() + velocity.getX());
        position.setY(position.getY() + velocity.getY());
    }

    public void render() {



    }




    private static Texture2D makeAstroidTexture() {

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
