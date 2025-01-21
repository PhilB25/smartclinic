
using System.Data;
using System.Reflection;
using ClosedXML.Excel;
using Npgsql;
using smartclinic.Extensions;
using smartclinic.Models;

namespace smartclinic.Services
{
    public class UserService
    {
        public readonly DBContext _context;

        public UserService(DBContext context)
        {
            _context = context;
        }
        public List<User> GetUser(){
            _context.DB.Open();
             var query = "SELECT * FROM smartclinic.users";
                List<User> users = new List<User>();
                NpgsqlCommand cmd = new NpgsqlCommand(query,_context.DB);

            NpgsqlDataReader reader =  cmd.ExecuteReader();
            while (reader.Read()){
                User user = new User();
                user.id = reader.GetInt32(0);
                user.username = reader.GetString(1);
                user.email = reader.GetString(2);
                user.password = reader.GetString(3);
                user.created_at = reader.GetDateTime(5);
                user.updated_at = reader.GetDateTime(6);
                user.deleted_at = reader.IsDBNull(7) ? DateTime.UtcNow : reader.GetDateTime(7);
                user.is_deleted = reader.GetBoolean(8);
                user.firstname = reader.GetString(9);
                user.lastname = reader.GetString(10);
                users.Add(user);
            }
            return users;
        }
        public MemoryStream UserToExcel(List<User> users){
            var wb = new XLWorkbook();
            var data = users.ToDataTable();
            wb.AddSheet("Users");
            wb.WriteDataByList(users);
            wb.SaveAs("C:\\Users\\Phil\\source\\repos\\smartclinic\\smartclinic\\ExcelFile\\Users.xlsx");
            return wb.ToFile();
        }
       
    
    }
}