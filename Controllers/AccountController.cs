using BankAppMvc.Models;
using BankAppMvc.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BankAppMvc.Controllers
{
    [Authorize(Roles = "Cashier")]
    public class AccountController : Controller
    {
        private BankAppDataContext _ctx;

        public AccountController(BankAppDataContext ctx)
        {
            _ctx = ctx;
        }


        public IActionResult Deposit(int id)
        {
            var model = new PayAndWithdrawalViewModel();
            model.Account = _ctx.Accounts.SingleOrDefault(a => a.AccountId == id);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Deposit(PayAndWithdrawalViewModel newvalues)
        {
            if (ModelState.IsValid)
            {
                newvalues.Transaction.AccountId = newvalues.Account.AccountId;
                newvalues.Transaction.Date = DateTime.Now;
                newvalues.Transaction.Date.ToShortDateString();
                newvalues.Transaction.Type = "Credit";
                newvalues.Transaction.Operation = "Credit in Cash";

                var oldvalues = _ctx.Accounts.SingleOrDefault(a => a.AccountId == newvalues.Account.AccountId);
                newvalues.Account.Frequency = oldvalues.Frequency;
                newvalues.Account.Created = oldvalues.Created;
                newvalues.Transaction.Balance = oldvalues.Balance + newvalues.Transaction.Amount;
                newvalues.Account.Balance = newvalues.Transaction.Balance;

                _ctx.Entry(oldvalues).CurrentValues.SetValues(newvalues.Account);
                _ctx.Transactions.Add(newvalues.Transaction);
                _ctx.SaveChanges();

                ModelState.Clear();
            }
            return PartialView("_DoneDepositMsg", newvalues);
        }


        public IActionResult Withdrawal(int id)
        {
            var model = new PayAndWithdrawalViewModel();
            model.Account = _ctx.Accounts.SingleOrDefault(a => a.AccountId == id);

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Withdrawal(PayAndWithdrawalViewModel newvalues)
        {
            if (ModelState.IsValid)
            {
                var oldvalues = _ctx.Accounts.SingleOrDefault(a => a.AccountId == newvalues.Account.AccountId);

                if ((newvalues.Transaction.Amount > oldvalues.Balance) || newvalues.Transaction.Amount <= 0 )
                {
                    return PartialView("_FailedWithdrawalMsg");
                }

                else
                {
                    newvalues.Transaction.AccountId = newvalues.Account.AccountId;
                    newvalues.Transaction.Date = DateTime.Now;
                    newvalues.Transaction.Date.ToShortDateString();
                    newvalues.Transaction.Type = "Debit";
                    newvalues.Transaction.Operation = "Withdrawal in Cash";
                    newvalues.Account.Frequency = oldvalues.Frequency;
                    newvalues.Account.Created = oldvalues.Created;
                    newvalues.Transaction.Balance = oldvalues.Balance + (-(newvalues.Transaction.Amount));
                    newvalues.Account.Balance = newvalues.Transaction.Balance;

                    _ctx.Entry(oldvalues).CurrentValues.SetValues(newvalues.Account);
                    _ctx.Transactions.Add(newvalues.Transaction);
                    _ctx.SaveChanges();

                    ModelState.Clear();

                    return PartialView("_DoneWithdrawalMsg", newvalues);
                }
            }
            return View();
        }


        public IActionResult Transaction(int id)
        {
            var model = new Transactions();
            model.AccountId = id;

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Transaction(Transactions newvalues)
        {
            if (ModelState.IsValid)
            {
                var transactionInfo = new TransactionViewModel();
                transactionInfo.Transaction = newvalues;
                transactionInfo.Amount = newvalues.Amount;

                var transactionFrom = new Transactions();
                transactionFrom = newvalues;
                transactionFrom.Date = DateTime.Now;
                transactionFrom.Date.ToShortDateString();
                transactionFrom.Type = "Debit";
                transactionFrom.Operation = "Remittance to Another Bank";

                var transactionTo = new Transactions();
                transactionTo.AccountId = Int32.Parse(newvalues.Account);
                transactionTo.Date = transactionFrom.Date;
                transactionTo.Type = "Credit";
                transactionTo.Operation = "Collection from Another Bank";
                transactionTo.Amount = newvalues.Amount;
                transactionTo.Symbol = newvalues.Symbol;
                transactionTo.Bank = newvalues.Bank;
                transactionTo.Account = newvalues.AccountId.ToString();

                var oldvalueTo = _ctx.Accounts.SingleOrDefault(a => a.AccountId == Int32.Parse(newvalues.Account));

                if (oldvalueTo == null) 
                {
                    return PartialView("_FailedAccountNumMsg", newvalues);
                }

                var newvalueTo = oldvalueTo;
                newvalueTo.Balance = oldvalueTo.Balance + newvalues.Amount;
                transactionTo.Balance = newvalueTo.Balance;

                var oldFrom = _ctx.Accounts.SingleOrDefault(a => a.AccountId == newvalues.AccountId);

                if ((oldFrom.Balance < newvalues.Amount) || (newvalues.Amount <= 0))
                {
                    return PartialView("_FailedTransactionMsg");
                }

                transactionFrom.Amount = (-(newvalues.Amount));

                var newFrom = oldFrom;
                newFrom.Balance = oldFrom.Balance + (newvalues.Amount);
                transactionFrom.Balance = newFrom.Balance;

                _ctx.Entry(oldFrom).CurrentValues.SetValues(newFrom);
                _ctx.Entry(oldvalueTo).CurrentValues.SetValues(newvalueTo);
                _ctx.Transactions.Add(transactionFrom);
                _ctx.Transactions.Add(transactionTo);
                _ctx.SaveChanges();

                ModelState.Clear();

                return PartialView("_DoneTransactionMsg", transactionInfo);
            }

            return View();
        }


        public IActionResult AccountDetails(int id, int page = 1)
        {
            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";

            const int pageSize = 20;
           
            var repo = new TransactionHistoryViewModel();
            

            repo.Account = _ctx.Accounts.SingleOrDefault(a => a.AccountId == id);
            var firstTransactionList = _ctx.Transactions.Where(t => t.AccountId == id);
            repo.TransactionList = firstTransactionList.ToList();
            var totalNumber = repo.TransactionList.Count();
            var transactions = GetTransactions(id, pageSize, (page - 1) * pageSize);

            repo.ListViewModel = new LongListViewModel
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalNumberOfItems = totalNumber,
                CanShowMore = page * pageSize < totalNumber,
                Transactions = transactions
            };

            if (isAjax)
            {
                return PartialView("_TransactionRows", repo);
            }

            else
            {
                return View(repo);
            };
        }

        public IList<Transactions> GetTransactions(int id, int take, int skip)
        {
            var list = _ctx.Transactions.Where(t => t.AccountId == id);

            return list
                .Skip(skip).Take(take)
                .ToList();
        }
    }
}  
