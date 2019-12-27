using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Vtc_Freelancer.Models;

namespace Vtc_Freelancer.Services
{
    public class UserService
    {
        private MyDbContext dbContext;
        private HashPassword hashPassword;
        public UserService(MyDbContext dbContext, HashPassword hashPassword)
        {
            this.dbContext = dbContext;
            this.hashPassword = hashPassword;
        }
        public bool Register(string username, string email, string password)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.UserName == username);
            if (user != null)
            {
                return false;
            }
            else
            {
                try
                {
                    Users User = new Users();
                    User.UserName = username;
                    User.Email = email;
                    MD5 mD5 = MD5.Create();
                    User.Password = GetMd5Hash(mD5, password);
                    User.RegisterDate = DateTime.Now;
                    dbContext.Add(User);
                    dbContext.SaveChanges();
                    return true;
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }
        // public Users GetUsers()
        public Users Login(string email, string password)
        {
            var character = "@";
            var user = new Users();
            MD5 md5Hash = MD5.Create();
            if (email.Contains(character))
            {
                user = dbContext.Users.FirstOrDefault(u => u.Email == email);
                if (user != null)
                {
                    if (VerifyMd5Hash(md5Hash, password, user.Password))
                    {
                        // System.Console.WriteLine(user.UserName);
                        return user;
                    }
                    else
                    {
                        return null;
                    }

                }
                return null;
            }
            else
            {
                user = dbContext.Users.FirstOrDefault(u => u.UserName == email);
                if (user != null)
                {
                    if (user.Password == password)
                    {
                        return user;
                    }
                }
                return null;
            }

        }
        public bool EditProfile(int Id, string Email, string UserName)
        {
            Users user = new Users();
            user = GetUserByUserId(Id);
            if (user != null)
            {
                user.UserName = UserName;
                user.Email = Email;
                dbContext.Update(user);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public Seller BecomeSeller(Users users, Languages languages, Seller seller1, Category category, Skills skills)
        {
            try
            {
                Seller seller = new Seller();
                seller.UserId = users.UserId;
                seller.SellerPoint = 0;
                seller.Description = seller1.Description;
                seller.RegisterDateSeller = DateTime.Now;
                dbContext.Add(seller);
                dbContext.SaveChanges();
                bool userlevel = UpdateIsSeller(users);
                bool lang = AddLanguage(seller, languages, users);
                bool skill = AddSkills(seller, skills, users);
                bool addcateseller = AddSellerCategory(seller, category);
                if (userlevel && lang && skill && addcateseller)
                {
                    return seller;
                }
                return null;
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

        }
        public bool AddLanguage(Seller seller, Languages languages1, Users users)
        {
            try
            {
                Languages languages = new Languages();
                languages.SellerId = seller.SellerId;
                languages.Language = languages1.Language;
                languages.Level = languages1.Level;
                languages.UserId = users.UserId;
                dbContext.Add(languages);
                dbContext.SaveChanges();
                return true;

            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
                throw;
            }


        }
        public bool AddSellerCategory(Seller seller, Category category)
        {
            try
            {
                SellerCategory sellerCategory = new SellerCategory();
                sellerCategory.SellerId = seller.SellerId;
                sellerCategory.CategoryId = category.CategoryId;

                dbContext.Add(sellerCategory);
                dbContext.SaveChanges();
                return true;
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
                throw;
            }
        }
        public bool UpdateIsSeller(Users users)
        {
            users.IsSeller = 1;
            dbContext.Update(users);
            dbContext.SaveChanges();
            return true;

        }
        public bool AddSkills(Seller seller, Skills skills, Users users)
        {

            try
            {
                // skills.SellerId = seller.SellerId;
                // skills.UserId = users.UserId;
                // dbContext.Add(skills);
                // dbContext.SaveChanges();
                Console.WriteLine(skills.SkillName);
                string[] ListSkills = skills.SkillName.Split(',');
                foreach (var item in ListSkills)
                {
                    if (item != "")
                    {
                        Skills skills1 = new Skills();
                        skills1.SellerId = seller.SellerId;
                        skills1.UserId = users.UserId;
                        skills1.SkillName = item;
                        dbContext.Add(skills1);
                        dbContext.SaveChanges();
                    }
                }
                return true;
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
                throw;
            }

        }

        public Users GetUserByUserId(int? Id)
        {
            Users users = new Users();
            users = dbContext.Users.FirstOrDefault(u => u.UserId == Id);
            if (users != null)
            {
                return users;
            }
            else
            {
                return null;
            }
        }

        public Seller GetSellerByUserID(int? userID)
        {
            return dbContext.Seller.FirstOrDefault(s => s.UserId == userID);
        }
        public Seller GetSellerBySellerID(int? sellerId)
        {
            return dbContext.Seller.FirstOrDefault(s => s.SellerId == sellerId);
        }
        public Users GetUserByUsername(string username)
        {
            Users user = dbContext.Users.FirstOrDefault(s => s.UserName == username);
            return user;
        }
        public Users GetUserByEmail(string email)
        {
            Users users = dbContext.Users.FirstOrDefault(s => s.Email == email);

            return users;
        }
        public List<Languages> GetLanguagesByUserId(int? userId)
        {
            List<Languages> languages = dbContext.Languages.Where(x => x.UserId == userId).ToList();
            return languages;
        }
        public List<Skills> GetSkillsByUserId(int? userId)
        {
            List<Skills> listSkills = dbContext.Skills.Where(x => x.UserId == userId).ToList();
            return listSkills;
        }
        public bool UploadAvater(int? userId, string urlAvatar)
        {
            var entity = dbContext.Users.FirstOrDefault(u => u.UserId == userId);
            if (entity != null)
            {
                entity.Avatar = urlAvatar;
                dbContext.Users.Update(entity);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public bool UpdateDescription(int? userId, string description)
        {
            var entity = dbContext.Seller.FirstOrDefault(u => u.UserId == userId);
            if (entity != null)
            {
                entity.Description = description;
                dbContext.Seller.Update(entity);
                dbContext.SaveChanges();
                return true;
            }

            return false;
        }
        public bool UpdateLanguage(int? userId, string language)
        {
            var entity = dbContext.Languages.FirstOrDefault(u => u.UserId == userId);
            if (entity != null)
            {
                entity.Language = language;
                dbContext.Languages.Update(entity);
                dbContext.SaveChanges();
                return true;
            }
            return false;

        }

        public bool UpdateSkills(int? userId, string skillName)
        {
            var entity = dbContext.Skills.FirstOrDefault(u => u.UserId == userId);
            if (entity != null)
            {
                entity.SkillName = skillName;
                dbContext.Skills.Update(entity);
                dbContext.SaveChanges();
                return true;
            }
            return false;

        }



        public bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            string hashOfInput = GetMd5Hash(md5Hash, input);

            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        public bool ChangePassword(string currentpassword, string newpassword, string repassword, int? UserId)
        {
            MD5 md5Hash = MD5.Create();
            Users users = dbContext.Users.FirstOrDefault(x => x.UserId == UserId);
            if (VerifyMd5Hash(md5Hash, currentpassword, users.Password))
            {

                MD5 mD5 = MD5.Create();
                users.Password = GetMd5Hash(mD5, newpassword);
                dbContext.Update(users);
                dbContext.SaveChanges();
                return true;

            }
            return false;
        }
        public bool BillingInformation(string FullName, string Country, string Address, int? userId)
        {
            Users users = dbContext.Users.FirstOrDefault(x => x.UserId == userId);
            users.FullName = FullName;
            users.Country = Country;
            users.Address = Address;
            dbContext.Update(users);
            dbContext.SaveChanges();
            return true;
        }
        public List<Category> getCategoryOfSellerByUserId(int userId)
        {
            Seller seller = dbContext.Seller.FirstOrDefault(x => x.UserId == userId);
            seller.SellerCategorys = dbContext.SellerCategory.Where(x => x.SellerId == seller.SellerId).ToList();
            List<Category> listCategory = new List<Category>();
            foreach (var item in seller.SellerCategorys)
            {
                listCategory.Add(dbContext.Category.FirstOrDefault(x => x.CategoryId == item.CategoryId));
            }
            return listCategory;
        }
    }
}