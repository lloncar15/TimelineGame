public abstract class BehaviourCommand : IBehaviourCommand {
    public float ExecutionTime { get; set; }
    public bool IsActive { get; set; }
    public float StartTime { get; set; }
    public int Priority { get; set; }
    
    public virtual void ExecuteInUpdate() {
        if(!IsActive) return;
    }

    public virtual void ExecuteInFixedUpdate() {
        if(!IsActive) return;
    }
}

public class JumpCommand : BehaviourCommand {
    private PlayerController _playerController;
    private JumpCommandData _jumpCommandData;

    public JumpCommand(PlayerController playerController, JumpCommandData jumpCommandData) {
        _playerController = playerController;
        _jumpCommandData = jumpCommandData;
    }
    
    public override void ExecuteInUpdate() {
        base.ExecuteInUpdate();
        
        
    }
}

public struct JumpCommandData {
    public float JumpSpeed;
    public float JumpDuration;
    public bool IsDoubleJump;
}