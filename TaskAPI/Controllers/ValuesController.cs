using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading;
using System.Web.Http;
using TaskAPI.Models;

namespace TaskAPI.Controllers
{
    public class ValuesController : ApiController
    {
        BankEntities entities = new BankEntities();
        public ValuesController()
        {
            Thread.Sleep(2000);
        }

        [HttpGet]
        public IEnumerable<Customer> GetAllCustomers()
        {
            return entities.Customers;
        }

        //[HttpGet]
        //public HttpResponseMessage GetAllCustomers()
        //{
        //    return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad reqeust!");
        //}

        [HttpGet]
        [Route("api/values/GetAllCustomerAccounts/{id}")]
        public object GetAllCustomerAccounts(int id)
        {
            int EGPCurrencyId = entities.Currencies.FirstOrDefault(cr => cr.CurrencyName == "EGP").CurrencyId;
            double CalculatedBalance = 0.00;
            foreach (CustomerAccount account in entities.CustomerAccounts.Where(acc => acc.CustomerId == id && acc.Status))
            {
                //calc formatted balance in EGP
                if (account.CurrencyId == EGPCurrencyId)
                {
                    CalculatedBalance += account.Openning_Balance;
                }
                else
                {
                    ExchangeRate rate = entities.ExchangeRates.FirstOrDefault(ex => ex.FromCurrencyId == account.CurrencyId && ex.ToCurrencyId == EGPCurrencyId);
                    if (rate.Operator == "m")
                    {
                        CalculatedBalance += account.Openning_Balance * rate.Amount;
                    }
                    else // == "d"
                    {
                        CalculatedBalance += account.Openning_Balance / rate.Amount;
                    }
                }
            }
            var AccountsList = (from accounts in entities.CustomerAccounts
                                where accounts.CustomerId == id
                                select new
                                {
                                    accounts.Acc_ID,
                                    accounts.Acc_Number,
                                    accounts.Acc_Type,
                                    accounts.Class_Code,
                                    accounts.currency.CurrencyName,
                                    accounts.Openning_Balance,
                                    accounts.Status
                                }).ToList();
            Customer c = entities.Customers.FirstOrDefault(cs => cs.CustomerId == id);
            object AccountInfo = new
            {
                CustomerId = c.CustomerId,
                CustomerName = c.CustomerName,
                OpenDate = c.OpenDate,
                CalculatedBalance = CalculatedBalance
            };
            return new { AccountInfo, AccountsList };
        }

        [HttpPost]
        [Route("api/values/AddAccount")]
        public object AddAccount([FromBody]CustomerAccount account)
        {
            account = entities.CustomerAccounts.Add(account);
            entities.SaveChanges();
            int EGPCurrencyId = entities.Currencies.FirstOrDefault(cr => cr.CurrencyName == "EGP").CurrencyId;
            double addedBalance = 0.00;
            if (account.CurrencyId == EGPCurrencyId)
            {
                addedBalance = account.Openning_Balance;
            }
            else
            {
                ExchangeRate rate = entities.ExchangeRates.FirstOrDefault(ex => ex.FromCurrencyId == account.CurrencyId && ex.ToCurrencyId == EGPCurrencyId);
                if (rate.Operator == "m")
                {
                    addedBalance = account.Openning_Balance * rate.Amount;
                }
                else // == "d"
                {
                    addedBalance = account.Openning_Balance / rate.Amount;
                }
            }
            var newAccount = from accounts in entities.CustomerAccounts
                             where accounts.Acc_ID == account.Acc_ID
                             select new
                             {
                                 accounts.Acc_ID,
                                 accounts.Acc_Number,
                                 accounts.Acc_Type,
                                 accounts.Class_Code,
                                 accounts.currency.CurrencyName,
                                 accounts.Openning_Balance,
                                 accounts.Status
                             };
            return new { addedBalance = addedBalance, newAccount = newAccount };
        }

        [HttpGet]
        [Route("api/values/GetAllCurrencies")]
        public IEnumerable<Currency> GetAllCurrencies()
        {
            return entities.Currencies;
        }

        [HttpGet]
        [Route("api/values/GetClassCodes/{Acc_Type}")]
        public IEnumerable<Class_Code> GetAllClassCodes(string Acc_Type)
        {
            return entities.Classes_Codes.Where(cc => cc.Acc_Type == Acc_Type);
        }

        [HttpPost]
        [Route("api/values/ToggleAccountStatus")]
        public object ToggleAccountStatus([FromBody]int accountId)
        {
            CustomerAccount acc = entities.CustomerAccounts.FirstOrDefault(ac => ac.Acc_ID == accountId);
            bool newState = acc.Status = !acc.Status;
            entities.SaveChanges();
            int EGPCurrencyId = entities.Currencies.FirstOrDefault(cr => cr.CurrencyName == "EGP").CurrencyId;
            double addedBalance = 0.00;
            if (acc.CurrencyId == EGPCurrencyId)
            {
                addedBalance = acc.Openning_Balance;
            }
            else
            {
                ExchangeRate rate = entities.ExchangeRates.FirstOrDefault(ex => ex.FromCurrencyId == acc.CurrencyId && ex.ToCurrencyId == EGPCurrencyId);
                if (rate.Operator == "m")
                {
                    addedBalance = acc.Openning_Balance * rate.Amount;
                }
                else // == "d"
                {
                    addedBalance = acc.Openning_Balance / rate.Amount;
                }
            }
            return new { addedBalance = acc.Status ? addedBalance : -addedBalance, newState, acc.Acc_ID };
        }
    }
}