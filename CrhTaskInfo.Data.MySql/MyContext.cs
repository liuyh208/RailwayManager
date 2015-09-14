//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Text;
//using GasWebMap.Domain;
//using GasWebMap.Repository.MySql;

//namespace CrhTaskInfo.Data.MySql
//{
//    public class MyContext:AbstractDbContext
//    {
//        public MyContext() : base("mysql")
//        {
//        }

       
//        public DbSet<User> Users { get; set; }

//        public DbSet<Role> Roles { get; set; }

//        public DbSet<MenuInfo> Menus { get; set; }

//        public DbSet<Department> Departments { get; set; }

//        public DbSet<DeviceInfo> DeviceInfos { get; set; }
//        public DbSet<SlabProblem> SlabProblems { get; set; }
//        public DbSet<ImageStore> ImageStores { get; set; }
//        public DbSet<UserRole> UserRoles { get; set; }


//        public DbSet<RoleMenu> RoleMenus { get; set; }

//        public DbSet<MenuInfoGroup> MenuInfoGroups { get; set; } 
//    }
//}
