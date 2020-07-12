using System.Data.Entity;
using System.Linq;
using CRM.Entity;
using System;
using DJ_Entity;

namespace CRM.WEB.Contexts
{
    public class UsersContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<ApplicantExamAttempt> ApplicantExamAttempts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public bool ChangePassword(string userName, string newPassword)
        {
            try
            {
                var user = Users.SingleOrDefault(u => u.UserName == userName);
                user.Password = newPassword;
                SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public void AddUser(User user)
        {
            Users.Add(user);
            SaveChanges();
        }

        public User GetUser(string userName)
        {
            var user = Users.SingleOrDefault(u => u.UserName == userName);
            return user;
        }

        public Applicant GetApplicant(string userName)
        {
            var user = Applicants.SingleOrDefault(u => u.MobileNo == userName);
            return user;
        }

        public ApplicantExamAttempt GetApplicantExamAttempts(int? ApplicantID)
        {
            var data = ApplicantExamAttempts.OrderByDescending(x => x.AttemptID).FirstOrDefault(u => u.ApplicantID == ApplicantID);
            return data;
        }

        public User GetUser(string userName, string password)
        {
            try
            {
                var user = Users.SingleOrDefault(u => u.UserName == userName && u.Password == password);
                return user;
            }
            catch (Exception ex) { return null; }
        }

        public void AddUserRole(UserRole userRole)
        {
            var roleEntry = UserRoles.SingleOrDefault(r => r.UserId == userRole.UserId);
            if (roleEntry != null)
            {
                UserRoles.Remove(roleEntry);
                SaveChanges();
            }
            UserRoles.Add(userRole);
            SaveChanges();
        }
    }
}
