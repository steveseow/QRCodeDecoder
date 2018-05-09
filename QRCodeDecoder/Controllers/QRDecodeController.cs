using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ZXing;


namespace QRCodeDecoder.Controllers
{
    public class QRDecodeController : ApiController
    {
        [Route("DecodeQRCode")]
        [HttpPost]
        public string DecodeQRCode(string imageUri)
        {
            return DecodeQRCode(imageUri);
        }

        private string ProcessDecodeQRCode(string imageUri)
        {
            var barcodeReader = new BarcodeReader();
            var request = WebRequest.Create(imageUri);
            try
            {
                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                {
                    var barcodeBitmap = (Bitmap)Bitmap.FromStream(stream);
                    var barcodeResult = barcodeReader.Decode(barcodeBitmap);
                    return barcodeResult?.Text;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
