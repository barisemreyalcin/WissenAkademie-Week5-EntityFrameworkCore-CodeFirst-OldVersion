using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region DBINIT AND CHANGE TRACKER
            // Db'ye yansıtmamız için:
            //using (SchoolDbContext context = new SchoolDbContext())
            //{
            //    //Gender gender = new Gender();
            //    //gender.GenderID = 1;
            //    //gender.GenderName = "Male";
            //    //gender.State = true;

            //    Student student = new Student()
            //    {
            //        FirstName = "Jax",
            //        LastName = "Teller",
            //        BirthDate = DateTime.Now.AddYears(-30),
            //        Email = "jt@contoso.com",
            //        Phone = "123456789",
            //        Weight = 80,
            //        Height = 180,
            //        //Gender = gender,
            //    };

            //    context.Students.Add(student); // Localde collection'a ekliyorum

            //    // data çekip student'e atıyoz
            //    Gender gender = context.Genders.Find(1);
            //    student.Gender = gender;

            //    context.SaveChanges(); // db'ye yansıtır

            //    #region Entities TrackChanges
            //    ChangeTracker();
            //    #endregion
            //}
            #endregion
            
            #region LOADING TYPES
            NORTHWINDEntities context = new NORTHWINDEntities();
            #region LAZY
            // Default: Lazy Loading
            //var customers = context.Customers.ToList(); // Connection açıp kapatıyor, sadece customer bilgileri gelir, ilişkili diğer tablolardaki bilgiler onları ayrı çağırmadığım sürece gelmez. 
            //var customer = customers.Where(cus => cus.CustomerID == "VINET").FirstOrDefault();
            //var orderList = from order in context.Orders // Buradaki bilgileri çağırınca erişiyorum
            //                join orderDetail in context.Order_Details
            //                on order.OrderID equals orderDetail.OrderID
            //                where order.CustomerID == customer.CustomerID
            //                select new
            //                {
            //                    OrderID = orderDetail.OrderID,
            //                    OrderDate = order.OrderDate,
            //                    OrderDetails = order.Order_Details,
            //                };

            //foreach (var item in orderList)
            //{
            //    Console.WriteLine($"Order ID: {item.OrderID} - Order Date: {item.OrderDate}");
            //    Console.WriteLine($"------------------- Order Detail Info -------------------");
            //    foreach (var itemDetail in item.OrderDetails)
            //    {
            //        Console.WriteLine($"Product ID: {itemDetail.ProductID} - Product Name: {itemDetail.Products.ProductID} - " +
            //            $"Quantity: {itemDetail.Quantity} - Unit Price: {itemDetail.UnitPrice} - Discount: {itemDetail.Discount}");
            //    }
            //    Console.WriteLine($"---------------------------------------------------------");
            //}
            #endregion

            ///////////////////////////////////////////////////////////////////////////////////////

            #region EAGER
            // Eager Loading
            // İlişkili tablolardaki datamı yüklemek için

            // Buradda kategorileri alırken product bilgileri de alalım
            //var categoryList = from category in context.Categories // Category için connection atarken
            //                   .Include("Products") // Product bilgilerine de böyle erişirim
            //                   where category.CategoryName.Contains("D")
            //                   select new
            //                   {
            //                       CategoryID = category.CategoryID,
            //                       CategoryName = category.CategoryName,
            //                       Products = category.Products
            //                   };

            //foreach (var item in categoryList)
            //{
            //    Console.WriteLine($"Category ID: {item.CategoryID} - Category Name: {item.CategoryName} - Product Count: {item.Products.Count()}");
            //    Console.WriteLine($"------------------- Products -------------------");
            //    foreach (var itemProduct in item.Products)
            //    {
            //    Console.WriteLine($"Product ID: {itemProduct.ProductID} - Product Name: {itemProduct.ProductName}");
            //    }
            //    Console.WriteLine($"---------------------------------------------------------");
            //}

            // Birden fazla ilişkili data için birden fazla include kullanılabilir
            //var orderList = from order in context.Orders
            //                .Include("Customers")
            //                .Include("Employees")
            //                where order.CustomerID == "VINET"
            //                select new
            //                {
            //                    OrderId = order.OrderID,
            //                    CustomerName = order.Customers.CompanyName,
            //                    EmployeeName = string.Concat(order.Employees.FirstName, " ", order.Employees.LastName),
            //                };
            //foreach (var item in orderList)
            //{
            //    Console.WriteLine($"OrderID: {item.OrderId} - Customer Name: {item.CustomerName} - Employee Name: {item.EmployeeName}");
            //}
            #endregion

            #region EXPLICIT
            // Explicit Loading
            // Ben çağırdığımda aynı connection ile çalışır. Hem Lazy gibi hem Eager gibi çalışır
            //var orders = context.Orders
            //    .Where(x => x.OrderID == 10252).FirstOrDefault();

            //context.Entry(orders).Reference(x => x.Customers).Load(); // aynı connectiondan bu datayı yükler
            //context.Entry(orders).Reference(x => x.Employees).Load();

            //Console.WriteLine($"Order ID: {orders.OrderID} - Customer Name: {orders.Customers.CompanyName} - " + 
            //    $"Employee Name: {orders.Employees.FirstName + " " + orders.Employees.LastName}");
            #endregion

            // SqlQuery
            // Standart Framework'te sql yazma şansımız var
            var orderSqlQuery = context.Orders
                .SqlQuery("Select * From Orders Where CustomerID = @CustomerID", new SqlParameter("@CustomerID", "VINET"));

            foreach (var item in orderSqlQuery)
            {
                Console.WriteLine($"Order ID: {item.OrderID} - Order Date: {item.OrderDate}");
            }

            Console.ReadLine();
            #endregion
        }

        private static void ChangeTracker()
        {
            // Hangi entity hangi durumda takip ettiğim state'ler mevcut
            /* Entity States:
             * 1 - Detached: Böyle olanları takip etmiyor
             * 2 - Added: Yeni bir context eklenince
             * 3 - Unchanged: Db'den ilk sorgumda her şey bu state ile locale yüklenir
             * 4 - Modified: Update ettiğimde
             * 5 - Deleted: Context'ten silince
             */

            // Bu sistem Hem .net core ve standart'ta aynı çalışır

            Console.WriteLine("****************** Change Track Start ******************");
            using(SchoolDbContext context = new SchoolDbContext())
            {
                Student student = new Student()
                {
                    FirstName = "Ada",
                    LastName = "Shelby",
                    BirthDate = DateTime.Now.AddYears(-28),
                    Email = "as@contoso.com",
                    Phone = "987654321",
                    Weight = 55,
                    Height = 168,
                };
                Console.WriteLine("****************** Add New Student ******************");
                context.Students.Add(student);

                // Nasıl bir değişiklik olduğunu gösteren method
                DisplayEntitiesTrackState(context);

                Console.WriteLine("****************** Get Student ******************");
                Student existingStudent = context.Students.Find(1);
                DisplayEntitiesTrackState(context);

                Console.WriteLine("****************** Get Gender ******************");
                Gender existingGender = context.Genders.Find(3);
                DisplayEntitiesTrackState(context);

                Console.WriteLine("****************** Update Gender ******************");
                existingGender.GenderName = "Default Gender Update";
                DisplayEntitiesTrackState(context);

                Console.WriteLine("****************** Delete Student ******************");
                context.Students.Remove(existingStudent);
                DisplayEntitiesTrackState(context);

                context.SaveChanges();

                Console.ReadLine();

            }
        }

        private static void DisplayEntitiesTrackState(SchoolDbContext context)
        {
            DbChangeTracker changeTracker = context.ChangeTracker;
            Console.WriteLine($"Context Track Entities: {changeTracker.Entries().Count()}");

            var entries = changeTracker.Entries();
            foreach (var ent in entries)
            {
                Console.WriteLine($"Entry Name: {ent.Entity.GetType().Name}"); // hangi entity'de değişiklik
                Console.WriteLine($"Entity Status: {ent.State}");
            }
        }
    }
}
