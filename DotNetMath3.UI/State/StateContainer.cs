namespace DotNetMath3.UI.State
{
    public class StateContainer
    {
        private string userName;
        private string jwtToken;

        public string UserName
        {
            get => userName ?? string.Empty;
            set
            {
                userName = value;
                NotifyStateChanged();
            }
        }

        public string JWTToken
        {
            get => jwtToken ?? string.Empty;
            set
            {
                jwtToken = value;
                NotifyStateChanged();
            }
        }

        public event Action OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}