using DotNetMath3.Shared.Models;

namespace DotNetMath3.UI.State
{
    public class LoginStateContainer
    {
        private string userName;
        private string jwtToken;

        public string UserName
        {
            get => userName ?? string.Empty;
            set
            {
                userName = value;
                NotifyLoginDataChanged();
            }
        }

        public string JWTToken
        {
            get => jwtToken ?? string.Empty;
            set
            {
                jwtToken = value;
                NotifyLoginDataChanged();
            }
        }

        public void LogOut()
        {
            UserName = string.Empty;
            JWTToken = string.Empty;
        }

        public event Action LoginDataChanged;

        private void NotifyLoginDataChanged() => LoginDataChanged?.Invoke();
    }
}