using System;


public static class Camera {

    static Vector vector = new Vector();

public static Vector getPosition() {
    return vector;
}

public static float getXPosition() {
    return vector.getX();
}

public static float getYPosition() {
    return vector.getY();
}

public static void addToPosition(Vector addition) {
    vector.setX(vector.getX() + addition.getX());
    vector.setY(vector.getY() + addition.getY());
}

public static void addToX(float x) {
    vector.setX(vector.getX() + x);
}

public static void addToY(float y) {
    vector.setY(vector.getY() + y);
}

}
