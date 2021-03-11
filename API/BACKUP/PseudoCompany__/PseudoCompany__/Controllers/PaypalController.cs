using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PayPal.Api;
using System;
using System.Collections.Generic;

namespace ParqueAPICentral.Controllers
{
    [Route("api/Paypal")]
    public class PaypalController : Controller
    {
        private readonly Dictionary<string, string> _payPalConfig;
        private Payment payment;

        public PaypalController(IConfiguration config)
        {
            _payPalConfig = new Dictionary<string, string>()
            {
                { "clientId" , config.GetSection("paypal:settings:clientId").Value },
                { "clientSecret", config.GetSection("paypal:settings:clientSecret").Value },
                { "mode", config.GetSection("paypal:settings:mode").Value },
                { "business", config.GetSection("paypal:settings:business").Value },
                { "merchantId", config.GetSection("paypal:settings:merchantId").Value },
            };

            //var accessToken = new OAuthTokenCredential(_payPalConfig).GetAccessToken();
        }

        // POST: api/Paypal : Pagamento
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [EnableCors]
        [HttpPost]
        public ActionResult PostPagamento()
        {
            var accessToken = new OAuthTokenCredential(_payPalConfig).GetAccessToken();

            APIContext aPIContext = new APIContext(accessToken);

            Payment payment = new Payment
            {
                id = "1",
            };
            var pay2 = CreatePayment(aPIContext);

            return Ok(pay2.links[1]);
        }

        [HttpGet("Pagamento")]
        public ActionResult ExecutarPagamento([FromQuery] string PaymentID, [FromQuery] string PayerID)
        {
            var accessToken = new OAuthTokenCredential(_payPalConfig).GetAccessToken();

            APIContext ApiContext = new APIContext(accessToken);

            var pe = new PaymentExecution()
            {
                payer_id = PayerID
            };

            var payment = new Payment()
            {
                id = PaymentID
            };

            payment.Execute(ApiContext, pe);

            return Ok("Pagamento Efetuado com sucesso");
        }

        private Payment CreatePayment(APIContext apiContext)
        {
            //create itemlist and add item objects to it
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };
            //Adding Item Details like name, currency, price etc
            itemList.items.Add(new Item()
            {
                name = "Item_1",
                currency = "USD",
                price = "2",
                quantity = "2",
                sku = "sku"
            });
            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Configure Redirect Urls here with RedirectUrls object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = "www.google.pt" + "&Cancel=true",
                return_url = "https://localhost:44330/api/paypal/pagamento"
            };
            // Adding Tax, shipping and Subtotal details
            var details = new Details()
            {
                tax = "1",
                shipping = "1",
                subtotal = "4"
            };
            //Final amount with details
            var amount = new Amount()
            {
                currency = "USD",
                total = "6", // Total must be equal to sum of tax, shipping and subtotal.
                details = details
            };
            var transactionList = new List<Transaction>();
            // Adding description about the transaction
            transactionList.Add(new Transaction()
            {
                description = "Transaction description",
                invoice_number = Guid.NewGuid().ToString(), //Generate an Invoice No
                amount = amount,
                item_list = itemList
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext
            return this.payment.Create(apiContext);
        }

    }
}