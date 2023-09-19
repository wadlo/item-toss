using Godot;
using System;

public partial class DemoItem : Area2D
{
    [Export]
    public Node2D target;

    [Export]
    public PackedScene itemWithShadowScene;

    [Export]
    public Node2D hideWhileThrowing;

    public override void _Ready()
    {
        this.InputEvent += OnClick;
    }

    public void OnClick(Node viewport, InputEvent @event, long shapeIdx)
    {
        if (@event is InputEventMouseButton eventMouseButton)
        {
            if (eventMouseButton.ButtonIndex == MouseButton.Left && eventMouseButton.Pressed)
            {
                if (hideWhileThrowing.Visible)
                {
                    Toss();
                    hideWhileThrowing.Visible = false;
                }
            }
        }
    }

    private void Toss()
    {
        ItemWithShadow item = itemWithShadowScene.Instantiate<ItemWithShadow>();
        item.SetSprite(GetChild(0).GetChild<Sprite2D>(0).Texture);
        TossItem.Toss(
            item,
            this,
            this,
            target,
            (Node2D target) => {
                //hideWhileThrowing.Visible = true;
            }
        );
    }
}
