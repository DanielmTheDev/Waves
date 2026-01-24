using Godot;

namespace Waves.Code.World.Combat.CoverPoints;

[Tool]
public partial class CoverPoint : Node2D
{
    private float _shootDistance = 100f;

    [Export]
    public float ShootDistance
    {
        get => _shootDistance;
        set
        {
            _shootDistance = value;
            UpdatePosition();
            QueueRedraw();
        }
    }

    public bool IsOccupied { get; private set; }

    public Node2D ShootingPoint => GetNode<Node2D>(UniqueNames.ShootingPoint);
    public Node2D HidingPoint => GetNode<Node2D>(UniqueNames.HidingPoint);

    public void Occupy()
        => IsOccupied = true;

    public void UnOccupy()
        => IsOccupied = false;

    private void UpdatePosition()
        => ShootingPoint.Position = new Vector2(0, _shootDistance);
}