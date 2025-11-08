using Godot;
using Waves.Code.Infrastructure;

namespace Waves.Code.SceneManagement.SceneControllers;

public partial class SceneController : Node
{
    public override void _Ready()
        => EventBus.Instance.HitPointChanged += OnHitPointChanged;

    private void OnHitPointChanged(int current, int max)
    {
        if (current <= 0)
        {
            GetTree().SetPause(true);
        }
    }
}