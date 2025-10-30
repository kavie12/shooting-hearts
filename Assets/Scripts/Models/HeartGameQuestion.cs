using UnityEngine;

public class HeartGameQuestion
{
    public Texture2D texture;
    public int heartsCount;
    public int carrotsCount;

    public HeartGameQuestion(Texture2D texture, int hearts, int carrots)
    {
        this.texture = texture;
        this.heartsCount = hearts;
        this.carrotsCount = carrots;
    }
}