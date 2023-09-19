using Godot;
using System;

public partial class ItemWithShadow : Node2D
{
    [Export]
    public Sprite2D itemSprite;

    [Export]
    public Sprite2D shadowSprite;

    public float height;
    public float rotation;

    public override void _Process(double delta)
    {
        itemSprite.Position = new Vector2(0.0f, -height);
        height = Mathf.Max(height, 0.0f);
        shadowSprite.Scale = 1.0f / (1.0f + height / 500.0f) * Vector2.One;
        itemSprite.Scale = (1.0f + height / 1000.0f) * Vector2.One;

        shadowSprite.Rotation = rotation;
        itemSprite.Rotation = rotation;
    }

    public void SetSprite(Texture2D texture)
    {
        itemSprite.Texture = texture;
        shadowSprite.Texture = texture;
    }
}
