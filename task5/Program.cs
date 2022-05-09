using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeAPIAuthSem4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
    /*
         {
        "UserName": "JMBarry",
        "Password": "Neverland"
    }
        {
        "UserName": "Lotty",
        "Password": "CurrelBell"
    }
        {
        "UserName": "Charly",
        "Password": "Annie7Reese"
    }

    [
    {
        "Name": "Julia",
        "Id": "3456789089",
        "CardNumber": "2323656508086969",
        "Cvc": "466",
        "Month": 3,
        "Year": 2024,
        "Date": "2022-12-23",
        "Amount": 3600
    },
    {
        "Name": "Stephan",
        "Id": "0004567303",
        "CardNumber": "4545767698980909",
        "Cvc": "3647",
        "Month": 3,
        "Year": 2022,
        "Date": "2007-01-06",
        "Amount": 2345
    },
    {
        "Name": "Lily",
        "Id": "7676745659",
        "CardNumber": "2345567890345345",
        "Cvc": "4651",
        "Month": 5,
        "Year": 2027,
        "Date": "2023-10-07",
        "Amount": 532
    },
    {
        "Name": "Roger",
        "Id": "5676588878",
        "CardNumber": "1234567890345676",
        "Cvc": "450",
        "Month": 5,
        "Year": 2021,
        "Date": "2013-02-08",
        "Amount": 1404
    },
    {
        "Name": "Devid",
        "Id": "9999123456",
        "CardNumber": "9292838347475859",
        "Cvc": "292",
        "Month": 8,
        "Year": 2021,
        "Date": "2015-12-07",
        "Amount": 7856
    },
    {
        "Name": "Leslie",
        "Id": "7676009899",
        "CardNumber": "2340987890345345",
        "Cvc": "4602",
        "Month": 5,
        "Year": 2026,
        "Date": "2021-11-09",
        "Amount": 5300
    },
    {
        "Name": "Felix",
        "Id": "3456784454",
        "CardNumber": "5678567845639603",
        "Cvc": "9981",
        "Month": 11,
        "Year": 2019,
        "Date": "2019-10-06",
        "Amount": 4580
    },
    {
        "Name": "Dorin",
        "Id": "3456745673",
        "CardNumber": "5678567845639603",
        "Cvc": "3681",
        "Month": 3,
        "Year": 2016,
        "Date": "2012-10-06",
        "Amount": 450000
    }
]
     */
}
