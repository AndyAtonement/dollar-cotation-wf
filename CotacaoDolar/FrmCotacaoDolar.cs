using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Net.Http;
using System.Windows.Forms;

namespace CotacaoDolar
{
    public partial class FrmCotacaoDolar : Form
    {
        public FrmCotacaoDolar()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string Url = "https://api.hgbrasil.com/finance?array_limit=1&fields=only_results,USD&key=c63b1fcf";

            using (HttpClient client = new HttpClient())
            {
                try { 
                    var response = client.GetAsync(Url).Result;
                    if(response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;

                        Market market = JsonConvert.DeserializeObject<Market>(result);

                        lblBuy.Text = market.Currency.Buy.ToString("C", CultureInfo.GetCultureInfo("pt-BR"));
                        lblSell.Text = String.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", market.Currency.Sell);
                        lblVariation.Text = String.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:P}", market.Currency.Variation);
                    }
                }
                catch(HttpRequestException httpRquestException)
                {
                    Console.WriteLine(httpRquestException.StackTrace);
                }
            }
        }

    }
}
