using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace KangarooPunchWithInterface
{
    class KangarooPunch
    {
        private List<currency> currencyList;
        private String status;
        private String timestamp;

        //only one constructor
        public KangarooPunch()
        {
            currencyList = new List<currency>();
            status = "Constructing KangarooPunch.";
            timestamp = ""; ;
            updateRates();
        }

        public List<currency> getCurrencyList()
        { return currencyList; }

        public void setCurrencyList(List<currency> newCurrencyList)
        { currencyList = newCurrencyList; }

        public String getStatus()
        { return status; }

        public void setStatus(String newStatus)
        { status = newStatus; }

        public String getTimestamp()
        { return timestamp; }

        public void setTimestamp(String newTime)
        { timestamp = newTime; }

        /*updateRates() populates currencyList with the most current exchange rates
        if we cannot contact our remote APIs, it pulls values in from our legacy files*/
        public void updateRates()
        {
            currencyList = new List<currency>();
            try
            {
                getRealExchangeRates();
                getCryptoExchangeRates();
                updateTimeStamp();
                saveLegacyRates();
                status = "Using exchange rates fetched at " + timestamp + ".";
            }
            catch (Exception E)
            {
                String errMessage = E.Message;
                fetchLegacyRates();
                status = "API failure - using legacy rates from " + timestamp + ".";
            }

        }

        public void getRealExchangeRates()
        {
            currency C = null;
            double exchangeRate = 0;
            String ISO = "";
            String fullName = "";
            String rate = "";
            String url = "https://openexchangerates.org/api/latest.json?app_id=4d9a6a283a104a6fa454298522d751d7";


            //call OpenExchange
            WebClient wb = new WebClient();
            var response = wb.DownloadString(url);
            dynamic obj = JsonConvert.DeserializeObject(response);

            foreach (var temp in obj.rates)
            {
                ISO = temp.Name;
                rate = temp.Value;

                //attempt to parse exchange rate
                try
                {
                    exchangeRate = double.Parse(rate);
                }
                catch (Exception e) { exchangeRate = 1; }

                //check if the ISO is one that we care about
                fullName = lookupName(ISO);

                if (!fullName.Equals("ERROR"))
                {
                    //add a new currency to currencyList
                    C = new currency(ISO,fullName,exchangeRate);
                    insertAlphabetical(C);
                }
            }
            return;
        }

        public void getCryptoExchangeRates()
        {
            currency C = null;
            double exchangeRate = 0;
            String ISO = "";
            String fullName = "";
            String rate = "";
            String url = "https://min-api.cryptocompare.com/data/price?fsym=USD&tsyms=BTC,ETH,XRP,EOS,LTC,XLM,ADA,MIOTA,USDT,TRX,NEO";

            //call CryptoCompare
            WebClient wb = new WebClient();
            var response = wb.DownloadString(url);
            dynamic obj = JsonConvert.DeserializeObject(response);

            //parse json
            foreach(var temp in obj)
            {
                ISO = temp.Name;
                rate = temp.Value;

                //attempt to parse exchange rate
                try
                {
                    exchangeRate = double.Parse(rate);
                }
                catch (Exception e) { exchangeRate = 1; }

                //check if the ISO is one that we care about
                fullName = lookupName(ISO);

                if (!fullName.Equals("ERROR"))
                {
                    //add a new currency to currencyList
                    C = new currency(ISO, fullName, exchangeRate);
                    insertAlphabetical(C);
                }
            }

            return;
        }

        public void updateTimeStamp()
        {
            timestamp = DateTime.Now.ToString("yyyy-MM-dd hh:mm tt");
        }

        public void saveLegacyRates()
        {
            int i = 0;
            string filename = "legacy.txt";
            String line = "";
            currency C;

            System.IO.StreamWriter writer = new System.IO.StreamWriter(filename);

            //write timestamp to file first
            writer.WriteLine(timestamp);

            for (i = 0; i < currencyList.Count; i++)
            {
                C = currencyList[i];
                line = C.getISO() + "~" + C.getName() + "~" + C.getRate();
                writer.WriteLine(line);
            }

            writer.Close();

            return;
        }

        public void fetchLegacyRates()
        {
            int lineCount = 0;
            double value = 0;
            string legacy = "legacy.txt";
            string line = "";
            System.IO.StreamReader file = null;
            currency C;

            try//get legacy rates
            { file = new System.IO.StreamReader(legacy);}
            catch (Exception E)//exception - file does not exist
            { status = "Error! Could not read legacy exchange rates! Verify internet connection and retry."; return; }

            while ((line = file.ReadLine()) != null)
            {
                //skip the first line
                if (lineCount == 0)
                {
                    timestamp = line;
                    continue;
                }

                //convert each line of our text file to a currency class
                try
                {
                    string[] currency = line.Split('~');
                    value = double.Parse(currency[2]);
                    C = new KangarooPunchWithInterface.currency(currency[0], currency[1], value);
                    currencyList.Add(C);
                }
                catch (Exception EE) { C = null; }

                lineCount++;
            }

            file.Close();
            return;
        }

        public double convert(String from, String to, double amount)
        {
            //grab new exchange rates every minute
            string currentTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm tt");

            //refresh rates if 60 seconds has passed
            if (!timestamp.Equals(currentTime))
            { updateRates(); }

            //begin conversion
            currency fromCurrency = lookupISO(from);
            currency toCurrency = lookupISO(to);
            double result = 0;

            result = amount / fromCurrency.getRate();
            result = result * toCurrency.getRate();

            return result;
        }

        public void insertAlphabetical(currency C)
        {
            int i = 0;
            int j = 0;
            int length = currencyList.Count;
            String insertName = C.getISO().ToUpper();
            String compareName = "";

            //empty list, insert at front
            if (length == 0)
            {
                currencyList.Add(C);
                return;
            }

            //iterate through every element of our list
            for (i = 0; i < length; i++)
            {
                compareName = currencyList[i].getISO().ToUpper();

                //ignore duplicates
                if (insertName.Equals(compareName))
                { return; }

                //insert comes before compare alphabetically, insert here
                if (insertName[0] < compareName[0])
                {
                    currencyList.Insert(i, C);
                    return;
                }
                //each one begins with the same letter
                else if (insertName[0] == compareName[0])
                {
                    //compare each character in Insertname and Comparename until we find a mismatching character
                    for (j = 0; j < insertName.Length && j < compareName.Length; j++)
                    {
                        //insertName comes before compareName alphabetically at index j
                        if (insertName[j] < compareName[j])
                        {
                            currencyList.Insert(i, C);
                            return;
                        }
                    }
                }
            }

            //exhausted list, insert at end
            currencyList.Insert(i, C);
            return;
        }

        public currency lookupISO(String ISO)
        {
            int i = 0;
            currency C = new currency("not found", "not found", 1);

            for (i = 0; i < currencyList.Count; i++)
            {
                if (currencyList[i].getISO().Equals(ISO))
                { return currencyList[i]; }
            }

            return C;
        }

        public String lookupName(String ISO)
        {
            String name = "ERROR";

            if (ISO.Equals("USD"))
            { name = "US Dollar"; }
            else if (ISO.Equals("EUR"))
            {name = "Euro"; }
            else if (ISO.Equals("GBP"))
            { name = "British Pound"; }
            else if (ISO.Equals("INR"))
            { name = "Indian Rupee"; }
            else if (ISO.Equals("AUD"))
            { name = "Australian Dollar"; }
            else if (ISO.Equals("CAD"))
            { name = "Canadian Dollar"; }
            else if (ISO.Equals("SGD"))
            { name = "Singapore Dollar"; }
            else if (ISO.Equals("CHF"))
            { name = "Swiss Franc"; }
            else if (ISO.Equals("MYR"))
            { name = "Malaysian Ringgit"; }
            else if (ISO.Equals("JPY"))
            { name = "Japanese Yen"; }
            else if (ISO.Equals("CNY"))
            { name = "Chinese Yuan Renminbi"; }
            else if (ISO.Equals("BTC"))
            { name = "Bitcoin"; }
            else if (ISO.Equals("ETH"))
            { name = "Ethereum"; }
            else if (ISO.Equals("XRP"))
            { name = "XRP"; }
            else if (ISO.Equals("EOS"))
            { name = "EOS"; }
            else if (ISO.Equals("LTC"))
            { name = "Litecoin"; }
            else if (ISO.Equals("XLM"))
            { name = "Stellar"; }
            else if (ISO.Equals("ADA"))
            { name = "Cardano"; }
            else if (ISO.Equals("MIOTA"))
            { name = "IOTA"; }
            else if (ISO.Equals("USDT"))
            { name = "Tether"; }
            else if (ISO.Equals("TRX"))
            { name = "TRON"; }
            else if (ISO.Equals("NEO"))
            { name = "NEO"; }

            return name;
        }
    }

    class currency
    {
        private String ISO; //3 character abbreviation for currency
        private String name; //self explanatory
        private double rate; //the currency's worth in USD

        public currency()
        {
            ISO = String.Empty;
            name = String.Empty;
            rate = 0;
        }  

        public currency(String iso, String nm, double rt)
        {
            ISO = iso;
            name = nm;
            rate = rt;
        }

        public String getISO()
        { return ISO; }

        public void setISO(String newISO)
        { ISO = newISO; }

        public String getName()
        { return name; }

        public void setName(String newName)
        { name = newName; }

        public double getRate()
        { return rate; }

        public void setRate(double newRate)
        { rate = newRate; }
    }
}
