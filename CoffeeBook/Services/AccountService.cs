using CoffeeBook.DTOs;
using CoffeeBook.Enum;
using CoffeeBook.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace CoffeeBook.DAOs
{
    public class AccountService
    {
        CoffeeManagementsContext context;
        public AccountService()
        {
            context = new CoffeeManagementsContext();
        }

        public bool Login(string userName, string passWord)
        {

            //string query = "USP_Login @UserName , @PassWord";
            var result = context.Accounts.FirstOrDefault(u => u.UserName == userName
            && u.PassWord == passWord);
            //DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { userName, hasPass /*list*/});
            return result != null;
            // return AccountDAO.Instance.Login(userName, passWord);
        }


        public Account GetAccountById(int Id)
        {
            Account? account = context.Accounts.Find(Id);
            return account;
        }

        public Account GetAccountByUserName(string name)
        {
            Account? account = context.Accounts.FirstOrDefault(u => u.UserName.Equals(name));
            return account;
        }

        public Account GetAnotherAccountByUserName(Account accountParam)
        {
            Account? account = context.Accounts.FirstOrDefault(u => u.Id != accountParam.Id && u.UserName.Equals(accountParam.UserName));
            return account;
        }

        public bool UpdateAccount(Account account, string newPass)
        {
            if (GetAnotherAccountByUserName(account) != null)
            {
                throw new ArgumentException("Username is already exist!");
            }
            bool isUpdated = false;
            try
            {
                var account1 = GetAccountById(account.Id);
                if(account.DisplayName != "")
                {
                    account1.DisplayName = account.DisplayName;
                }

                if(newPass != null)
                {
                    account1.PassWord = newPass;
                }
                account1.Type = account.Type;
                context.Update(account1);
                var NoOfRowsAffected = context.SaveChanges();
                isUpdated = NoOfRowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //int result = DataProvider.Instance.ExecuteNonQuery("exec USP_UpdateAccount @userName , @displayName , @password , @newPassword", new object[] { userName, displayName, pass, newPass });
            return isUpdated;
        }
        public List<AccountDTO> getListAccount()
        {
            //return DataProvider.Instance.ExecuteQuery("SELECT UserName, DisplayName, Type FROM Account");

            List<AccountDTO> objAccountList = new List<AccountDTO>();
            try
            {
                var ObjQuery = from obj in context.Accounts
                               select obj;
                foreach (var account in ObjQuery)
                {
                    objAccountList.Add(new AccountDTO
                    {
                        Id = account.Id,
                        UserName = account.UserName,
                        DisplayName = account.DisplayName,
                        Type = account.Type,
                        TypeStr = account.Type.Equals((byte)AccountTypeEnum.Admin) ? "Admin" : "Staff"
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objAccountList;
        }

        public bool InsertAccount(AccountDTO account)
        {
            Account a = new Account();
            a.Id = account.Id;
            a.UserName = account.UserName;
            bool IsAdded = false;
            if (GetAnotherAccountByUserName(a) != null)
            {
                throw new ArgumentException("Username is already exist!");
            }

            try
            {
                var account1 = new Account();
                account1.UserName = account.UserName;
                account1.DisplayName = account.DisplayName;
                account1.Type = account.Type;

                context.Accounts.Add(account1);
                var NoOfRowsAffected = context.SaveChanges();
                IsAdded = NoOfRowsAffected > 0;
            }
            catch (SqlException ex)
            {

                throw ex;
            }

            return IsAdded;
            /*string query = string.Format("INSERT dbo.Account ( UserName, DisplayName, Type )VALUES  ( N'{0}', N'{1}', {2})", name, displayName, type);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;*/
        }

        public bool UpdateAccount(Account accountParam)
        {
            /*string query = string.Format("UPDATE Account SET DisplayName = N'{1}', Type = {2} WHERE UserName = N'{0}'", name, displayName, type);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;*/

            bool isUpdated = false;
            var a = GetAnotherAccountByUserName(accountParam);
            if (a != null)
            {
                throw new ArgumentException("Username is already exist!");
            }
            try
            {
                var account = GetAccountById(accountParam.Id);
                account.DisplayName = accountParam.DisplayName;
                account.UserName = accountParam.UserName;
                account.Type = accountParam.Type;
                account.PassWord = accountParam.PassWord;
                context.Update(account);
                var NoOfRowsAffected = context.SaveChanges();
                isUpdated = NoOfRowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //int result = DataProvider.Instance.ExecuteNonQuery("exec USP_UpdateAccount @userName , @displayName , @password , @newPassword", new object[] { userName, displayName, pass, newPass });
            return isUpdated;
        }

        public bool DeleteAccount(Account account)
        {
            /*string query = string.Format("Delete Account where UserName = N'{0}'", name);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;*/
            bool IsDeleted = false;
            try
            {
                var accountDelete = GetAccountById(account.Id);
                context.Accounts.Remove(accountDelete);
                var NoOfRowsAffected = context.SaveChanges();
                IsDeleted = NoOfRowsAffected > 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return IsDeleted;
        }

        public bool resetPassWord(Account accountParam)
        {
            /*string query = string.Format("Update Account SET PassWord=N'0' WHERE UserName=N'{0}'", name);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;*/

            bool isUpdated = false;
            try
            {
                var account = GetAccountById(accountParam.Id);
                account.PassWord = "0";
                var NoOfRowsAffected = context.SaveChanges();
                isUpdated = NoOfRowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //int result = DataProvider.Instance.ExecuteNonQuery("exec USP_UpdateAccount @userName , @displayName , @password , @newPassword", new object[] { userName, displayName, pass, newPass });
            return isUpdated;
        }

        public List<AccountDTO> SearchAccountByUserName(string username)
        {
            //return DataProvider.Instance.ExecuteQuery("SELECT UserName, DisplayName, Type FROM Account");

            List<AccountDTO> objAccountList = new List<AccountDTO>();
            try
            {
                var ObjQuery = context.Accounts
                    .Where(x => x.UserName.Contains(username))
                    .ToList();
                foreach (var account in ObjQuery)
                {
                    objAccountList.Add(new AccountDTO
                    {
                        Id = account.Id,
                        UserName = account.UserName,
                        DisplayName = account.DisplayName,
                        Type = account.Type,
                        TypeStr = account.Type.Equals((byte)AccountTypeEnum.Admin) ? "Admin" : "Staff"
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objAccountList;
        }

        /*public Account GetAccountById(int id)
        {
            try
            {
                var account = this.context.Accounts.Where(account => account.Id == id).FirstOrDefault();
                return account;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }*/

        /*public bool UpdateAccount(Account a)
        {
            try
            {
                var user = this.context.Accounts.Where(account => account.UserName == a.UserName).FirstOrDefault();
                if (user.Id != a.Id) return false;
                var account = this.context.Accounts.Where(account => account.Id == a.Id).FirstOrDefault();
                account.UserName = a.UserName;
                account.DisplayName = a.DisplayName;
                a.PassWord = a.PassWord;
                this.context.Accounts.Update(account);
                this.context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }*/
    }
}
