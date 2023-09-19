using Godot;
using System;

public partial class TossItem : Node2D
{
    Vector2 startPosition;
    Vector2 endPosition;
    Action<Node2D> onFinish;
    Node2D target;

    float speed = 1.5f;
    float height = 200.0f;

    float rotateSpeed = 10.0f;

    [Export]
    public ItemWithShadow item;

    float percentComplete = 0.0f;
    float visualPercentComplete = 0.0f;

    public TossItem(Node2D from, Node2D target, Action<Node2D> onFinish)
    {
        startPosition = from.Position;
        endPosition = target.Position;
        this.onFinish = onFinish;
        this.target = target;
    }

    public static void Toss(
        ItemWithShadow item,
        Node2D parent,
        Node2D from,
        Node2D target,
        Action<Node2D> onFinish
    )
    {
        TossItem tossItem = new TossItem(from, target, onFinish);
        tossItem.GlobalPosition = from.GlobalPosition;
        item.Position = Vector2.Zero;
        item.GetParent()?.RemoveChild(item);
        tossItem.item = item;
        tossItem.AddChild(item);
        parent.AddChild(tossItem);
    }

    public override void _PhysicsProcess(double delta)
    {
        // Track progress inside physics so completion time isn't framerate dependent.
        percentComplete = Mathf.Min(1.0f, percentComplete + speed * (float)delta);
        if (percentComplete == 1.0f)
        {
            if (onFinish != null)
            {
                onFinish.Invoke(target);
            }
            QueueFree();
        }
    }

    public override void _Process(double delta)
    {
        // Position
        visualPercentComplete = Mathf.Min(1.0f, percentComplete + speed * (float)delta);
        GlobalPosition = startPosition + (endPosition - startPosition) * visualPercentComplete;

        // Make currHeight follow an arc.
        float currHeight = height * (1.0f - Mathf.Pow(2.0f * visualPercentComplete - 1.0f, 2.0f));
        item.height = currHeight;

        // Rotation
        item.rotation += rotateSpeed * (float)delta;
    }
}
