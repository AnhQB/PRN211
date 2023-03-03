using CoffeeBook.DTOs;
using CoffeeBook.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        public Account GetAccountByUserName(string userName)
        {

            Account? account = context.Accounts.Find(userName);
            return account;
        }
        public bool UpdateAccount(string userName, string displayName, string pass, string newPass)
        {
            bool isUpdated = false;
            try
            {
                var account = GetAccountByUserName(userName);
                account.DisplayName = displayName;
                account.PassWord = newPass;
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
                        Type = account.Type == 0 ? "Admin" : "Staff"
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objAccountList;
        }

        public bool InsertAccount(string name, string displayName, byte type)
        {
            bool IsAdded = false;
            if (GetAccountByUserName(name) != null)
            {
                throw new ArgumentException("Username is already exist!");
            }

            try
            {
                var account = new Account();
                account.UserName = name;
                account.DisplayName = displayName;
                account.Type = type;

                context.Accounts.Add(account);
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

        public bool UpdateAccount(string name, string displayName, byte type)
        {
            /*string query = string.Format("UPDATE Account SET DisplayName = N'{1}', Type = {2} WHERE UserName = N'{0}'", name, displayName, type);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;*/

            bool isUpdated = false;
            try
            {
                var account = GetAccountByUserName(name);
                account.DisplayName = displayName;
                account.Type = type;
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

        public bool DeleteAccount(string name)
        {
            /*string query = string.Format("Delete Account where UserName = N'{0}'", name);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;*/
            bool IsDeleted = false;
            try
            {
                var accountDelete = GetAccountByUserName(name);
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

        public bool resetPassWord(string name)
        {
            /*string query = string.Format("Update Account SET PassWord=N'0' WHERE UserName=N'{0}'", name);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;*/

            bool isUpdated = false;
            try
            {
                var account = GetAccountByUserName(name);
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

    }
}
