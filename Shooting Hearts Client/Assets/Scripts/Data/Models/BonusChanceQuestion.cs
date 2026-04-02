using UnityEngine;

// Data model representing a bonus chance question with an image, hearts count, and carrots count.
public class BonusChanceQuestion
{
    public Texture2D ImageTexture { get; }
    public int HeartsCount { get; }
    public int CarrotsCount { get; }

    public BonusChanceQuestion(Texture2D texture, int hearts, int carrots)
    {
        ImageTexture = texture;
        HeartsCount = hearts;
        CarrotsCount = carrots;
    }
}