using BankAppMvc.Models;
using BankAppMvc.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppMvc.Controllers
{
   
    public class HomeController : Controller
    {
        private BankAppDataContext _ctx;

    public HomeController(BankAppDataContext ctx)
    {
        _ctx = ctx;
    }

        [Authorize]
        public IActionResult Index()
        {
            MyBankStaticsInfo model = new MyBankStaticsInfo();
            model.nrOfCustomers = _ctx.Customers.ToList().Count;
            model.nrOfAccounts = _ctx.Accounts.ToList().Count;
            model.totalBalance = _ctx.Accounts.Sum(b => b.Balance);

            return View(model);
        }

        
        public IActionResult NewCustomer()
        {
            var model = new CustomerMessageViewModel();
            return View(model);
        }

        [Authorize(Roles = "Cashier")]
        [HttpPost]       
        [ValidateAntiForgeryToken]
        public IActionResult NewCustomer(CustomerMessageViewModel values)
        {
            if (values.Customer.CustomerId == 0)
            {
                if (ModelState.IsValid)
                {
                    _ctx.Customers.Add(values.Customer);
                    Accounts newAccount = new Accounts();
                    newAccount.Balance = 0;
                    newAccount.Frequency = "Monthly";
                    newAccount.Created = DateTime.Now;
                    _ctx.Accounts.Add(newAccount);

                    Dispositions newDisposition = new Dispositions();
                    newDisposition.AccountId = newAccount.AccountId;
                    newDisposition.CustomerId = values.Customer.CustomerId;
                    newDisposition.Type = "OWNER";
                    _ctx.Dispositions.Add(newDisposition);

                    _ctx.SaveChanges();

                    ModelState.Clear();

                    values.Message = "A new customer has registered. a transaktion account for customer is created.";

                    return View(values);
                }
                else
                {
                    values.Message = "A new customer could not successfully registered! Chack if you have filled all fiels correctly.";
                    return View(values);
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var old = _ctx.Customers.SingleOrDefault(c => c.CustomerId == values.Customer.CustomerId);
                    _ctx.Entry(old).CurrentValues.SetValues(values.Customer);
                    _ctx.SaveChanges();
                    ModelState.Clear();

                    values.Message = "Update done!";

                    return View(values);
                }
                values.Message = "The customer information could not successfully be saved! Chack if you have filled all fiels correctly.";
                return View(values);
            }
        }

        [Authorize(Roles = "Cashier")]
        public IActionResult EditCustomer(int id)
        {
            var model = new CustomerMessageViewModel();
            model.Customer = _ctx.Customers.SingleOrDefault(c => c.CustomerId == id);
            return View("NewCustomer", model);
        }


        [Authorize(Roles = "Cashier")]
        public IActionResult SearchForm()
    {
        return View();
    }


        [Authorize(Roles = "Cashier")]
        [HttpGet]   
    public IActionResult SearchCustomerByName(SearchViewModel values)
    {

        if (values.SearchValue == null)
        {
            var model = new SearchViewModel();
            return View(model);
        }
        else
        {
            const int pageSize = 50;
            int offSet = pageSize * (values.page - 1);

            var model = new SearchViewModel();

            model.SearchValue = values.SearchValue;
            model.NameOrCity = values.NameOrCity;
            model.page = values.page;

            if (values.NameOrCity == "name")
            {
                var customerList = _ctx.Customers.Where(c => c.Givenname.Contains(values.SearchValue) || c.Surname.Contains(values.SearchValue));
                model.CustomerList = customerList.ToList();
            }

            else if (values.NameOrCity == "city")
            {
                var customerList = _ctx.Customers.Where(c => c.City.Contains(values.SearchValue));
                model.CustomerList = customerList.ToList();
            }
            else
            {
                ViewData["Message"] = "Check your search option";
                return View(model);
            }

            var total = model.CustomerList.Count();
            model.HasMorePages = offSet + pageSize < total;

            var firstCustomerList = model.CustomerList.Skip((values.page - 1) * pageSize).Take(pageSize);
            model.CustomerList = firstCustomerList.ToList();

            if (model.CustomerList.Count == 0)
            {
                ViewData["Message"] = "Not found!";
                return View(model);
            }
            else
            {
                if (total % 50 != 0)
                {
                    model.TotalPages = (total / pageSize) + 1;
                }
                else
                {
                    model.TotalPages = total / pageSize;
                }
            }
            return View(model);
        }
    }


        [Authorize(Roles = "Cashier")]
        public IActionResult CustomerPage(int CustomerId)
    {
        CustomerPageViewModel model = new CustomerPageViewModel();
        model.SearchCustomers = _ctx.Customers.Where(c => c.CustomerId.Equals(CustomerId)).ToList();
        model.SearchDispositions = _ctx.Dispositions.Where(d => d.CustomerId.Equals(CustomerId)).ToList();


        foreach (var d in model.SearchDispositions)
        {
            foreach (var ac in model.SearchDispositions)
            {
                model.SearchAccountIds.Add(ac.AccountId);
            }
        }

        foreach (var a in model.SearchAccountIds)
        {

            model.SearchAccounts = _ctx.Accounts.Where(x => x.AccountId.Equals(a)).ToList();

            foreach (var tb in model.SearchAccounts)
            {
                model.TotalBalance = model.TotalBalance + tb.Balance;
            }
        }

        return View("CustomerPage", model);
    }


        [Authorize(Roles = "Cashier")]
        [HttpGet]       
    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            SearchViewModel model = new SearchViewModel();
            return View(model);
        }

        else
        {
           
            SearchViewModel model = new SearchViewModel();
            model.Customer = _ctx.Customers.FirstOrDefault(c => c.CustomerId == id);
            return View(model);
            
        }
    }



        [Authorize(Roles = "Cashier")]
        [HttpPost]
        
        public IActionResult Edit(SearchViewModel obj)
    {
        _ctx.Customers.Update(obj.Customer);
        _ctx.SaveChangesAsync();

        return View("EditConfirmation");

    }


        [Authorize(Roles = "Cashier")]
        [HttpGet]       
    public IActionResult Remove(int id)
    {

        _ctx.Customers.Remove(_ctx.Customers.FirstOrDefault(c => c.CustomerId == id));
        _ctx.SaveChangesAsync();
        return View("DeleteConfirmation");
    }

}
}
