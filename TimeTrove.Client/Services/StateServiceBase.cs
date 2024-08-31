namespace TimeTrove.Client.Services
{
    public abstract class StateServiceBase
    {
        public event Action? OnStateChanged;

        protected void NotifyStateChanged() => OnStateChanged?.Invoke();
    }
}
