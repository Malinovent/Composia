public interface IActivatable
{
    bool IsActivated { get; set; }
    bool StaysActive { get; set; }

    public bool TryActivate();

    public void Activate();

    public void DeActivate();

}
