using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using TekkenApp.Data;
using TekkenApp.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            TekkenDbContext _tekkenDbContext = new TekkenDbContext();
//tekkendbContext.GetAllEntityQueries()


            //var someDbSet = tekkendbContext.Set(typeof(HitType_name));
           // Console.WriteLine(someDbSet);

            //tekkendbContext.Set(typeof(HitType_name)).Add(entity);


            //foreach (var entity in tekkendbContext.GetAllEntityQueries())
            //{
            //    Console.WriteLine(entity);
            //}


            var a = _tekkenDbContext.hitType;
            BaseTranslateName transe = new HitType_name();
            transe.Base_code = 110000011;
            transe.Language_code = "ko";
            transe.Name = "ko";
            transe.Checked = false;


            //var tableName = "HitType_name";
            //    tekkendbContext.GetDBSet("asfd").
            //Type objType = typeof(HitType_name);

            //// Print the assembly full name.
            //Console.WriteLine($"Assembly full name:\n   {objType.Assembly.FullName}.");

            //// Print the assembly qualified name
            //Console.WriteLine($"Assembly qualified name:\n   {objType.AssemblyQualifiedName}.");
            //IQueryable q = tekkendbContext.GetEntityQuery(typeof(HitType_name));
            //tekkendbContext.remo
            //NewMethod(tableName, objType);

            //foreach (BaseEntity entity in list)
            //{
            //    cntx.Set(entity.GetType()).Add(entity);
            //}
        }

        private static void NewMethod(string tableName, Type objType)
        {
            //var tableName = "HitType_name";
            string tableFullName = $"{objType.AssemblyQualifiedName}{tableName}";
            string domainAssembly = $"{objType.AssemblyQualifiedName}";
            string assemblyQualifiedName = Assembly.CreateQualifiedName(domainAssembly, tableFullName);
            var entityType = Type.GetType(objType.AssemblyQualifiedName);
            //Type entityType = typeof(HitType_name);

            using (var dbContext = new TekkenDbContext())
            {

                //var dbSet = dbContext.Set(entityType);

                var entityJson = Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    Avatar = "https://www.google.com/images/branding/googlelogo/2x/googlelogo_color_150x54dp.png",
                    //Gender = UserGender.Man,
                    NickName = "UserName",
                    UniqueId = Guid.NewGuid().ToString("N"),
                });
                BaseTranslateName transe = new HitType_name();
                transe.Base_code = 110000011;
                transe.Language_code = "ko";
                transe.Name = "ko";
                transe.Checked = false;

                //            var model = Newtonsoft.Json.JsonConvert.DeserializeObject(entityJson, entityType);
                ///r methods = dbSet.GetType().GetMethods();


                

                //object retVal = AddMethod.Invoke(dbSet, new object[] { transe });
                //object retVal = AddMethod.Invoke(dbSet, transe);
                dbContext.SaveChanges();
            }
        }
    }
    public static class Tt
    {

        public static IQueryable Set(this DbContext context, Type T)
        {
            MethodInfo method = typeof(DbContext).GetMethods()
                .Where(p => p.Name == "Set" && p.ContainsGenericParameters).FirstOrDefault();

            // Build a method with the specific type argument you're interested in
            method = method.MakeGenericMethod(T);

            return method.Invoke(context, null) as IQueryable;
        }

        public static IQueryable GetEntityQuery(this DbContext context, Type entityType)
        {
            MethodInfo method = typeof(DbContext).GetMethod(nameof(DbContext.Set));
            MethodInfo genericMethod = method.MakeGenericMethod(entityType);
            var dbSet = genericMethod.Invoke(context, null);

            return (IQueryable)dbSet;
        }

        public static IEnumerable<IQueryable> GetAllEntityQueries(this DbContext context)
        {
            foreach (var entityType in context.Model.GetEntityTypes())
            {
                yield return context.GetEntityQuery(entityType.ClrType);
            }
        }
    }
}

