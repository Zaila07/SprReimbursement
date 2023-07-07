using Azure;
using Azure.AI.FormRecognizer;
using Microsoft.AspNetCore.Http;


namespace SprEmployeeReimbursement.Business.FormRecognizer
{
    public class FormRecognizerHelper
    {
        public static async Task<string> ExtractReceiptTextAsync(IFormFile receiptImage, string apiKey, string endpoint)
        {
            var formRecognizerOptions = new AzureKeyCredential(apiKey);
            var formRecognizerClient = new FormRecognizerClient(new Uri(endpoint), formRecognizerOptions);
          

           
           
                using var stream = receiptImage.OpenReadStream();
                var form = await formRecognizerClient.StartRecognizeContentAsync(stream);
                var operationResult = await form.WaitForCompletionAsync();
                var formPage = operationResult.Value.FirstOrDefault();

                if (formPage != null)
                {
                    var receiptText = string.Join(" ", formPage.Lines
                        .Select(line => line.Text));
                  return receiptText;
                }


            return string.Empty;
        }
    }
}
