namespace Warehouse.UI.Models
{
    public class ErrorViewModel
    {
        public ErrorViewModel()
        {   
        }

        public ErrorViewModel(string errorMessage)
        {
            this.ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; set; }
    }
}