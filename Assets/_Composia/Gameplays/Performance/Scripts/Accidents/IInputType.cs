namespace Composia
{
    public interface IInputType : IRestartable
    {
        public abstract bool GetValidation();

        public abstract float GetValue();

        public abstract void ResolutionInputGamepad();

        public abstract void ResolutionInputKeyboard();

        public abstract void DetectInput();

        //public abstract void Stop();
    }
}