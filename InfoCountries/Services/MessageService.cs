namespace InfoCountries.Services
{
    using System.Windows;

    /// <summary>
    /// Class to manage message to the user
    /// </summary>
    public static class MessageService
    {
        public static void ShowMessage(string title, string message)
        {
            MessageBox.Show(title, message);
        }

    }
}
